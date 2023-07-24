using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Answer : MonoBehaviour
{
    [SerializeField] private TMP_Text answerText;
    [SerializeField] private LayerMask collidersWithObjectsLayerMask;

    private int boardGameSizeX = 0;
    private int boardGameSizeY = 0;
    private int randomPosX = 0;
    private int randomPosZ = 0;

    private Vector3 prefabPosition = Vector3.zero;

    private void Start()
    {
        boardGameSizeX = GameManager.InstanceGM.GameSizeX;
        boardGameSizeY = GameManager.InstanceGM.GameSizeY;

        prefabPosition = transform.position;
    }

    private void OnEnable()
    {
        prefabPosition = transform.position;
        RePosition();
    }

    /// <summary>
    /// Method to assign answer to UI element
    /// </summary>
    /// <param name="correctAnswer"></param>
    public void AssignAnswer(int correctAnswer)
    {
        answerText.text = correctAnswer.ToString();
    }

    /// <summary>
    /// Method for generating all wrong answers depending on range in level
    /// </summary>
    /// <param name="numberRange"></param>
    /// <param name="correctAnswer"></param>
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

    /// <summary>
    /// Method for changing answer position if is to close from obstacles
    /// </summary>
    public void RePosition()
    {
        List<Transform> allSnake = GameManager.InstanceGM.GetAllSnakeParts();
        float minDistanceToSnake = GameManager.InstanceGM.GetDetectionRange();

        while (allSnake.Any(s => Vector3.Distance(prefabPosition, s.position) < minDistanceToSnake))
        {
            RandomAnswerPositionXZ();
        }

        transform.position = prefabPosition;

    }

    /// <summary>
    /// Method for draw random position coordinates for answer
    /// </summary>
    private void RandomAnswerPositionXZ()
    {
        randomPosX = Random.Range(-boardGameSizeX, boardGameSizeX + 1);
        randomPosZ = Random.Range(-boardGameSizeY, boardGameSizeY + 1);
        prefabPosition = new Vector3(randomPosX, 0.0f, randomPosZ);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsInLayerMask(collision.gameObject, collidersWithObjectsLayerMask))
        {
            RandomAnswerPositionXZ();
            RePosition();
        }
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return (layerMask.value & (1 << obj.layer)) > 0;
    }
}
