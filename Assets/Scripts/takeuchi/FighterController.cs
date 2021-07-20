using UnityEngine;
// Photon 用の名前空間を参照する
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class FighterController : MonoBehaviour
{
    [SerializeField] float m_movePower = 5f;
    [SerializeField] float m_jumpPower = 5f;
    [SerializeField] float m_jumpDump = 0.8f;
    [Tooltip("多段ジャンプ最大回数")]
    [SerializeField] int m_maxAirJumpCount = 1;
    [Tooltip("弱攻撃威力")]
    [SerializeField] float m_lightAttackPower = 5f;
    [Tooltip("強攻撃威力")]
    [SerializeField] float m_strongAttackPower = 8f;
    [Tooltip("プレイヤーのマーク")]
    [SerializeField] GameObject m_playerMark;
    [SerializeField] ActionControlBase m_action = null;
    Rigidbody2D m_rb = null;
    float m_h = 0f;
    int m_airJumpCount = 0;
    float m_lastHorizontalInput = 1f;
    PhotonView m_view = null;

    [SerializeField] float isGroundLine;
    [SerializeField] LayerMask groundLayer = ~0;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!m_view || !m_view.IsMine)
        {
            if (m_playerMark)
            {
                m_playerMark.SetActive(false);
            }
            return;      // 自分が生成したものだけ処理する
        }

        m_h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        FlipX(m_h);
        if (IsGround())
        {
            if (m_h == 0)
            {
                m_action.Stop();
            }
            else
            {
                m_action.Walk();
            }
        }
        else
        {
            m_action.Fall();
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGround())
            {
                m_airJumpCount = m_maxAirJumpCount;
                m_action.Jump(m_jumpPower);
            }
            else if (m_airJumpCount > 0)
            {
                m_airJumpCount--;
                m_action.Jump(m_jumpPower * 1.2f);
            }
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

        if (Input.GetButtonDown("LightAttack"))
        {
            if (v < 0)//上下入力によって技を分岐させる
            {
                m_action.LightAttackD(m_lightAttackPower);
            }
            else if (v > 0)
            {
                m_action.LightAttackU(m_lightAttackPower);
            }
            else
            {
                m_action.LightAttack(m_lightAttackPower);
            }
        }
        else if (Input.GetButtonDown("StrongAttack"))
        {
            if (v < 0)
            {
                m_action.StrongAttackD(m_strongAttackPower);
            }
            else if (v > 0 && m_airJumpCount >= 0)
            {
                m_airJumpCount--;
                m_action.StrongAttackU(m_strongAttackPower);
            }
            else
            {
                m_action.StrongAttack(m_strongAttackPower);
            }
        }
    }

    void FixedUpdate()
    {
        if (!m_view || !m_view.IsMine) return;      // 自分が生成したものだけ処理する

        m_rb.AddForce(m_movePower * m_h * Vector2.right, ForceMode2D.Force);
    }

    bool IsGround()
    {
        Vector2 stat = this.gameObject.transform.position;
        Vector2 end = stat + Vector2.down * isGroundLine;
        Debug.DrawLine(stat, end);
        bool isGround = Physics2D.Linecast(stat, end,groundLayer);
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
            m_action.Hit(attackVector);
        }
    }
}
