using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeParticleEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem wrongAnswerEffect;
    [SerializeField] private ParticleSystem correctAnswerEffect;

    private void Start()
    {
        wrongAnswerEffect.gameObject.SetActive(false);
    }

    public void SetNewPositionForWrongParticleEffect(Vector3 actualPosition)
    {
        wrongAnswerEffect.transform.position = actualPosition;
    }

    public void ActivateWrongParticleEffect()
    {
        wrongAnswerEffect.gameObject.SetActive(true);
        wrongAnswerEffect.Play();
        if (wrongAnswerEffect.isStopped)
        {
            wrongAnswerEffect.gameObject.SetActive(false);
        }
    }

    public void ActivateCorrectParticleEffect(Vector3 answerPosition)
    {
        correctAnswerEffect.transform.position = answerPosition;
        correctAnswerEffect.gameObject.SetActive(true);
        correctAnswerEffect.Play();
        if (correctAnswerEffect.isStopped)
        {
            correctAnswerEffect.gameObject.SetActive(false);
        }
    }

    public bool GetWrongParticleEffectStopped()
    {
        return wrongAnswerEffect.isStopped;
    }
}
