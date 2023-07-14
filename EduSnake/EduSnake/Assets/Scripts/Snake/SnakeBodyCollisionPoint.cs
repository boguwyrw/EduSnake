using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyCollisionPoint : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private SphereCollider bodySphereCollider;

    [SerializeField] private ParticleSystem wrongAnswerEffectPrefab;

    private float radiusRange = 0.0f;

    private Vector3 collisionPoint;
    //public Vector3 CollisionPoint { get { return collisionPoint; } }

    private void Start()
    {
        radiusRange = bodySphereCollider.radius;
    }

    public void CalculateCollisionPoint()
    {
        wrongAnswerEffectPrefab.Play();
        /*
        Collider[] colliders = Physics.OverlapSphere(transform.position, radiusRange + 0.01f, layerMask);

        if (colliders.Length > 0)
        {
            wrongAnswerEffectPrefab.transform.position = colliders[0].transform.position;
            wrongAnswerEffectPrefab.gameObject.SetActive(true);
            wrongAnswerEffectPrefab.Play();
            if (wrongAnswerEffectPrefab.isStopped)
            {
                wrongAnswerEffectPrefab.gameObject.SetActive(false);
            }
        }
        */
    }
}
