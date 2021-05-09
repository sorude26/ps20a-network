using UnityEngine;
using UnityEngine.UI;
// Photon 用の名前空間を参照する
using Photon.Pun;

public class TagGameManager : MonoBehaviour
{
    [SerializeField] Text m_console = null;
    bool m_isGameStarted = false;

    void Start()
    {
        m_console.text = "Wait for other players...";
    }

    void Update()
    {
        if (PhotonNetwork.InRoom && !m_isGameStarted
            && PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            m_console.text = "Game Start!";
            Invoke("ClearConsole", 1.5f);
            m_isGameStarted = true;

            if (PhotonNetwork.IsMasterClient)
            {
                PlayerController2D[] players = GameObject.FindObjectsOfType<PlayerController2D>();
                PhotonView view = players[Random.Range(0, players.Length)].GetComponent<PhotonView>();
                view.RPC("Tag", RpcTarget.All);
            }
        }
    }

    void ClearConsole()
    {
        m_console.text = "";
    }
}
