using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public Image BGImage;
    public static BackgroundController Instance;

    private void Awake()
    {
        if (Instance != this) Destroy(Instance);
        Instance = this;
    }

    public void ChangeBackground(Sprite image, Color color)
    {
        BGImage.sprite = image;
        BGImage.color = color;
    }
}
