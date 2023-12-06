using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        GameManager.Instance.InitializeGame();
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }

}
