using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyMovement : SnakeMovement
{
    private Transform snakeParent;
    private Transform previousPart;

    protected override void Start()
    {
        base.Start();
        snakeParent = transform.parent;
        previousPart = snakeParent.GetChild(transform.GetSiblingIndex() - 1);
    }

    private void Update()
    {
        float partsDistance = Vector3.Distance(transform.position, previousPart.position);
        if (partsDistance >= 0.995f)
        {
            SnakePartsMovement();
        }

        transform.LookAt(previousPart);
    }
}
