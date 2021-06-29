using UnityEngine;
// Photon 用の名前空間を参照する
using Photon.Pun;

public class PunchController : MonoBehaviour
{
    float m_punchPower = 5f;
    PhotonView m_view = null;

    void Start()
    {
        m_view = GetComponent<PhotonView>();
    }
    public void SetPower(float power)
    {
        m_punchPower = power;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_view && m_view.IsMine && collision.gameObject.CompareTag("Player"))
        {
            PhotonView view = collision.gameObject.GetComponent<PhotonView>();
            
            if (view)
            {
                Vector3 dir = view.transform.position - this.transform.parent.position;
                view.RPC("Hit", RpcTarget.Others, dir.normalized * m_punchPower);
            }
        }
    }
}
