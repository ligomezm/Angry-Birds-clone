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
}
