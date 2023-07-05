using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadMovement : SnakeMovement
{
    [SerializeField] private Joystick horizontalJoystick;

    private float rotationSpeed = 450.0f;
    private float clampValue = 1.1f;

    private void Start()
    {
        
    }

    private void Update()
    {      
        SnakePartsMovement();
        RotateSnakeHead();
    }

    private void RotateSnakeHead()
    {
        float angleValue = horizontalJoystick.Horizontal * Time.deltaTime * rotationSpeed;
        float angleValueY = Mathf.Clamp(angleValue, -clampValue, clampValue);
        transform.Rotate(Vector3.up, angleValueY);
    }
}
