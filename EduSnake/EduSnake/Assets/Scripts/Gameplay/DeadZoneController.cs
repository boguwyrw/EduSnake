using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    [SerializeField] private Transform snakeHead;

    private Transform deadZone;
    // wy�aczy� jako� deadzone �eby nie przestawia�o pozycji Answer

    private void Start()
    {
        deadZone = transform.GetChild(0);
    }

    private void LateUpdate()
    {
        deadZone.position = snakeHead.position;
    }
}
