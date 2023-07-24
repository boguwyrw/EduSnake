using System;
using System.Collections;
using UnityEngine;

public class SnakeBodyDetection : MonoBehaviour
{
    [SerializeField] private ParticleSystem answerEffect;
    [SerializeField] private GameObject[] snakeBodies;

    public event Action OnBodyColided;

    private void ActivateAnswerCollisionEffect()
    {
        StartCoroutine(ActivateCollisionEffectDelay());
    }

    private IEnumerator ActivateCollisionEffectDelay()
    {
        GameManager.InstanceGM.ActivateStopMovingSnakeHead();
        answerEffect.Play();
        yield return new WaitUntil(() => answerEffect.isStopped);
        OnBodyColided?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            ActivateAnswerCollisionEffect();
        }
    }

    public void ActivateSnakeBodyPart()
    {
        int snakeBodyIndex = UnityEngine.Random.Range(0, snakeBodies.Length);
        snakeBodies[snakeBodyIndex].SetActive(true);
    }
}
