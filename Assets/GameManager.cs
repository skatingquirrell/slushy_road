using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameEnded = false;
    public float restartGameDelaySec = 2f;

    public void EndGame()
    {
        if(!gameEnded)
        {
            gameEnded = true;
            Debug.Log("GAME OVER!");
            Invoke("Restart", restartGameDelaySec);
        }
    }
    void Restart()
    {
        Debug.Log("LEVEL RESTARTED");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
