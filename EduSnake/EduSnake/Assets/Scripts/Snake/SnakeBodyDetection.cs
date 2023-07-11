using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyDetection : MonoBehaviour
{
    public event Action BodyColided;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            BodyColided?.Invoke();
        }
    }
}
