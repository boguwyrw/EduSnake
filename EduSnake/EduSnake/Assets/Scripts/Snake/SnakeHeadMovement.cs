using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadMovement : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody snakeHeadRigidbody;

    [SerializeField] private float slowMovement = 3.0f;
    [SerializeField] private float fastMovement = 9.0f;

    private float currentMovement = 0.0f;
    private float movementSpeedInterval = 0.0f;
    private float speedIncreaseValue = 0.0f;
    private float rotationSpeed = 0.0f;
    private float maxRotationSpeed = 450.0f;

    private Transform snakeParent;
    private Transform currentBodyPart;
    private Transform previousBodyPart;

    private Vector3 headStartPosition;
    private Vector3 snakeDirection;

    private void Start()
    {
        movementSpeedInterval = (slowMovement + fastMovement) / 2.0f;
        snakeParent = transform.parent;
        headStartPosition = transform.position;
        rotationSpeed = maxRotationSpeed;
    }

    private void FixedUpdate()
    {
        SnakeHeadMove();
        RotateSnakeHead();
        BodyPartsMovement();
    }

    private void LateUpdate()
    {
        
    }

    private void SnakeHeadMove()
    {
        snakeDirection = new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical);
        transform.Translate(Vector3.forward * Time.deltaTime * currentMovement);
    }

    private void RotateSnakeHead()
    {
        if (snakeDirection != Vector3.zero && rotationSpeed != 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(snakeDirection * Time.deltaTime * rotationSpeed);
        }
    }

    private void BodyPartsMovement()
    {
        if (snakeParent.childCount > 1 && currentMovement > 0.0f)
        {
            for (int i = 1; i < snakeParent.childCount; i++)
            {
                currentBodyPart = snakeParent.GetChild(i);
                previousBodyPart = snakeParent.GetChild(i - 1);

                Vector3 newPosForBody = previousBodyPart.position;

                currentBodyPart.position = Vector3.Slerp(currentBodyPart.position, newPosForBody, Time.deltaTime * currentMovement);
                Vector3 directionToBodyPart = (newPosForBody - currentBodyPart.position).normalized;
                Quaternion rotationToBodyPart = Quaternion.LookRotation(directionToBodyPart);
                currentBodyPart.rotation = Quaternion.Slerp(currentBodyPart.rotation, rotationToBodyPart, Time.deltaTime * currentMovement);
            }
        }
    }

    public void StartMovingSnakeHead()
    {
        currentMovement = slowMovement;
    }

    public void StopMovingSnakeHead()
    {
        currentMovement = 0.0f;
        rotationSpeed = 0.0f;
    }

    public void ResumeMovingSnakeHead()
    {
        transform.position = headStartPosition;
        currentMovement = slowMovement;
        rotationSpeed = maxRotationSpeed;
    }

    public void AssignNextSpeed()
    {
        speedIncreaseValue = movementSpeedInterval / (float)GameManager.InstanceGM.GetMaxTasksNumber();
        currentMovement = currentMovement + speedIncreaseValue;
    }
}
