using UnityEngine;
// Photon 用の名前空間を参照する
using Photon.Pun;

public class KillzoneController : MonoBehaviour
{
    [SerializeField] HockeyGamemanager m_gameManager = null;
    [SerializeField] int m_sideOfPlayer = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView view = collision.gameObject.GetComponent<PhotonView>();

        if (view && view.IsMine)
        {
            PhotonNetwork.Destroy(view.gameObject);
        }

        m_gameManager.Serve(m_sideOfPlayer);
    }
}
