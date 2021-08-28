using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public UnityEvent Event;
    public bool OneUseOnly;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            Event.Invoke();
            if (OneUseOnly) Destroy(gameObject);
        }
    }
}
