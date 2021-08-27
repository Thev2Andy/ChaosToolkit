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
        Instance = this;
        if (Instance != this && Instance != null) Destroy(Instance);
    }

    public void ChangeBackground(Sprite image, Color color)
    {
        BGImage.sprite = image;
        BGImage.color = color;
    }
}
