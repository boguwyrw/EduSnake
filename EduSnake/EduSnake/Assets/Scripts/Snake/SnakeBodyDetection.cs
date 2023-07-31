using System;
using System.Collections;
using UnityEngine;

public class SnakeBodyDetection : MonoBehaviour
{
    [SerializeField] private ParticleSystem answerEffect;
    [SerializeField] private GameObject[] snakeBodies;

    public event Action OnBodyColided;

    /// <summary>
    /// Method responsible for starting coroutine with particle effect after snake collide with answer
    /// </summary>
    private void ActivateAnswerCollisionEffect()
    {
        StartCoroutine(ActivateCollisionEffectDelay());
    }

    /// <summary>
    /// Coroutine responsible for activate particle effect and stopping snake movement
    /// </summary>
    /// <returns></returns>
    private IEnumerator ActivateCollisionEffectDelay()
    {
        GameManager.Instance.ActivateStopMovingSnakeHead();
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

    /// <summary>
    /// Method responsible for turning on snake body part from list after current part was hidden after collision
    /// </summary>
    public void ActivateSnakeBodyPart()
    {
        int snakeBodyIndex = UnityEngine.Random.Range(0, snakeBodies.Length);
        snakeBodies[snakeBodyIndex].SetActive(true);
    }
}
