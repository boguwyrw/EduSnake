using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    [SerializeField] private Transform snakeHead;

    private Transform deadZone;
    // wy³aczyæ jakoœ deadzone ¿eby nie przestawia³o pozycji Answer

    private void Start()
    {
        deadZone = transform.GetChild(0);
    }

    private void LateUpdate()
    {
        deadZone.position = snakeHead.position;
    }
}
