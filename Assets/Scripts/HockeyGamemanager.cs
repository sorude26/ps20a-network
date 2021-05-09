using UnityEngine;
using UnityEngine.UI;
// Photon 用の名前空間を参照する
using Photon.Pun;

/// <summary>
/// 二人プレイを前提としている。
/// 三人以上で同時にプレイするようにしたい場合は設計し直す必要がある。
/// </summary>
public class HockeyGamemanager : MonoBehaviour
{
    [SerializeField] string m_packPrefabName = "ResourcePrefabName";
    [SerializeField] Transform m_packSpawnPoint = null;
    [SerializeField] Text m_console = null;
    HockeyGameState m_state = HockeyGameState.Initializing;

    void Start()
    {
        m_console.text = "Wait...";
    }

    void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            switch (m_state)
            {
                case HockeyGameState.Initializing:
                    if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
                    {
                        m_state = HockeyGameState.Player2Serve;
                    }
                    break;
                case HockeyGameState.Player1Serve:
                    SpawnPack(1);
                    m_state = HockeyGameState.InGame;
                    break;
                case HockeyGameState.Player2Serve:
                    SpawnPack(2);
                    m_state = HockeyGameState.InGame;
                    break;
                default:
                    break;
            }
        }
    }

    void SpawnPack(int actorNumber)
    {
        int myActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        float x = Random.Range(-1f, 1f);
        float y = myActorNumber == 1 ? -1 : 1;
        m_packSpawnPoint.up = new Vector3(x, y, 0f);

        if (myActorNumber == actorNumber)
        {
            PhotonNetwork.Instantiate(m_packPrefabName, m_packSpawnPoint.position, m_packSpawnPoint.rotation);
            m_console.text = "Press SPACE to serve...";
            Invoke("ClearConsole", 1.5f);
        }
        else
        {
            ClearConsole();
        }
    }

    void ClearConsole()
    {
        m_console.text = "";
    }

    public void Serve(int actorNumber)
    {
        if (actorNumber == 1)
        {
            m_state = HockeyGameState.Player1Serve;
        }
        else
        {
            m_state = HockeyGameState.Player2Serve;
        }
    }
}

public enum HockeyGameState
{
    Initializing,
    Player2Serve,
    Player1Serve,
    InGame,
}