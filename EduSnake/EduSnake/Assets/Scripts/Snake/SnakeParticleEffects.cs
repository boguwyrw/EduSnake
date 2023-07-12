using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeParticleEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem wrongAnswerEffect;

    private void Start()
    {
        wrongAnswerEffect.gameObject.SetActive(false);
    }

    public void ActivateWrongParticleEffect()
    {
        wrongAnswerEffect.gameObject.SetActive(true);
        if (wrongAnswerEffect.isStopped)
        {
            wrongAnswerEffect.gameObject.SetActive(false);
        }
    }

    public void DeactivateWrongParticleEffect()
    {
        wrongAnswerEffect.gameObject.SetActive(false);
    }

    public bool GetWrongParticleEffectStopped()
    {
        return wrongAnswerEffect.isStopped;
    }
}
