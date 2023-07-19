using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Answer : MonoBehaviour
{
    [SerializeField] private TMP_Text answerText;
    [SerializeField] private LayerMask snakeLayerMask;
    [SerializeField] private LayerMask collidersLayerMask;

    private int boardGameSizeX = 0;
    private int boardGameSizeY = 0;


    private void Start()
    {
        boardGameSizeX = GameManager.InstanceGM.GameSizeX;
        boardGameSizeY = GameManager.InstanceGM.GameSizeY;
    }

    private void OnEnable()
    {
        // zrobic RePosition() jak pojawi siê za blisko weza
        Collider[] snakeColliders = null;
        //List<Transform> allSnakeParts = GameManager.InstanceGM.GetAllSnakeParts();
        do
        {
            RePosition();
            snakeColliders = Physics.OverlapSphere(transform.position, 5.0f, collidersLayerMask);
        }
        while (snakeColliders.Length > 0);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (IsInLayerMask(collision.gameObject, snakeLayerMask))
        {
            RePosition();
        }
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return (layerMask.value & (1 << obj.layer)) > 0;
    }
}
