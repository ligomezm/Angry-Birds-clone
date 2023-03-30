using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class CharacterDatabase : ScriptableObject
{
    public Character[] characters;

    public int CharacterCount
    {
        get
        {
            return characters.Length;
        }
    }

    /*public Character GetCharacter(string name)
    {
        int index;

        for (int i = 0; i < characters.Length; i++)
        {
            //if (character[i].GetType().GetProperty("characterName").GetValue(character[i], null).ToString() == name)
            //{
            //index = i;
            //}
        }
        /*int index = 0;
        GameObject characterPrefab;
        foreach (Character bird in character)
        {
            if (bird.GetType().GetProperty("characterName").GetValue(bird, null).ToString() == name)
            { 
                characterPrefab = bird.
            }
        }*/

      //  return character[index];
    //}
    
    /*public Character GetCharacter(int index)
    {
        return character[index];
    }*/
}
