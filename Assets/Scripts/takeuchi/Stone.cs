using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Stone : MonoBehaviourPunCallbacks
{

    [SerializeField] float m_punchPower = 10f;
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
        if (m_view && !m_view.IsMine && collision.gameObject.CompareTag("Player"))
        {
            PhotonView collisionView = collision.GetComponent<PhotonView>();
            Vector3 dir = collision.transform.position - this.transform.position;

            collision.GetComponent<ActionControlBase>().Hit(dir.normalized * m_punchPower);
            Debug.Log(dir.normalized * m_punchPower);

        }

        if (collision.tag == "GameZoon")
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
    }

}
