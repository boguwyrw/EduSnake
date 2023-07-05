using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadMovement : SnakeMovement
{
    [SerializeField] private Joystick horizontalJoystick;

    private float rotationSpeed = 300.0f;
    private float clampValue = 0.82f;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {      
        SnakePartsMovement();
        RotateSnakeHead();
    }

    private void RotateSnakeHead()
    {
        float rotationValue = Input.GetAxis("Horizontal");
        float angleValue = horizontalJoystick.Horizontal * Time.deltaTime * rotationSpeed;
        float angleValueY = Mathf.Clamp(angleValue, -clampValue, clampValue);
        transform.Rotate(Vector3.up, angleValueY);
    }
}
