using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGenerator : MonoBehaviour
{
    public GameObject characterPrefab;
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
            SwitchCharacter(GameManager.characterName);
            RandomizeStreetSceneAppearance(streetScene);
            finishedRandomizingScene = true;
        }
    }
    public void NewRandomScene()
    {
        Debug.Log("Game character: " + GameManager.characterName);

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

    private void SwitchCharacter(string characterName)
    {
        if (characterName == null || characterName.Equals(""))
        {
            return;
        }
        // Get the new character prefab based on the selected character
        string prefabPath = GetPrefabPath(characterName);
        GameObject newCharacterPrefab = Resources.Load<GameObject>(prefabPath);

        // Replace the existing character prefab with the new one
        ReplaceCharacterPrefab(newCharacterPrefab);
    }

    private void ReplaceCharacterPrefab(GameObject newCharacterPrefab)
    {
        if (newCharacterPrefab != null && characterPrefab != null)
        {
            CopyMaterialSettingsFromChild(newCharacterPrefab, characterPrefab, "PlayerChild/Char");
        }
        else
        {
            Debug.LogError("Prefab not found or characterPrefab not set.");
        }

    }

    void CopyMaterialSettingsFromChild(GameObject source, GameObject target, string childPath)
    {
        Transform sourceChild = source.transform.Find(childPath);
        Transform targetChild = target.transform.Find(childPath);

        if (sourceChild == null || targetChild == null)
        {
            Debug.LogWarning("Child not found in either source or target prefab.");
            return;
        }

        // Get the Material component from the source child
        Renderer sourceRenderer = sourceChild.GetComponent<Renderer>();
        if (sourceRenderer != null)
        {
            Material sourceMaterial = sourceRenderer.sharedMaterial;

            // Replace the Material on the target child
            Renderer targetRenderer = targetChild.GetComponent<Renderer>();
            if (targetRenderer != null)
            {
                targetRenderer.sharedMaterial = sourceMaterial;
            }
            else
            {
                Debug.LogWarning("Renderer not found on target child.");
            }
        }
        else
        {
            Debug.LogWarning("Renderer not found on source child.");
        }
    }

    public static string GetPrefabPath(string characterName)
    {
        var characterData = CharacterConstants.CharacterList.Find(data => data.Item1 == characterName);

        if (characterData != default)
        {
            // characterData.Item4 represents the prefab path
            return characterData.Item4;
        }
        else
        {
            // Return null or handle the case when the character name is not found
            return null;
        }
    }
}
