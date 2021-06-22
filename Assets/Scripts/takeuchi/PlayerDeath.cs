using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;  
using ExitGames.Client.Photon;


public class PlayerDeath : MonoBehaviour
{
    PhotonView m_view;

    public bool m_PlayerDeath = false;

    FighterController Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<FighterController>();
        m_view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
   
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GameZoon")
        {
            if (m_view.IsMine)
            {
                Debug.Log("死んだ");
                m_PlayerDeath = true;
            }
            
        }
    }

    /// <summary>
    /// 自分が落ちた時に呼び出す
    /// </summary>
    void GameOvaer()
    {

    }
}
