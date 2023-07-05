using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    protected float slowMovement = 2.0f;
    protected float normalMovement = 4.0f;
    protected float fastMovement = 6.0f;
    protected float currentMovement = 0.0f;

    protected virtual void Start()
    {
        currentMovement = fastMovement;
    }

    protected void SnakePartsMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * currentMovement);
    }
}
