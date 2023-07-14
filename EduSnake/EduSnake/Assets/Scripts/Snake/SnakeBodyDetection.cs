using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyDetection : MonoBehaviour
{
    [SerializeField] private SnakeBodyCollisionPoint snakeBodyCollisionPoint;

    public event Action BodyColided;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            snakeBodyCollisionPoint.CalculateCollisionPoint();
            //GameManager.InstanceGM.SetPositionForWrongParticleEffect(snakeBodyCollisionPoint.CollisionPoint);
            BodyColided?.Invoke();
        }
    }
}
