using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyMovement : MonoBehaviour
{
    private SnakeHeadMovement snakeHeadMovement;

    private void Start()
    {
        Transform snakeParent = transform.parent;
        snakeHeadMovement = snakeParent.GetChild(0).GetComponent<SnakeHeadMovement>();
    }

    private void LateUpdate()
    {
        transform.position = snakeHeadMovement.PreviousPosition;
        transform.rotation = snakeHeadMovement.PreviousRotation;
    }
}
