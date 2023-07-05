using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Answer : MonoBehaviour
{
    [SerializeField] private TMP_Text answerText;

    public void AssignAnswer(int correctAnswer)
    {
        answerText.text = correctAnswer.ToString();
    }

    public void GenerateWrongAnswer(int numberRange, int correctAnswer)
    {
        int wrongAnswer = 0;
        do
        {
            wrongAnswer = Random.Range(1, numberRange);
        }
        while (wrongAnswer == correctAnswer);

        answerText.text = wrongAnswer.ToString();
    }
}
