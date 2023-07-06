using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyMovement : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            GameManager.InstanceGM.StopGame();
        }
    }
}
