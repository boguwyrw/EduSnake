using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Answer : MonoBehaviour
{
    [SerializeField] private TMP_Text answerText;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
    }
}
