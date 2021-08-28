using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.gameObject.CompareTag("Player")) Destroy(c.gameObject);
    }
}
