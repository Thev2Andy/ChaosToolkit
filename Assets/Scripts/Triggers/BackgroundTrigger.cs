using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTrigger : MonoBehaviour
{
    public Sprite BGImage;
    public Color BGColor;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            BackgroundController.Instance.ChangeBackground(BGImage, BGColor);
        }
    }
}
