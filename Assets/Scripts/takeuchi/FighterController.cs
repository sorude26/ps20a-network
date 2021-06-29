using UnityEngine;
// Photon 用の名前空間を参照する
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class FighterController : MonoBehaviour
{
    [SerializeField] float m_movePower = 5f;
    [SerializeField] float m_jumpPower = 5f;
    [SerializeField] float m_jumpDump = 0.8f;
    Rigidbody2D m_rb = null;
    float m_h = 0f;
    bool m_isGrounded = false;
    Animator m_anim = null;
    float m_lastHorizontalInput = 1f;
    PhotonView m_view = null;

    [SerializeField] float isGroundLine;
    [SerializeField] LayerMask groundLayer = ~0;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!m_view || !m_view.IsMine) return;      // 自分が生成したものだけ処理する

        m_h = Input.GetAxisRaw("Horizontal");

        FlipX(m_h);

        if (Input.GetButtonDown("Jump") && IsGround())
        {
            m_rb.AddForce(Vector2.up * m_jumpPower, ForceMode2D.Impulse);
        }

        if (!Input.GetButton("Jump"))
        {
            Vector2 velocity = m_rb.velocity;
            
            if (velocity.y > 0)
            {
                velocity.y *= m_jumpDump;
                m_rb.velocity = velocity;
            }
        }

        if (m_anim)
            m_anim.SetBool("Punch", Input.GetButtonDown("Fire1"));
    }

    void FixedUpdate()
    {
        if (!m_view || !m_view.IsMine) return;      // 自分が生成したものだけ処理する

        m_rb.AddForce(m_movePower * m_h * Vector2.right, ForceMode2D.Force);
    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    // 手抜き接地判定。このオブジェクトにトリガーがもう一つ付いてしまうと使えない。その場合は BoxCast などを使って接地判定をする。
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        m_isGrounded = true;
    //    }
    //}

    //void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        m_isGrounded = false;
    //    }
    //}

    bool IsGround()
    {
        Vector2 stat = this.gameObject.transform.position;
        Vector2 end = stat + Vector2.down * isGroundLine;
        Debug.DrawLine(stat, end);
        bool isGround = Physics2D.Linecast(stat, end,groundLayer);
        string isGroundLog = isGround.ToString();
        Debug.Log(isGroundLog);
        return isGround;
    }

    void FlipX(float horizontalInput)
    {
        if (horizontalInput != 0)
        {
            if (horizontalInput * m_lastHorizontalInput < 0f)
            {
                Vector3 scale = this.transform.localScale;
                scale.x *= -1;
                this.transform.localScale = scale;
            }

            m_lastHorizontalInput = m_h;
        }
    }

    [PunRPC]
    void Hit(Vector3 attackVector)
    {
        Debug.Log("Hit");
        if (m_view && m_view.IsMine)
        {
            m_rb.AddForce(attackVector, ForceMode2D.Impulse);
        }
    }
}
