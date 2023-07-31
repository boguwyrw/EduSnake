using UnityEngine;

public class PointerArrow : MonoBehaviour
{
    [SerializeField] private GameObject pointerArrow;
    [SerializeField] private GameObject destinationPoint;

    private Transform correctAnswer;

    private float minDistanceToAnswer = 1.85f;
    private float middleValue = 0.6f;
    private float minValue = 0.2f;
    private float currentMiddleValue = 0.0f;
    private float increaseValue = 0.005f;
    private float uiCoveringValue = 0.88f;

    private void Start()
    {
        pointerArrow.SetActive(false);
        destinationPoint.SetActive(false);
        currentMiddleValue = middleValue;
        correctAnswer = GameManager.Instance.GetCorrectAnswer();
    }

    private void LateUpdate()
    {
        PointerArrowFuncionality();
    }

    /// <summary>
    /// Method responsible for pointer arrow working system and replace them with destination point
    /// </summary>
    private void PointerArrowFuncionality()
    {
        if (GameManager.Instance.GetCorrectAnswer() != null && GameManager.Instance.GetAreAnswersSpawned())
        {
            Vector3 startPosition = new Vector3(transform.parent.position.x, correctAnswer.position.y, transform.parent.position.z);
            float distanceToAnswer = Vector3.Distance(transform.position, correctAnswer.position);

            Vector3 viewPos = Camera.main.WorldToViewportPoint(correctAnswer.position);

            if (viewPos.x > 0.0f && viewPos.y < uiCoveringValue && viewPos.y > 0.0f)
            {
                if (currentMiddleValue < middleValue)
                {
                    currentMiddleValue += increaseValue;
                }
                transform.position = Vector3.Lerp(startPosition, correctAnswer.position, currentMiddleValue);
            }
            else
            {
                if (currentMiddleValue > minValue)
                {
                    currentMiddleValue -= increaseValue;
                }
                transform.position = Vector3.Lerp(startPosition, correctAnswer.position, currentMiddleValue);
            }

            transform.LookAt(correctAnswer);

            if (distanceToAnswer < minDistanceToAnswer)
            {
                pointerArrow.SetActive(false);
                destinationPoint.SetActive(true);
            }
            else
            {
                pointerArrow.SetActive(true);
                destinationPoint.SetActive(false);
            }
        }
        else
        {
            pointerArrow.SetActive(false);
            destinationPoint.SetActive(false);
        }
    }
}
