using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerArrow : MonoBehaviour
{
    [SerializeField] private GameObject pointerArrow;
    [SerializeField] private GameObject destinationPoint;

    private Transform correctAnswerTransform;

    private float minDistanceToAnswer = 1.8f;

    private void Start()
    {
        pointerArrow.SetActive(false);
        destinationPoint.SetActive(false);
    }

    private void LateUpdate()
    {
        if (GameManager.InstanceGM.GetCorrectAnswer() != null && GameManager.InstanceGM.GetAreAnswersSpawned())
        {
            Vector3 startPosition = new Vector3(transform.parent.position.x, GameManager.InstanceGM.GetCorrectAnswer().position.y, transform.parent.position.z);
            transform.position = Vector3.Lerp(startPosition, GameManager.InstanceGM.GetCorrectAnswer().position, 0.35f);
            //destinationPoint.transform.position = Vector3.Lerp(startPosition, GameManager.InstanceGM.GetCorrectAnswer().position, 0.5f);

            transform.LookAt(GameManager.InstanceGM.GetCorrectAnswer());

            float distanceToAnswer = Vector3.Distance(transform.position, GameManager.InstanceGM.GetCorrectAnswer().position);
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
