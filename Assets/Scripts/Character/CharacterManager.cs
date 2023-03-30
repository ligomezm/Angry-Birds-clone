using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public GameObject birdPrefab;
    private string selectedOptionName;

    public void Start()
    {
        for (int i = 0; i < characterDB.CharacterCount; i++)
        {
            int index = i;
            Button button = transform.Find("Slot" + (index + 1)).GetChild(0).GetComponent<Button>();
            button.onClick.AddListener(() => CharacterSelected(characterDB.characters[index]));
        }

    }

    void CharacterSelected(Character character)
    {
        birdPrefab = character.characterPrefab;
        selectedOptionName = character.characterName;
    }

}
