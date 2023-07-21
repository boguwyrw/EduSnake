using System.Collections;
using System.Collections.Generic;
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

    public void TurnOffFireEffect()
    {
        snakeOnFireEffect.gameObject.SetActive(false);
    }

    public void TurnOnFireEffect()
    {
        snakeOnFireEffect.gameObject.SetActive(true);
    }
}
