using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadMovement : SnakeMovement
{
    [SerializeField] private Joystick horizontalJoystick;


    private Transform snakeParent;
    private Transform currentBodyPart;
    private Transform previousBodyPart;

    private float rotationSpeed = 450.0f;
    private float clampValue = 1.1f;
    private float bodyPartsDistance = 0.0f;
    private float minDistance = 0.3f;

    private void Start()
    {
        snakeParent = transform.parent;
    }

    private void Update()
    {      
        SnakePartsMovement();
        RotateSnakeHead();
        BodyPartsMovement();
    }

    private void RotateSnakeHead()
    {
        float angleValue = horizontalJoystick.Horizontal * Time.deltaTime * rotationSpeed;
        float angleValueY = Mathf.Clamp(angleValue, -clampValue, clampValue);
        transform.Rotate(Vector3.up, angleValueY);
    }

    private void BodyPartsMovement()
    {
        if (snakeParent.childCount > 1)
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
}
