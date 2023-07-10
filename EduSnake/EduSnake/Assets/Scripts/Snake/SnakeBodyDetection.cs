using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyDetection : MonoBehaviour
{
    private SnakeCollisionDetection collisionDetection;

    private void Start()
    {
        collisionDetection = transform.parent.GetComponent<SnakeCollisionDetection>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            collisionDetection.RemoveSnakeBodyParts();
            Debug.Log("Body collision: " + collision.gameObject.name);
            GameManager.InstanceGM.StopGame();
        }
    }
}
