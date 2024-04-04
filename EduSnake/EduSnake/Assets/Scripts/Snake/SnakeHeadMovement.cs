using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadMovement : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody snakeHeadRigidbody;
    [SerializeField] private GameObject directionArrow;

    [SerializeField] private float slowMovement = 3.2f;
    [SerializeField] private float fastMovement = 8.0f;

    private float currentMovement = 0.0f;
    private float movementSpeedInterval = 0.0f;
    private float speedIncreaseValue = 0.0f;
    private float superSpeedIncreaseValue = 1.35f;
    private float superSpeedTime = 3.0f;
    private float rotationSpeed = 0.0f;
    private float maxRotationSpeed = 260.0f; // 360

    private Transform snakeParent;
    private Transform currentBodyPart;
    private Transform previousBodyPart;

    private Vector3 snakeDirection;

    private void Start()
    {
        movementSpeedInterval = (slowMovement + fastMovement) / 2.0f;
        snakeParent = transform.parent;
        rotationSpeed = maxRotationSpeed;

        HideDirectionArrow();
    }

    private void FixedUpdate()
    {
        SnakeHeadMove();
        RotateSnakeHead();
        BodyPartsMovement();
    }

    /// <summary>
    /// Method responsible for snake head movement functionality
    /// </summary>
    private void SnakeHeadMove()
    {
        Vector3 joystickDirection = new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical);
        snakeDirection = (joystickDirection + transform.forward * Time.deltaTime * rotationSpeed).normalized;
        transform.Translate(Vector3.forward * Time.deltaTime * currentMovement);
    }

    /// <summary>
    /// Method responsible for snake head rotation functionality
    /// </summary>
    private void RotateSnakeHead()
    {
        if (snakeDirection != Vector3.zero && rotationSpeed != 0.0f)
        {
            Quaternion rotationTarget = Quaternion.LookRotation(snakeDirection);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(snakeDirection);
        }
    }

    /// <summary>
    /// Method responsible for following movement of snake body parts
    /// </summary>
    private void BodyPartsMovement()
    {
        if (snakeParent.childCount > 1 && currentMovement > 0.0f)
        {
            for (int i = 1; i < snakeParent.childCount; i++)
            {
                currentBodyPart = snakeParent.GetChild(i);
                previousBodyPart = snakeParent.GetChild(i - 1);

                Vector3 newPosForBody = previousBodyPart.position;

                currentBodyPart.position = Vector3.Slerp(currentBodyPart.position, newPosForBody, Time.deltaTime * currentMovement);
                Vector3 directionToBodyPart = (newPosForBody - currentBodyPart.position).normalized;
                Quaternion rotationToBodyPart = Quaternion.LookRotation(directionToBodyPart);
                currentBodyPart.rotation = Quaternion.Slerp(currentBodyPart.rotation, rotationToBodyPart, Time.deltaTime * currentMovement);
            }
        }
    }

    /// <summary>
    /// Coroutine responsible for activate fire spark (snake on fire) effect and temporary increasing snake speed
    /// </summary>
    /// <returns></returns>
    private IEnumerator FireSparkCollisionDelay()
    {
        float lastCurrentMovement = currentMovement;
        GameManager.Instance.TurnOnSnakeOnFireEffect();
        currentMovement *= superSpeedIncreaseValue;
        yield return new WaitForSeconds(superSpeedTime);
        currentMovement = lastCurrentMovement;
        GameManager.Instance.TurnOffSnakeOnFireEffect();
    }

    /// <summary>
    /// Method responsible for assigning first snake movement speed
    /// </summary>
    public void StartMovingSnakeHead()
    {
        currentMovement = slowMovement;
    }

    /// <summary>
    /// Method responsible for assigning speed and rotation values on 0 and stop snake
    /// </summary>
    public void StopMovingSnakeHead()
    {
        currentMovement = 0.0f;
        rotationSpeed = 0.0f;
    }

    /// <summary>
    /// Method responsible for increasing snake movement speed
    /// </summary>
    public void AssignNextSpeed()
    {
        speedIncreaseValue = movementSpeedInterval / (float)GameManager.Instance.GetMaxTasksNumber();
        currentMovement = currentMovement + speedIncreaseValue;
    }

    /// <summary>
    /// Method responsible for activate coroutine with particle effect collision
    /// </summary>
    public void FireSparkCollision()
    {
        StartCoroutine(FireSparkCollisionDelay());
    }

    /// <summary>
    /// Method responsible for hidding direction arrow during game
    /// </summary>
    public void HideDirectionArrow()
    {
        directionArrow.SetActive(false);
    }

    /// <summary>
    /// Method responsible for showing direction arrow during game
    /// </summary>
    public void ShowDirectionArrow()
    {
        directionArrow.SetActive(true);
    }

    /// <summary>
    /// Method responsible for returning transform from all snake body parts
    /// </summary>
    /// <returns></returns>
    public List<Transform> AllSnakeParts()
    {
        List<Transform> snakeParts = new List<Transform>();
        snakeParts.Clear();
        for (int i = 0; i < snakeParent.childCount; i++)
        {
            snakeParts.Add(snakeParent.GetChild(i));
        }
        return snakeParts;
    }
}
