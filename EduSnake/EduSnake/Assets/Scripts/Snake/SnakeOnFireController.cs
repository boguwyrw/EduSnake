using UnityEngine;

public class SnakeOnFireController : MonoBehaviour
{
    [SerializeField] private Transform snakeHead;
    [SerializeField] private Transform snakeOnFireEffect;

    private Vector3 offset;

    private void Start()
    {
        offset = snakeOnFireEffect.position - snakeHead.position;
        TurnOnOffFireEffect(false);
    }

    private void LateUpdate()
    {
        snakeOnFireEffect.position = snakeHead.position + offset;
    }

    /// <summary>
    /// Method responsible for showing and hidding snake on fire particle effect during game
    /// </summary>
    /// <param name="isOnOff"></param>
    public void TurnOnOffFireEffect(bool isOnOff)
    {
        snakeOnFireEffect.gameObject.SetActive(isOnOff);
    }
}
