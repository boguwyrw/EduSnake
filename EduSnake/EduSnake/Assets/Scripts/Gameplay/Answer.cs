using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Answer : MonoBehaviour
{
    [SerializeField] private TMP_Text answerText;
    [SerializeField] private LayerMask snakeLayerMask;
    //[SerializeField] private float detectionRange = 10.0f;

    private int boardGameSizeX = 0;
    private int boardGameSizeY = 0;


    private void Start()
    {
        boardGameSizeX = GameManager.InstanceGM.GameSizeX;
        boardGameSizeY = GameManager.InstanceGM.GameSizeY;

        //SnakeDetection();
    }

    public void AssignAnswer(int correctAnswer)
    {
        answerText.text = correctAnswer.ToString();
    }

    public void GenerateWrongAnswer(int numberRange, int correctAnswer)
    {
        int wrongAnswer = -1;
        do
        {
            wrongAnswer = Random.Range(1, numberRange);
        }
        while (wrongAnswer == correctAnswer);

        answerText.text = wrongAnswer.ToString();
    }

    public void RePosition()
    {
        int randomPosX = Random.Range(-boardGameSizeX, boardGameSizeX + 1);
        int randomPosZ = Random.Range(-boardGameSizeY, boardGameSizeY + 1);
        transform.position = new Vector3(randomPosX, 0.0f, randomPosZ);
    }
    /*
    private void SnakeDetection()
    {
        Collider[] collidersDetected = Physics.OverlapSphere(transform.position, detectionRange, snakeLayerMask);
        if (collidersDetected.Length > 0)
        {
            RePosition();
        }
    }
    */
    private void OnCollisionEnter(Collision collision)
    {
        bool collisionsObjects = collision.gameObject.layer == 8 || collision.gameObject.layer == 9 || collision.gameObject.layer == 10;
        if (collisionsObjects)
        {
            RePosition();
        }
    }
}
