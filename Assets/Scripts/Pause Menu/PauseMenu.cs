using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseUI;
    public GameObject OptionsUI;
    public HealthSystem HS;

    public static PauseMenu Instance;

    // Private / Hidden variables.
    [HideInInspector] public bool Paused;

    private void Awake()
    {
        if(Instance != this) Destroy(Instance);
        Instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !HS.Dead) InvokePause();
    }

    public void InvokePause()
    {
        if(!Paused)
        {
            Pause();
        }else
        {
            Resume();
        }
    }

    public void Pause()
    {
        Paused = true;
        OptionsUI.SetActive(false);
        PauseUI.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Paused = false;
        OptionsUI.SetActive(false);
        PauseUI.SetActive(false);

        Time.timeScale = 1f;
    }

    public void Quit(int scene)
    {
        SceneManager.LoadScene(scene);
    }

}
