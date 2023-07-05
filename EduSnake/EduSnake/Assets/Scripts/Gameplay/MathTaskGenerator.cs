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

    private int firstNumber = 0;
    private int secondNumber = 0;
    private int resultNumber = 0;
    private int numberRange = 101;
    private int taskNumber = 1;

    private float spawnAnswersDelayTime = 1.2f;

    private List<GameObject> allAnswers = new List<GameObject>();

    private void Start()
    { 
        SpawnAnswers();
    }

    private void Update()
    {
        
    }

    private void SpawnCorrectAnswer()
    {
        int randomPosX = Random.Range(-23, 24);
        int randomPosZ = Random.Range(-23, 24);
        GameObject correctAnswerClone = Instantiate(correctAnswerPrefab, new Vector3(randomPosX, 0.0f, randomPosZ), Quaternion.identity);
        allAnswers.Add(correctAnswerClone);
        Answer correctAnswer = correctAnswerClone.GetComponent<Answer>();
        correctAnswer.AssignAnswer(resultNumber);
    }

    private void SpawnWrongAnswer()
    {
        int randomPosX = Random.Range(-23, 24);
        int randomPosZ = Random.Range(-23, 24);
        GameObject wrongAnswerClone = Instantiate(wrongAnswerPrefab, new Vector3(randomPosX, 0.0f, randomPosZ), Quaternion.identity);
        allAnswers.Add(wrongAnswerClone);
        Answer wrongAnswer = wrongAnswerClone.GetComponent<Answer>();
        wrongAnswer.GenerateWrongAnswer(numberRange * 2, resultNumber);
    }

    private void SpawnAnswers()
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

    private void RemoveAllAnswers()
    {
        for (int i = 0; i < allAnswers.Count; i++)
        {
            Destroy(allAnswers[i]);
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
        StartCoroutine(SpawnAnswersDelay());
    }

    public void ShowPlayerWrongChoose()
    {
        resultText.color = Color.red;
        resultText.text = "NO";
        RemoveAllAnswers();
        StartCoroutine(SpawnAnswersDelay());
    }
}
