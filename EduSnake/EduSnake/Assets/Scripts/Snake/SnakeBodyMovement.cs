using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyMovement : SnakeMovement
{
    [SerializeField] private SphereCollider snakeBodyCollider;

    private Transform snakeParent;
    private Transform previousPart;

    /*
    private void Start()
    {
        snakeParent = transform.parent;
        previousPart = snakeParent.GetChild(transform.GetSiblingIndex() - 1);
        StartGame();
    }

    private void Update()
    {
        float partsDistance = Vector3.Distance(transform.position, previousPart.position);
        if (partsDistance >= 0.995f)
        {
            if (snakeBodyCollider.isTrigger)
            {
                snakeBodyCollider.isTrigger = false;
            }
            SnakePartsMovement();
        }

        transform.LookAt(previousPart);
    }
    */
}
