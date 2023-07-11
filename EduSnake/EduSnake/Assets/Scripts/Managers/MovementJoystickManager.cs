using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementJoystickManager : MonoBehaviour
{
    [SerializeField] private RectTransform movementRect;

    private float rectWidth = 0.0f;
    private float rectHeight = 0.0f;
    private float minAnchorValue = 1.0f; /* right side 1.0f */ /* left side 0.0f */
    private float maxAnchorValue = 1.0f; /* right side 1.0f */ /* left side 0.0f */
    private float joystickSideValue = -1.0f; /* right side -1.0f */ /* left side 1.0f */

    private void Start()
    {
        rectWidth = movementRect.rect.width;
        rectHeight = movementRect.rect.height;

        JoystickSide(minAnchorValue, maxAnchorValue, joystickSideValue);
    }

    private void Update()
    {
        
    }

    private void JoystickSide(float minAnchor, float maxAnchor, float joystickSide)
    {
        movementRect.anchorMin = new Vector2(minAnchor, 0.0f);
        movementRect.anchorMax = new Vector2(maxAnchor, 0.0f);
        movementRect.anchoredPosition = new Vector3(joystickSide * rectWidth, rectHeight, 0.0f);
    }

    public void RightSideJoystick()
    {
        minAnchorValue = 1.0f;
        maxAnchorValue = 1.0f;
        joystickSideValue = -1.0f;
        JoystickSide(minAnchorValue, maxAnchorValue, joystickSideValue);
    }

    public void LeftSideJoystick()
    {
        minAnchorValue = 0.0f;
        maxAnchorValue = 0.0f;
        joystickSideValue = 1.0f;
        JoystickSide(minAnchorValue, maxAnchorValue, joystickSideValue);
    }
}
