using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    protected float slowMovement = 1.0f;
    protected float normalMovement = 2.0f;
    protected float fastMovement = 4.0f;
    protected float currentMovement = 0.0f;

    protected virtual void Start()
    {
        currentMovement = slowMovement;
    }

    protected void HeadMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * currentMovement);
    }
}
