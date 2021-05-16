using UnityEngine;
// Photon 用の名前空間を参照する
using Photon.Pun;

public class PunchController : MonoBehaviour
{
    [SerializeField] float m_punchPower = 5f;
    PhotonView m_view = null;

    void Start()
    {
        m_view = GetComponent<PhotonView>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 手抜き。ここは本来は、他プレイヤーのオーナーのViewから、自らを吹っ飛ばす関数をRPCで呼ぶべき
        Rigidbody2D other = collision.gameObject.GetComponent<Rigidbody2D>();

        if (other)
        {
            Vector3 dir = other.transform.position - this.transform.position;
            other.AddForce(dir.normalized * m_punchPower, ForceMode2D.Impulse);
        }
    }
}
