using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCollisionDetection : MonoBehaviour
{
    [SerializeField] private GameObject snakeBodyPrefab;
    [SerializeField] private LayerMask snakeLayers;
    [SerializeField] private MathTaskGenerator mathTaskGenerator;
    [SerializeField] private SnakeParticleEffects snakeParticleEffects;

    private Transform snakeParent;

    private Queue<GameObject> snakePoolQueue = new Queue<GameObject>();

    private void Start()
    {
        snakeParent = transform.parent;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Vector3 collisionPosition = collision.gameObject.transform.position;
            GameManager.InstanceGM.ActivateParticleEffect(collisionPosition);

            int numberOfBodyParts = transform.parent.childCount;

            if (snakePoolQueue.Count > 0 && snakePoolQueue.Count > numberOfBodyParts)
            {
                GameObject snakeBody = snakePoolQueue.Dequeue();
                snakeBody.SetActive(true);
            }
            else
            {
                int lastSnakePartIndex = snakeParent.childCount - 1;
                Transform lastSnakePart = snakeParent.GetChild(lastSnakePartIndex).GetChild(0);
                Vector3 lastSnakePartPosition = new Vector3(lastSnakePart.position.x, lastSnakePart.position.y, lastSnakePart.position.z);
                GameObject snakeBodyClone = Instantiate(snakeBodyPrefab, lastSnakePartPosition, Quaternion.identity, snakeParent);
                GameManager.InstanceGM.GetAllSnakeParts();
                snakePoolQueue.Enqueue(snakeBodyClone);
                SnakeBodyDetection snakeBodyDetection = snakeBodyClone.GetComponent<SnakeBodyDetection>();
                snakeBodyDetection.ActivateSnakeBodyPart();
                snakeBodyDetection.BodyColided += RemoveSnakeBodyParts;
            }

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
            snakeParticleEffects.ActivateWrongParticleEffect();
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

        GameManager.InstanceGM.StopGame();
    }
}
