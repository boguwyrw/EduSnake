using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCollisionDetection : MonoBehaviour
{
    [SerializeField] private GameObject snakeBodyPrefab;
    [SerializeField] private LayerMask snakeLayers;

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
        //if (collision.gameObject.layer == 7 || collision.gameObject.layer == 8)
        //if (collision.gameObject.layer == snakeLayers)
        if (IsInLayerMask(collision.gameObject, snakeLayers))
        {
            Debug.Log(collision.gameObject.name);
        }

        if (collision.gameObject.layer == 9)
        {
            int lastSnakePartIndex = snakeParent.childCount - 1;
            Transform lastSnakePart = snakeParent.GetChild(lastSnakePartIndex);
            Instantiate(snakeBodyPrefab, lastSnakePart.position, Quaternion.identity, transform.parent);
        }
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << obj.layer)) > 0);
    }
}
