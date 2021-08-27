using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUIEvents : MonoBehaviour
{
    public void Quit(int menuScene)
    {
        SceneManager.LoadScene(menuScene);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
