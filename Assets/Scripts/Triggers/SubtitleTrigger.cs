using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleTrigger : MonoBehaviour
{
    public string SubtitleText;
    public float SubtitleDuration;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            SubtitleController.Instance.Show(SubtitleText, SubtitleDuration);
            Destroy(gameObject);
        }
    }
}
