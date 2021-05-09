using UnityEngine;
// Photon 用の名前空間を参照する
using Photon.Pun;

public class PackController : MonoBehaviour
{
    /// <summary>初速</summary>
    [SerializeField] float m_speed = 5f;
    bool m_isStarted = false;
    PhotonView m_view = null;
    Rigidbody2D m_rb = null;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (m_view && m_view.IsMine && !m_isStarted && Input.GetButtonUp("Jump"))
        {
            m_isStarted = true;
            m_rb.velocity = this.transform.up * m_speed;
        }
    }
}
