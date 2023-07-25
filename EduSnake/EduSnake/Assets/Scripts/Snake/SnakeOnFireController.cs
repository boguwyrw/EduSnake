using UnityEngine;

public class SnakeOnFireController : MonoBehaviour
{
    [SerializeField] private Transform snakeHead;
    [SerializeField] private Transform snakeOnFireEffect;

    private Vector3 offset;

    private void Start()
    {
        offset = snakeOnFireEffect.position - snakeHead.position;
        TurnOffFireEffect();
    }

    private void LateUpdate()
    {
        snakeOnFireEffect.position = snakeHead.position + offset;
    }

    /// <summary>
    /// Method responsible for hidding snake on fire particle effect during game
    /// </summary>
    public void TurnOffFireEffect()
    {
        snakeOnFireEffect.gameObject.SetActive(false);
    }

    /// <summary>
    /// Method responsible for showing snake on fire particle effect during game
    /// </summary>
    public void TurnOnFireEffect()
    {
        snakeOnFireEffect.gameObject.SetActive(true);
    }
}
