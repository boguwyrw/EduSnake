using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MathTaskGenerator : MonoBehaviour
{
    [SerializeField] private TMP_Text taskNumberText;
    [SerializeField] private TMP_Text firstNumberText;
    [SerializeField] private TMP_Text secondNumberText;
    [SerializeField] private TMP_Text resultText;

    [SerializeField] private GameObject correctAnswerPrefab;
    [SerializeField] private GameObject wrongAnswerPrefab;

    [SerializeField] private int maxTasksNumber = 30;

    private int taskNumber = 1;
    private int firstNumber = 0;
    private int secondNumber = 0;
    private int resultNumber = 0;
    private int numberRange = 101;
    private int boardGameSizeX = 0;
    private int boardGameSizeY = 0;

    private float spawnAnswersDelayTime = 1.2f;

    private List<GameObject> allAnswers = new List<GameObject>();

    private void Start()
    {
        boardGameSizeX = GameManager.InstanceGM.GameSizeX;
        boardGameSizeY = GameManager.InstanceGM.GameSizeY;
        SpawnAnswers();
    }

    private void Update()
    {
        
    }

    private void SpawnCorrectAnswer()
    {
        GameObject correctAnswerClone = GeneratePrefab(correctAnswerPrefab);
        allAnswers.Add(correctAnswerClone);
        Answer correctAnswer = correctAnswerClone.GetComponent<Answer>();
        correctAnswer.AssignAnswer(resultNumber);
    }

    private void SpawnWrongAnswer()
    {
        GameObject wrongAnswerClone = GeneratePrefab(wrongAnswerPrefab);
        allAnswers.Add(wrongAnswerClone);
        Answer wrongAnswer = wrongAnswerClone.GetComponent<Answer>();
        wrongAnswer.GenerateWrongAnswer(numberRange * 2, resultNumber);
    }

    private GameObject GeneratePrefab(GameObject answer)
    {
        int randomPosX = Random.Range(-boardGameSizeX, boardGameSizeX + 1);
        int randomPosZ = Random.Range(-boardGameSizeY, boardGameSizeY + 1);
        return Instantiate(answer, new Vector3(randomPosX, 0.0f, randomPosZ), Quaternion.identity);
    }

    private void SpawnAnswers()
    {
        if (taskNumber < maxTasksNumber)
        {
            taskNumberText.text = "Task: " + taskNumber.ToString();
            firstNumber = Random.Range(1, numberRange);
            firstNumberText.text = firstNumber.ToString();
            secondNumber = Random.Range(1, numberRange);
            secondNumberText.text = secondNumber.ToString();
            resultNumber = firstNumber + secondNumber;
            resultText.color = Color.white;
            resultText.text = "???";

            SpawnCorrectAnswer();

            for (int i = 0; i < taskNumber; i++)
            {
                SpawnWrongAnswer();
            }
            taskNumber++;
        }
    }

    private void RemoveAllAnswers()
    {
        for (int i = 0; i < allAnswers.Count; i++)
        {
            Destroy(allAnswers[i]);
            // przy pool zrobiæ gameObject.SetActive(false);
        }
    }

    private IEnumerator SpawnAnswersDelay()
    {
        yield return new WaitForSeconds(spawnAnswersDelayTime);
        SpawnAnswers();
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
}
