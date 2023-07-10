using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SnakeCollisionDetection : MonoBehaviour
{
    [SerializeField] private GameObject snakeBodyPrefab;
    [SerializeField] private LayerMask snakeLayers;
    [SerializeField] private MathTaskGenerator mathTaskGenerator;

    private Transform snakeParent;

    private List<GameObject> snakePool = new List<GameObject>();
    private int poolIndex = 0;

    public delegate void OnBodyCollisionDetection();
    public static OnBodyCollisionDetection onBodyCollisionDetection;

    private void Start()
    {
        snakeParent = transform.parent;
        onBodyCollisionDetection = RemoveSnakeBodyParts;
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (snakePool.Count > 0 && snakePool.All(s => !s.activeSelf))
            {
                poolIndex = 0;
            }

            if (snakePool.Count > 0 && snakePool.Any(s => !s.activeSelf))
            {
                snakePool[poolIndex].SetActive(true);
                poolIndex++;
            }
            else
            {
                poolIndex = 0;
                int lastSnakePartIndex = snakeParent.childCount - 1;
                Transform lastSnakePart = snakeParent.GetChild(lastSnakePartIndex).GetChild(0);
                Vector3 lastSnakePartPosition = new Vector3(lastSnakePart.position.x, lastSnakePart.position.y, lastSnakePart.position.z);
                GameObject snakeBodyClone = Instantiate(snakeBodyPrefab, lastSnakePartPosition, Quaternion.identity, snakeParent);
                snakePool.Add(snakeBodyClone);
            }

            GameManager.InstanceGM.AssignSnakePoints();
            mathTaskGenerator.ShowPlayerCorrectChoose();
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
        if (snakeParent == null)
        {
            snakeParent = transform.parent;
        }
       
        for (int i = 1; i < snakeParent.childCount; i++)
        {
            snakeParent.GetChild(i).gameObject.SetActive(false);
        }

        for (int j = 0; j < snakePool.Count; j++)
        {
            snakePool[j].SetActive(false);
        }

        GameManager.InstanceGM.StopGame();
    }
}
