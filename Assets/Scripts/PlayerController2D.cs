using UnityEngine;
// Photon 用の名前空間を参照する
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerController2D : MonoBehaviour
{
    /// <summary>動く力</summary>
    [SerializeField] float m_movePower = 10f;
    /// <summary>ダッシュ力</summary>
    [SerializeField] float m_dashPower = 50f;
    /// <summary>一度ダッシュした後、次にダッシュできるまでの秒数</summary>
    [SerializeField] float m_dashPeriod = 3f;
    /// <summary>鬼の時の色</summary>
    [SerializeField] Color m_taggedColor = Color.red;
    /// <summary>鬼ではない時の色</summary>
    [SerializeField] Color m_normalColor = Color.white;
    /// <summary>鬼を移された後、移せない</summary>
    [SerializeField] float m_gracePeriod = 1.5f;

    Rigidbody2D m_rb = null;
    PhotonView m_view = null;
    SpriteRenderer m_sprite = null;
    float m_dashTimer = 0f;
    float m_graceTimer = 0f;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_view = GetComponent<PhotonView>();
        m_dashTimer = m_dashPeriod;
        m_sprite = GetComponent<SpriteRenderer>();
        m_sprite.color = m_normalColor;
    }

    void Update()
    {
        if (!m_view || !m_view.IsMine) return;      // 自分が生成したものだけ処理する

        Move();
        Rotate();

        if (m_dashTimer < m_dashPeriod)
        {
            m_dashTimer += Time.deltaTime;
        }
        else if (Input.GetButtonDown("Jump"))
        {
            Dash();
            m_dashTimer = 0f;
        }

        if (m_graceTimer < m_gracePeriod)
        {
            m_graceTimer += Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_view && m_view.IsMine && m_sprite.color.Equals(m_taggedColor)
            && m_graceTimer >= m_gracePeriod && collision.gameObject.CompareTag("Player"))
        {
            PhotonView view = collision.gameObject.GetComponent<PhotonView>();

            if (view)
            {
                view.RPC("Tag", RpcTarget.All);
            }

            m_view.RPC("Release", RpcTarget.All);
        }
    }

    void Dash()
    {
        m_rb.AddForce(this.transform.up * m_dashPower, ForceMode2D.Impulse);
    }

    void Rotate()
    {
        this.transform.up = m_rb.velocity;
    }

    /// <summary>
    /// 上下左右にキャラクターを動かす
    /// </summary>
    void Move()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector2 dir = (Vector2.up * v + Vector2.right * h).normalized;

        if (dir != Vector2.zero)
        {
            m_rb.AddForce(dir * m_movePower, ForceMode2D.Force);
        }
    }

    [PunRPC]
    public void Tag()
    {
        ChangeColor(m_taggedColor.r, m_taggedColor.g, m_taggedColor.b, m_taggedColor.a);
    }

    [PunRPC]
    public void Release()
    {
        ChangeColor(m_normalColor.r, m_normalColor.g, m_normalColor.b, m_normalColor.a);
    }

    void ChangeColor(float r, float g, float b, float a)
    {
        m_sprite.color = new Color(r, g, b, a);
        m_graceTimer = 0f;
    }
}
