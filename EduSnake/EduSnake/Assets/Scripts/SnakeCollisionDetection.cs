using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCollisionDetection : MonoBehaviour
{
    [SerializeField] private GameObject snakeBodyPrefab;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7 || collision.gameObject.layer == 8)
        {

        }

        if (collision.gameObject.layer == 9)
        {
            Instantiate(snakeBodyPrefab);
        }
    }
}
