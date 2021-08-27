using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUIEvents : MonoBehaviour
{
    public void Quit(int menuScene)
    {
        SceneManager.LoadSceneAsync(menuScene);
    }

    public void Retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
