using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelClearMenu : MonoBehaviour
{

    public void OnQuitButtonClick()
    {
        GameManager.Instance.QuitGame();
    }

    public void OnMenuButtonClick()
    {
        GameManager.Instance.TogglePause();
        SceneManager.LoadScene(SceneNames.MenuScene);
    }

    public void NewGame()
    {
        GameManager.Instance.InitializeGame();
    }
}
