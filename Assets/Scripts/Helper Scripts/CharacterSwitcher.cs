using System.Collections.Generic;
using UnityEngine;

public static class CharacterConstants
{
    public static readonly List<(string, string, string, string)> CharacterList = new List<(string, string, string, string)>
    {
        ("Man", "GUI_Parts/Icons/skill_icon_01", "GUI_Parts/Icons/skill_icon_04", "Prefabs/Player Prefab/Player"),
        ("Woman", "GUI_Parts/Icons/skill_icon_03", "GUI_Parts/Icons/skill_icon_02", "Prefabs/Player Prefab/SilverRankPlayer"),
    };
}

public class CharacterData : ScriptableObject
{
    public string characterName;
    private Sprite skillSprite1;
    private Sprite skillSprite2;
    private string prefabPath;

    public Sprite SkillSprite2 { get => skillSprite2; set => skillSprite2 = value; }
    public Sprite SkillSprite1 { get => skillSprite1; set => skillSprite1 = value; }
    public string PrefabPath { get => prefabPath; set => prefabPath = value; }
}

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject[] characters;
    private List<CharacterData> characterDataList;
    private int currentIndex = 0;


    void Start()
    {
        InitializeCharacterData();
        UpdateCharacter();
    }
    private void InitializeCharacterData()
    {
        // Create instances of CharacterData from constant data
        characterDataList = new List<CharacterData>();

        foreach (var characterTuple in CharacterConstants.CharacterList)
        {
            CharacterData characterData = ScriptableObject.CreateInstance<CharacterData>();
            characterData.characterName = characterTuple.Item1;
            characterData.SkillSprite1 = Resources.Load<Sprite>(characterTuple.Item2);
            characterData.SkillSprite2 = Resources.Load<Sprite>(characterTuple.Item3);
            characterData.PrefabPath = characterTuple.Item4;
            characterDataList.Add(characterData);
        }
    }
    public void SwitchNextCharacter()
    {
        currentIndex = (currentIndex + 1) % characters.Length;
        UpdateCharacter();
    }

    public void SwitchPreviousCharacter()
    {
        currentIndex = (currentIndex - 1 + characters.Length) % characters.Length;
        UpdateCharacter();
    }

    private void UpdateCharacter()
    {
        // Disable all characters
        foreach (var character in characters)
        {
            character.SetActive(false);
        }

        // Enable the current character
        characters[currentIndex].SetActive(true);

        // Get SkillDisplay script and call DisplaySkills method
        SkillDisplay skillDisplay = GetComponent<SkillDisplay>();
        skillDisplay.DisplaySkills(GetSkillSprite1(currentIndex), GetSkillSprite2(currentIndex));
    }

    private Sprite GetSkillSprite2(int currentIndex)
    {
        if (currentIndex < characterDataList.Count)
        {

            return characterDataList[currentIndex].SkillSprite2;
        }
        return null;

    }

    private Sprite GetSkillSprite1(int currentIndex)
    {
        if (currentIndex < characterDataList.Count)
        {
            return characterDataList[currentIndex].SkillSprite1;

        }
        return null;
    }

    public void StartGame()
    {
        GameManager.Instance.InitializeGame(characterDataList[currentIndex].characterName);
    }
}
