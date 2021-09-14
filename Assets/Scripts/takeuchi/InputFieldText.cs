using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldText : MonoBehaviour
{
    [SerializeField] InputField roomName;
    [SerializeField] InputField playerName;

    public void RoomNameSet()
    {
        NetworkTest.m_joinRoomName = roomName.text;
    }

    public void PlayerNameSet()
    {
        NetworkTest.m_playerName = playerName.text;
    }
}
