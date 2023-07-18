using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MathTaskGenerator : MonoBehaviour
{
    [SerializeField] private TMP_Text taskNumberText;
    [SerializeField] private TMP_Text levelNumberText;
    [SerializeField] private TMP_Text firstNumberText;
    [SerializeField] private TMP_Text secondNumberText;
    [SerializeField] private TMP_Text resultText;

    [SerializeField] private GameObject correctAnswerPrefab;
    [SerializeField] private GameObject wrongAnswerPrefab;

    [SerializeField] private int maxTasksNumber = 30;
    [SerializeField] private int numberRange = 31;

    private int taskNumber = 0;
    private int firstNumber = 0;
    private int secondNumber = 0;
    private int resultNumber = 0;
    private int boardGameSizeX = 0;
    private int boardGameSizeY = 0;

    private float spawnAnswersDelayTime = 1.2f;

    private List<GameObject> allAnswers = new List<GameObject>();

    public int MaxTasksNumber { get { return maxTasksNumber; } }

    private void Start()
    {
        boardGameSizeX = GameManager.InstanceGM.GameSizeX;
        boardGameSizeY = GameManager.InstanceGM.GameSizeY;
        levelNumberText.text = "Level: " + GameManager.InstanceGM.CurrentSceneIndex;
        SpawnAnswers();
    }

    private void SpawnCorrectAnswer()
    {
        if (allAnswers.Count > 0)
        {
            AssignCorrectAnswer(allAnswers[0]);
            allAnswers[0].SetActive(true);
        }
        else
        {
            GameObject correctAnswerClone = GeneratePrefab(correctAnswerPrefab);
            allAnswers.Add(correctAnswerClone);
            AssignCorrectAnswer(correctAnswerClone);
        }
    }

    private void AssignCorrectAnswer(GameObject correctAnswerGO)
    {
        Answer correctAnswer = correctAnswerGO.GetComponent<Answer>();
        correctAnswer.AssignAnswer(resultNumber);
    }

    private void SpawnWrongAnswer()
    {
        int answersCount = taskNumber + 1;
        for (int i = 1; i < answersCount; i++)
        {
            if (i < allAnswers.Count)
            {
                AssignWrongAnswer(allAnswers[i]);
                allAnswers[i].SetActive(true);
            }
            else
            {
                GameObject wrongAnswerClone = GeneratePrefab(wrongAnswerPrefab);
                allAnswers.Add(wrongAnswerClone);
                AssignWrongAnswer(wrongAnswerClone);
            }
        }
    }

    private void AssignWrongAnswer(GameObject wrongAnswerGO)
    {
        Answer wrongAnswer = wrongAnswerGO.GetComponent<Answer>();
        wrongAnswer.GenerateWrongAnswer(numberRange * 2, resultNumber);
    }

    private GameObject GeneratePrefab(GameObject answer)
    {
        int randomPosX = Random.Range(-boardGameSizeX, boardGameSizeX + 1);
        int randomPosZ = Random.Range(-boardGameSizeY, boardGameSizeY + 1);
        // najpierw sprawdziæ czy pozycja jest daleko od weza a pozniej ja zespawnowac
        Vector3 prefabPosition = new Vector3(randomPosX, 0.0f, randomPosZ);
        // check distance to snake
        return Instantiate(answer, prefabPosition, Quaternion.identity);
    }

    private void RemoveAllAnswers()
    {
        for (int i = 0; i < allAnswers.Count; i++)
        {
            Answer answer = allAnswers[i].GetComponent<Answer>();
            answer.RePosition();
            allAnswers[i].SetActive(false);
        }
    }

    private IEnumerator SpawnAnswersDelay()
    {
        yield return new WaitForSeconds(spawnAnswersDelayTime);
        SpawnAnswers();
    }

    public void SpawnAnswers()
    {
        if (taskNumber < maxTasksNumber)
        {
            taskNumber++;

            taskNumberText.text = "Task: " + taskNumber.ToString() + "/" + maxTasksNumber;
            firstNumber = Random.Range(1, numberRange);
            firstNumberText.text = firstNumber.ToString();
            secondNumber = Random.Range(1, numberRange);
            secondNumberText.text = secondNumber.ToString();
            resultNumber = firstNumber + secondNumber;
            resultText.color = Color.black;
            resultText.text = "???";

            SpawnCorrectAnswer();

            for (int i = 0; i < taskNumber; i++)
            {
                SpawnWrongAnswer();
            }
        }
    }

    public void ShowPlayerCorrectChoose()
    {
        resultText.color = Color.green;
        resultText.text = resultNumber.ToString();
        RemoveAllAnswers();
        if (taskNumber < maxTasksNumber)
        {
            StartCoroutine(SpawnAnswersDelay());
        }
        else if (taskNumber == maxTasksNumber)
        {
            GameManager.InstanceGM.WinGameOver();
        }
    }

    public void ShowPlayerWrongChoose()
    {
        resultText.color = Color.red;
        resultText.text = "NO";
        GameManager.InstanceGM.RemoveSnakeLife();
        RemoveAllAnswers();
        if (taskNumber < maxTasksNumber)
        {
            StartCoroutine(SpawnAnswersDelay());
        }
        else if (taskNumber == maxTasksNumber)
        {
            GameManager.InstanceGM.WinGameOver();
        }
    }

    public void ShowAllAnswers()
    {
        for (int i = 0; i < allAnswers.Count; i++)
        {
            allAnswers[i].SetActive(true);
        }
    }
}
