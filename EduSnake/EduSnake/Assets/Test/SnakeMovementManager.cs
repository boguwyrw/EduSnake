using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovementManager : MonoBehaviour
{
    [SerializeField] private float distanceBetween = 0.25f;
    [SerializeField] private GameObject bodyPrefab;

    private float snakSpeed = 200.0f;
    private float rotationSpeed = 200.0f;
    private float rotationValue = 0.0f;
    private float countUp = 0.0f;

    private List<GameObject> snakeBodyParts = new List<GameObject>();

    private Rigidbody snakeRig;

    private void Start()
    {
        snakeBodyParts.Add(transform.GetChild(0).gameObject);
        snakeRig = snakeBodyParts[0].GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rotationValue = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        SnakeMove();
        CreateSnakeBodyPart();
    }

    private void SnakeMove()
    {
        snakeRig.velocity = snakeBodyParts[0].transform.forward * Time.deltaTime * snakSpeed;
        snakeBodyParts[0].transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * rotationValue);

        SnakeBodyMove();
    }

    private void SnakeBodyMove()
    {
        if (snakeBodyParts.Count > 1)
        {
            for (int i = 1; i < snakeBodyParts.Count; i++)
            {
                PointsPathManager pointsPathBody = snakeBodyParts[i - 1].GetComponent<PointsPathManager>();
                snakeBodyParts[i].transform.position = pointsPathBody.pointsMakers[0].pointPosition;
                snakeBodyParts[i].transform.rotation = pointsPathBody.pointsMakers[0].pointRotation;
                pointsPathBody.pointsMakers.RemoveAt(0);
            }
        }
    }

    private void CreateSnakeBodyPart()
    {
        PointsPathManager pointsPath = snakeBodyParts[0].GetComponent<PointsPathManager>();

        if (countUp == 0.0f)
        {
            pointsPath.ClearPointsList();
        }

        countUp += Time.deltaTime;

        if (countUp >= distanceBetween && snakeBodyParts.Count < 20)
        {
            GameObject bodyClone = Instantiate(bodyPrefab, pointsPath.transform.position, pointsPath.transform.rotation, transform);
            snakeBodyParts.Add(bodyClone);
            PointsPathManager pointsPathManager = bodyClone.GetComponent<PointsPathManager>();
            pointsPathManager.ClearPointsList();
            countUp = 0.0f;
        }
    }
}
