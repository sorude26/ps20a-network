using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Stone : MonoBehaviourPunCallbacks
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
                Vector3 dir = view.transform.position - this.transform.position;
                view.RPC("Hit", RpcTarget.Others, dir.normalized * m_punchPower);
            }
        }

        if (collision.tag == "GameZoon")
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
    }

}
