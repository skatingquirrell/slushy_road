using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void OnRestartButtonClick()
    {
        GameManager.Instance.Restart();
    }

    public void OnQuitButtonClick()
    {
        GameManager.Instance.QuitGame();
    }

    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene(SceneNames.MenuScene); 
    }
}
