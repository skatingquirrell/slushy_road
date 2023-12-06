using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void OnResumeButtonClick()
    {
        GameManager.Instance.TogglePause();
    }

    public void OnRestartButtonClick()
    {
        GameManager.Instance.TogglePause();
        GameManager.Instance.Restart();
    }

    public void OnQuitButtonClick()
    {
        GameManager.Instance.QuitGame();
    }

    public void OnMenuButtonClick()
    {
        GameManager.Instance.TogglePause();
        SceneManager.LoadScene(SceneNames.MenuScene); 
    }
}
