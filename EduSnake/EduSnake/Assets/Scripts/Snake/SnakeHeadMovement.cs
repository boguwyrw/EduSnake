using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadMovement : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody snakeHeadRigidbody;

    private Transform snakeParent;
    private Transform currentBodyPart;
    private Transform previousBodyPart;

    private Vector3 headStartPosition;
    private Vector3 snakeDirection;

    private float rotationSpeed = 0.0f;
    private float maxRotationSpeed = 450.0f;

    private float slowMovement = 3.0f;
    private float normalMovement = 6.0f;
    private float fastMovement = 9.0f;
    private float currentMovement = 0.0f;
    private float lastMovementSpeed = 0.0f;

    private void Start()
    {
        snakeParent = transform.parent;
        headStartPosition = transform.position;
        rotationSpeed = maxRotationSpeed;
    }

    private void Update()
    {
        RotateSnakeHead();
        BodyPartsMovement();
    }

    private void LateUpdate()
    {
        SnakeHeadMove();
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
                currentBodyPart.rotation = Quaternion.Slerp(currentBodyPart.rotation, previousBodyPart.rotation, Time.deltaTime * currentMovement);
            }
        }
    }

    public void StartMovingSnakeHead()
    {
        currentMovement = slowMovement;
    }

    public void StopMovingSnakeHead()
    {
        lastMovementSpeed = currentMovement;
        currentMovement = 0.0f;
        rotationSpeed = 0.0f;
    }

    public void ResumeMovingSnakeHead()
    {
        transform.position = headStartPosition;
        currentMovement = slowMovement;
        rotationSpeed = maxRotationSpeed;
    }

    public void AssignNormalSpeed()
    {
        currentMovement = normalMovement;
    }

    public void AssignFastSpeed()
    {
        currentMovement = fastMovement;
    }
}
