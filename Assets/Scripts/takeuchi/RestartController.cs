using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

[RequireComponent(typeof(NetworkGameManager))]
public class RestartController : MonoBehaviourPunCallbacks
{
    public void RestartSystem()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
