using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionsUIEvents : MonoBehaviour
{
    public TMP_Text GFXText;
    public TMP_Text FullscreenText;
    public TMP_Text SFXText;

    private void Start()
    {
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("GFX", 2), true);
        AudioListener.volume = PlayerPrefs.GetFloat("SFX", 1f);

        GFXText.text = ("Graphics: " + QualitySettings.names[QualitySettings.GetQualityLevel()]);
        FullscreenText.text = ("Fullscreen: " + (Screen.fullScreen ? "On" : "Off"));
        SFXText.text = ("Audio: " + ((AudioListener.volume == 0f) ? "Off" : "On"));
    }

    public void ChangeGFX()
    {
        int OldQualityIndex = QualitySettings.GetQualityLevel();
        QualitySettings.IncreaseLevel(true);
        if(OldQualityIndex == QualitySettings.GetQualityLevel())
        {
            QualitySettings.SetQualityLevel(0, true);
        }
        PlayerPrefs.SetInt("GFX", QualitySettings.GetQualityLevel());

        GFXText.text = ("Graphics: " + QualitySettings.names[QualitySettings.GetQualityLevel()]);
    }

    public void ChangeFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        FullscreenText.text = ("Fullscreen: " + (!Screen.fullScreen ? "On" : "Off"));
    }

    public void ChangeSFX()
    {
        AudioListener.volume = ((AudioListener.volume == 1f) ? 0f : 1f);
        SFXText.text = ("Audio: " + ((AudioListener.volume == 0f) ? "Off" : "On"));
        PlayerPrefs.SetFloat("SFX", AudioListener.volume);
    }
}
