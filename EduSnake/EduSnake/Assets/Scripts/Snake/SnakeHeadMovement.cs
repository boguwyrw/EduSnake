using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadMovement : SnakeMovement
{
    private float rotationSpeed = 80.0f;
    private float clampValue = 0.082f;

    protected override void Start()
    {
        base.Start();
    }

    private void LateUpdate()
    {
        SnakePartsMovement();
        RotateSnakeHead();
    }

    private void RotateSnakeHead()
    {
        float rotationValue = Input.GetAxis("Horizontal");
        float angleValue = rotationValue * Time.deltaTime * rotationSpeed;
        float angleValueY = Mathf.Clamp(angleValue, -clampValue, clampValue);
        transform.Rotate(Vector3.up, angleValueY);
    }
}
