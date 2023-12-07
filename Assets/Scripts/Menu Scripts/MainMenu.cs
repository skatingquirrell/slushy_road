using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        GameManager.Instance.CharacterSelectScene();
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }

}
