using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerArrow : MonoBehaviour
{
    [SerializeField] private GameObject pointerArrow;
    [SerializeField] private GameObject destinationPoint;

    private Transform correctAnswerTransform;

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
    }

    private void LateUpdate()
    {
        if (GameManager.InstanceGM.GetCorrectAnswer() != null && GameManager.InstanceGM.GetAreAnswersSpawned())
        {
            Vector3 startPosition = new Vector3(transform.parent.position.x, GameManager.InstanceGM.GetCorrectAnswer().position.y, transform.parent.position.z);
            float distanceToAnswer = Vector3.Distance(transform.position, GameManager.InstanceGM.GetCorrectAnswer().position);
            //Debug.Log(distanceToAnswer);
            Vector3 viewPos = Camera.main.WorldToViewportPoint(GameManager.InstanceGM.GetCorrectAnswer().position);

            if (viewPos.x > 0.0f && viewPos.y < uiCoveringValue && viewPos.y > 0.0f)
            {
                if (currentMiddleValue < middleValue)
                {
                    currentMiddleValue += increaseValue;
                }
                transform.position = Vector3.Lerp(startPosition, GameManager.InstanceGM.GetCorrectAnswer().position, currentMiddleValue);
            }
            else
            {
                if (currentMiddleValue > minValue)
                {
                    currentMiddleValue -= increaseValue;
                }
                transform.position = Vector3.Lerp(startPosition, GameManager.InstanceGM.GetCorrectAnswer().position, currentMiddleValue);
            }

            transform.LookAt(GameManager.InstanceGM.GetCorrectAnswer());

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
