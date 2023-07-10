using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyDetection : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            SnakeCollisionDetection.onBodyCollisionDetection?.Invoke();
        }
    }
}
