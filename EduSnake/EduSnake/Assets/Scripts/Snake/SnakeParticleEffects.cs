using UnityEngine;

public class SnakeParticleEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem wrongAnswerEffect;
    [SerializeField] private ParticleSystem correctAnswerEffect;

    private void Start()
    {
        wrongAnswerEffect.gameObject.SetActive(false);
    }

    /// <summary>
    /// Method responsible for activation particle effect after collision with obstacle
    /// </summary>
    public void ActivateCollisionParticleEffect()
    {
        wrongAnswerEffect.gameObject.SetActive(true);
        wrongAnswerEffect.Play();
        if (wrongAnswerEffect.isStopped)
        {
            wrongAnswerEffect.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Method responsible for activation particle effect after collision with correct answer
    /// </summary>
    /// <param name="answerPosition"></param>
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

    /// <summary>
    /// Method responsible for returning information about particle effect stop
    /// </summary>
    /// <returns></returns>
    public bool GetWrongParticleEffectStopped()
    {
        return wrongAnswerEffect.isStopped;
    }
}
