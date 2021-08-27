using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIController : MonoBehaviour
{
    public TMP_Text MessageText;
    float messageTimer;

    public static GameUIController Instance;

    private void Awake()
    {
       if (Instance != this && Instance != null) Destroy(Instance);
       Instance = this;
    }

    public void ShowMessage(string message, float duration)
    {
        MessageText.text = message;
        messageTimer = duration;
    }

    private void Update()
    {
       messageTimer -= Time.deltaTime;
       if(messageTimer < 0) messageTimer = 0;
       if(messageTimer <= 0) MessageText.text = "";
    }

}
