using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleController : MonoBehaviour
{
    public TMP_Text SubtitleText;
    public HealthSystem HS;
    public static SubtitleController Instance;

    // Private variables...
    float timer;

    private void Awake()
    {
       if (Instance != this) Destroy(Instance);
       Instance = this;
    }

    // Update is called once per frame
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0) timer = 0;

        if (timer <= 0 || HS.Dead) SubtitleText.text = "";
    }

    public void Show(string text, float time)
    {
        SubtitleText.text = text;
        timer = time;
    }
}
