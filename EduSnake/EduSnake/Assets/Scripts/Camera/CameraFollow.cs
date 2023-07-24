using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform snakeHead;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - snakeHead.position;
    }

    private void LateUpdate()
    {
        transform.position = snakeHead.position + offset;
    }
}
