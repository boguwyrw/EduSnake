using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadTest : MonoBehaviour
{
    public float speed = 5f;
    public float rottationSpeed = 2000f;

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            rottationSpeed *= 2f;

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        float horizontalRotation = Input.GetAxis("Horizontal");
        /*
        float verticalRotation = Input.GetAxis("Vertical");
        Vector3 snakeDirection = new Vector3(horizontalRotation, 0.0f, verticalRotation);
        if (snakeDirection != Vector3.zero && rottationSpeed != 0.0f)
            transform.rotation = Quaternion.LookRotation(snakeDirection * Time.deltaTime * rottationSpeed);
        */
        transform.Rotate(Vector3.up, horizontalRotation * rottationSpeed * Time.deltaTime);
    }
}
