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

    //private float bodyPartsOffset = 1.0f;

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
            SceneManager.LoadScene(0);
        }
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return (layerMask.value & (1 << obj.layer)) > 0;
    }
}
