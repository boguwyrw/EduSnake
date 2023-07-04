using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadMovement : MonoBehaviour
{
    private float slowTimeToMove = 1.0f;
    private float normalTimeToMove = 0.750f;
    private float fastTimeToMove = 0.50f;
    private float currentTimeToMove = 0.0f;
    private float actualTimeToMove = 0.0f;
    private float rotationAngle = 90.0f;

    private Vector3 previousPosition;
    public Vector3 PreviousPosition { get { return previousPosition; } }

    private Quaternion previousRotation;
    public Quaternion PreviousRotation { get { return previousRotation; } }

    private void Start()
    {
        currentTimeToMove = slowTimeToMove;
    }

    private void Update()
    {
        GridMovement();
        RotateSnakeHead();
    }

    private void GridMovement()
    {
        if (actualTimeToMove <= 0.0f)
        {
            previousPosition = transform.position;
            previousRotation = transform.rotation;
            transform.Translate(Vector3.forward);
            actualTimeToMove = currentTimeToMove;
        }

        actualTimeToMove -= Time.deltaTime;
    }

    private void TurningRight()
    {
        transform.Rotate(Vector3.up, rotationAngle);
    }

    private void TurningLeft()
    {
        transform.Rotate(Vector3.up, -rotationAngle);
    }

    private void RotateSnakeHead()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            TurningRight();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            TurningLeft();
        }
    }
}
