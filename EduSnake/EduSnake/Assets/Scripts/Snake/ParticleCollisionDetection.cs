using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionDetection : MonoBehaviour
{
    [SerializeField] private SnakeHeadMovement snakeHeadMovement;

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("KOLIZJA");
        snakeHeadMovement.FireSparkCollision();
    }
}
