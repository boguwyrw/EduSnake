using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadMovement : MonoBehaviour
{
    [SerializeField] private Joystick horizontalJoystick;
    [SerializeField] private Rigidbody snakeHeadRigidbody;

    private Transform snakeParent;
    private Transform currentBodyPart;
    private Transform previousBodyPart;

    private float rotationSpeed = 450.0f;
    private float clampValue = 1.1f;
    private float bodyPartsDistance = 0.0f;
    private float minDistance = 0.3f;

    private float slowMovement = 2.0f;
    private float normalMovement = 4.0f;
    private float fastMovement = 6.0f;
    private float currentMovement = 0.0f;

    private void Start()
    {
        snakeParent = transform.parent;
    }

    private void Update()
    {
        RotateSnakeHead();
        BodyPartsMovement();
    }

    private void FixedUpdate()
    {
        SnakeHeadMove(); 
    }

    private void SnakeHeadMove()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * currentMovement);
    }

    private void RotateSnakeHead()
    {
        float angleValue = horizontalJoystick.Horizontal * Time.deltaTime * rotationSpeed;
        float angleValueY = Mathf.Clamp(angleValue, -clampValue, clampValue);
        transform.Rotate(Vector3.up, angleValueY);
    }

    private void BodyPartsMovement()
    {
        if (snakeParent.childCount > 1 && currentMovement > 0.0f)
        {
            for (int i = 1; i < snakeParent.childCount; i++)
            {
                currentBodyPart = snakeParent.GetChild(i);
                previousBodyPart = snakeParent.GetChild(i - 1);

                bodyPartsDistance = Vector3.Distance(previousBodyPart.position, currentBodyPart.position);
                Vector3 newPosForBody = previousBodyPart.position;

                float timeValue = (Time.deltaTime * bodyPartsDistance) / (minDistance * currentMovement);

                currentBodyPart.position = Vector3.Slerp(currentBodyPart.position, newPosForBody, timeValue);
                currentBodyPart.rotation = Quaternion.Slerp(currentBodyPart.rotation, previousBodyPart.rotation, timeValue);
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
    }
}
