using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class SceneGenerator : MonoBehaviour
{
    private const string SceneName = "StreetScene";
    private const string DirectionalLight = "Directional Light";
    private const string EnvironmentObject = "Environment";
    private bool finishedRandomizingScene = false;
    private static SceneGenerator _instance;

    public static SceneGenerator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<SceneGenerator>();
                if (_instance == null)
                {
                    GameObject generatorObject = new GameObject("SceneGenerator");
                    _instance = generatorObject.AddComponent<SceneGenerator>();
                }
            }
            return _instance;
        }
    }

    private void Update()
    {
        if (finishedRandomizingScene)
        {
            return;
        }
        // Get the newly loaded scene
        Scene streetScene = SceneManager.GetSceneByName(SceneName);
        if (streetScene.isLoaded)
        {
            RandomizeStreetSceneAppearance(streetScene);
            finishedRandomizingScene = true;
        }
    }
    public void NewRandomScene()
    {
        // Load the StreetScene and set it as the active scene
        SceneManager.LoadScene(SceneName);
        Debug.Log("Loaded new game scene.");
    }

    private void RandomizeStreetSceneAppearance(Scene streetScene)
    {
        // Iterate through all GameObjects in the loaded scene
        foreach (GameObject go in streetScene.GetRootGameObjects())
        {
            // Randomize the Directional Light color
            if (go.name.Equals(DirectionalLight))

            {
                // Debug.Log("Found GameObject: " + go.name);
                go.GetComponent<Light>().color = Random.ColorHSV();
            }

            // Randomize the Material colors of the "environment" object's children
            if (go.name.Equals(EnvironmentObject))
            {
                // Debug.Log("Found GameObject: " + go.name);
                RandomizeMaterialColors(go);
            }
        }
    }

    void RandomizeMaterialColors(GameObject parentObject)
    {
        // Randomize the colors of all materials in the children of the parent object
        Renderer[] renderers = parentObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            foreach (Material material in renderer.materials)
            {
                material.color = Random.ColorHSV();
            }
        }
    }
}
