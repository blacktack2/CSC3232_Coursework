using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private Door _ToOpen;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody.gameObject.CompareTag("Player"))
        {
            _ToOpen.Open();
            Destroy(gameObject);
        }
    }
}
