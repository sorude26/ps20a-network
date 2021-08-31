using System.Collections.Generic;
using UnityEngine;
// Photon 用の名前空間を参照する
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public class CharacterManager : MonoBehaviourPunCallbacks // Photon Realtime 用のクラスを継承する
{
    /// <summary>プレイヤーのプレハブの名前</summary>
    [SerializeField] public static string m_playerPrefabName = "Prefab";

    public static void SetPrefabName(string name)
    {
        m_playerPrefabName = name;
    }
}