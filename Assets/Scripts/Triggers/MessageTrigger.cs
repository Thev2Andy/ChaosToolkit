using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
    public string MessageText;
    public float MessageDuration;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            GameUIController.Instance.ShowMessage(MessageText, MessageDuration);
            Destroy(gameObject);
        }
    }
}
