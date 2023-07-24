using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

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

    [SerializeField] private float detectionRange = 5.0f;

    private int taskNumber = 0;
    private int firstNumber = 0;
    private int secondNumber = 0;
    private int resultNumber = 0;
    private int boardGameSizeX = 0;
    private int boardGameSizeY = 0;
    private int randomPosX = 0;
    private int randomPosZ = 0;

    private float spawnAnswersDelayTime = 1.2f;

    private bool areAnswersSpawned = false;

    private List<GameObject> allAnswers = new List<GameObject>();

    public int MaxTasksNumber { get { return maxTasksNumber; } }

    public bool AreAnswersSpawned { get { return areAnswersSpawned; } }

    public float DetectionRange { get { return detectionRange; } }

    private void Start()
    {
        boardGameSizeX = GameManager.InstanceGM.GameSizeX;
        boardGameSizeY = GameManager.InstanceGM.GameSizeY;
        levelNumberText.text = "Level: " + GameManager.InstanceGM.CurrentSceneIndex;
        SpawnAnswers();
    }

    /// <summary>
    /// Method responsible for spawning correct answer in game map
    /// </summary>
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

    /// <summary>
    /// Method responsible for assigning correct answer in UI element on answer prefab
    /// </summary>
    /// <param name="correctAnswerGO"></param>
    private void AssignCorrectAnswer(GameObject correctAnswerGO)
    {
        Answer correctAnswer = correctAnswerGO.GetComponent<Answer>();
        correctAnswer.AssignAnswer(resultNumber);
    }

    /// <summary>
    /// Method responsible for spawning wrong answer in game map
    /// </summary>
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

    /// <summary>
    /// Method responsible for assigning wrong answer in UI element on answer prefab
    /// </summary>
    /// <param name="wrongAnswerGO"></param>
    private void AssignWrongAnswer(GameObject wrongAnswerGO)
    {
        Answer wrongAnswer = wrongAnswerGO.GetComponent<Answer>();
        wrongAnswer.GenerateWrongAnswer(numberRange * 2, resultNumber);
    }

    private GameObject GeneratePrefab(GameObject answer)
    {
        List<Transform> allSnake = GameManager.InstanceGM.GetAllSnakeParts();
        Vector3 prefabPosition = Vector3.zero;

        do
        {
            randomPosX = Random.Range(-boardGameSizeX, boardGameSizeX + 1);
            randomPosZ = Random.Range(-boardGameSizeY, boardGameSizeY + 1);
            prefabPosition = new Vector3(randomPosX, 0.0f, randomPosZ);
        }
        while (allSnake.Any(s => Vector3.Distance(prefabPosition, s.position) < detectionRange));

        return Instantiate(answer, prefabPosition, Quaternion.identity);
    }

    /// <summary>
    /// Method responsible for hidden all answers during the game
    /// </summary>
    private void RemoveAllAnswers()
    {
        areAnswersSpawned = false;

        for (int i = 0; i < allAnswers.Count; i++)
        {
            Answer answer = allAnswers[i].GetComponent<Answer>();
            answer.RePosition();
            allAnswers[i].SetActive(false);
        }
    }

    /// <summary>
    /// Coroutine responsible for delay SpawnAnswers() method
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnAnswersDelay()
    {
        yield return new WaitForSeconds(spawnAnswersDelayTime);
        SpawnAnswers();
    }

    /// <summary>
    /// Method responsible for generate tasks, assign them to UI element and spawning all answers (correct and wrong)
    /// </summary>
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

            areAnswersSpawned = true;
        }
    }

    /// <summary>
    /// Method responsible for showing on UI element correct player choice
    /// </summary>
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

    /// <summary>
    /// Method responsible for showing on UI element wrong player choice
    /// </summary>
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

    /// <summary>
    /// Method responsible for showing all answers during the game
    /// </summary>
    public void ShowAllAnswers()
    {
        for (int i = 0; i < allAnswers.Count; i++)
        {
            allAnswers[i].SetActive(true);
        }
    }

    /// <summary>
    /// Method responsible for returning correct answer transform
    /// </summary>
    /// <returns></returns>
    public Transform CorrectAnswer()
    {
        if (allAnswers.Count > 0)
        {
            return allAnswers[0].transform;
        }
        else
        {
            return null;
        }
    }
}
