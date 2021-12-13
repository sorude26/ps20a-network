using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{

    public void Select(int characterNumber)
    {
        NetworkManager.m_chatacterNum = characterNumber;
        Debug.Log(NetworkManager.m_chatacterNum);
    }
}
