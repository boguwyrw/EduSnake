using UnityEngine;

public class ParticleCollisionDetection : MonoBehaviour
{
    [SerializeField] private SnakeHeadMovement snakeHeadMovement;

    private void OnParticleCollision(GameObject other)
    {
        snakeHeadMovement.FireSparkCollision();
    }
}
