using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyTest : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        //transform.Translate(direction * speed * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Quaternion rotationToTarget = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, speed * 2.0f * Time.deltaTime);
        //transform.rotation = rotationToTarget;
    }
}
