using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Answer : MonoBehaviour
{
    [SerializeField] private TMP_Text answerText;

    private int boardGameSizeX = 0;
    private int boardGameSizeY = 0;

    private void Start()
    {
        boardGameSizeX = GameManager.InstanceGM.GameSizeX;
        boardGameSizeY = GameManager.InstanceGM.GameSizeY;
    }

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

    private void RePosition()
    {
        int randomPosX = Random.Range(-boardGameSizeX, boardGameSizeX + 1);
        int randomPosZ = Random.Range(-boardGameSizeY, boardGameSizeY + 1);
        transform.position = new Vector3(randomPosX, 0.0f, randomPosZ);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            RePosition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            RePosition();
        }
    }
}
