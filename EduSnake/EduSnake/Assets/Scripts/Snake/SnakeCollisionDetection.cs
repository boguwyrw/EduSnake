using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeCollisionDetection : MonoBehaviour
{
    [SerializeField] private GameObject snakeBodyPrefab;
    [SerializeField] private LayerMask snakeLayers;
    [SerializeField] private MathTaskGenerator mathTaskGenerator;

    private Transform snakeParent;

    private void Start()
    {
        snakeParent = transform.parent;
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            int lastSnakePartIndex = snakeParent.childCount - 1;
            Transform lastSnakePart = snakeParent.GetChild(lastSnakePartIndex).GetChild(0);
            Vector3 lastSnakePartPosition = new Vector3(lastSnakePart.position.x, lastSnakePart.position.y, lastSnakePart.position.z);
            Instantiate(snakeBodyPrefab, lastSnakePartPosition, Quaternion.identity, snakeParent);
            mathTaskGenerator.ShowPlayerCorrectChoose();
            GameManager.InstanceGM.AssignSnakePoints();
        }

        if (collision.gameObject.layer == 10)
        {
            mathTaskGenerator.ShowPlayerWrongChoose();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsInLayerMask(other.gameObject, snakeLayers))
        {
            RemoveSnakeBodyParts();
            GameManager.InstanceGM.StopGame();
        }
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return (layerMask.value & (1 << obj.layer)) > 0;
    }

    private void RemoveSnakeBodyParts()
    {
        for (int i = 1; i < snakeParent.childCount; i++)
        {
            snakeParent.GetChild(i).gameObject.SetActive(false);
        }
    }
}
