using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneNames
{
    public const string MenuScene = "MenuScene";
    public const string Game = "StreetScene";
    public const string PauseMenuScene = "PauseMenuScene";
    public const string Options = "OptionsScene";
    public const string LevelClearMenuScene = "LevelClearMenuScene";
    public const string CharacterSelectScene = "CharacterSelectScene";
}

public class GameManager : MonoBehaviour
{
    bool gameEnded = false;
    public float restartGameDelaySec = 2f;
    public int killedEnemy = 0;

    public static string characterName;
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameManager>();
                if (_instance == null)
                {
                    GameObject managerObject = new GameObject("GameManager");
                    _instance = managerObject.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    public void InitializeGame(string controlCharName = "")
    {
        characterName = controlCharName;
        // Generate a new random scene
        NewRandomScene();
    }

    public void PauseGame()
    {
        // Stop time to pause the game
        Time.timeScale = 0f;

        // Optionally, show the pause menu UI or perform other pause-related tasks
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        // Resume time to continue the game
        Time.timeScale = 1f;

        // Optionally, hide the pause menu UI or perform other resume-related tasks
        Debug.Log("Game Resumed");
    }

    void NewRandomScene()
    {
        SceneGenerator.Instance.NewRandomScene();
    }

    public void EndGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            Debug.Log("GAME OVER!");
            Invoke("Restart", restartGameDelaySec);
        }
    }
    public void Restart()
    {
        Debug.Log("LEVEL RESTARTED");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void CharacterSelectScene()
    {
        SceneManager.LoadScene(SceneNames.CharacterSelectScene);
    }

    public void LevelClear()
    {
        Debug.Log("YOU WIN!");
        SceneManager.LoadScene(SceneNames.LevelClearMenuScene);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    void Update()
    {
        // Check for user input to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        // Toggle the pause state
        bool isPaused = !Time.timeScale.Equals(0f);

        // Pause or resume the game based on the state
        if (isPaused)
        {
            Debug.Log("Game Paused");
            // Stop time to pause the game
            Time.timeScale = 0f;

            // Load the PauseMenuScene
            SceneManager.LoadScene(SceneNames.PauseMenuScene, LoadSceneMode.Additive);
        }
        else
        {
            Debug.Log("Game Resumed");
            // Unload the PauseMenuScene
            SceneManager.UnloadSceneAsync(SceneNames.PauseMenuScene);
            // Resume time to continue the game
            Time.timeScale = 1f;
        }
    }

     public void EnemyKilled()
    {
        killedEnemy++;
        Debug.Log("Killed Enemies Count: " + killedEnemy);
        if(killedEnemy == EnemyManager.instance.KilledEnemyQuota)
        {
                // LEVEL CLEAR
            GameManager.Instance.LevelClear();
        }
        else if( EnemyManager.instance.GeneratedFromPoolEnemies
                 < EnemyManager.instance.maxEnemies)
        {
            //Spawn new enemy after one died
            EnemyManager.instance.GetEnemyInstance();
        }
    }
}
