using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (IsInLayerMask(collision.gameObject, snakeLayers))
        {
            if (collision.gameObject.layer == 10)
            {
                mathTaskGenerator.ShowPlayerWrongChoose();
            }
        }

        if (collision.gameObject.layer == 9)
        {
            int lastSnakePartIndex = snakeParent.childCount - 1;
            Transform lastSnakePart = snakeParent.GetChild(lastSnakePartIndex);
            Instantiate(snakeBodyPrefab, lastSnakePart.position, Quaternion.identity, transform.parent);
            mathTaskGenerator.ShowPlayerCorrectChoose();
        }
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return (layerMask.value & (1 << obj.layer)) > 0;
    }
}
