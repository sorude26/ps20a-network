using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{

    public void Select(string characterName)
    {
        CharacterManager.SetPrefabName(characterName);
        Debug.Log(CharacterManager.m_playerPrefabName);
    }
}
