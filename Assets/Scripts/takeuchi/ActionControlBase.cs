using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移動以外の入力アクションをまとめた基底クラス
/// </summary>
public class ActionControlBase : MonoBehaviour
{
    [SerializeField] protected PunchController m_punch = null;
    protected Rigidbody2D m_rb = null;
    protected Animator m_anim = null;
    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
    }
    /// <summary>
    /// 歩行
    /// </summary>
    public virtual void Walk()
    {
        m_anim.SetBool("Walk", true);
        m_anim.SetBool("Fall", false);
    }
    /// <summary>
    /// 静止
    /// </summary>
    public virtual void Stop()
    {
        m_anim.SetBool("Walk", false);
        m_anim.SetBool("Fall", false);
    }
    /// <summary>
    /// 落下中
    /// </summary>
    public virtual void Fall()
    {
        m_anim.SetBool("Fall", true);
        m_anim.SetBool("Walk", false);
    }
    /// <summary>
    /// ジャンプ
    /// </summary>
    public virtual void Jump(float power)
    {
        m_rb.AddForce(Vector2.up * power, ForceMode2D.Impulse);
    }
    /// <summary>
    /// 弱攻撃
    /// </summary>
    /// <param name="power"></param>
    public virtual void LightAttack(float power)
    {
        m_punch.SetPower(power);
        if (m_anim)
            m_anim.SetBool("Punch",true);
        m_rb.AddForce(Vector2.right * this.transform.localScale.x * power, ForceMode2D.Impulse);
    }
    /// <summary>
    /// 上弱攻撃
    /// </summary>
    /// <param name="power"></param>
    public virtual void LightAttackU(float power)
    {
        m_punch.SetPower(power);
        if (m_anim)
            m_anim.SetBool("PunchUp", true);
        m_rb.AddForce(Vector2.right * this.transform.localScale.x * power, ForceMode2D.Impulse);
    }
    /// <summary>
    /// 下弱攻撃
    /// </summary>
    /// <param name="power"></param>
    public virtual void LightAttackD(float power)
    {
        m_punch.SetPower(power);
        if (m_anim)
            m_anim.SetBool("PunchDown", true);
        m_rb.AddForce(Vector2.right * this.transform.localScale.x * power, ForceMode2D.Impulse);
    }
    /// <summary>
    /// 強攻撃
    /// </summary>
    /// <param name="power"></param>
    public virtual void StrongAttack(float power)
    {
        m_punch.SetPower(power);
        if (m_anim)
            m_anim.SetBool("StrongPunch", true);
        m_rb.AddForce(Vector2.right * this.transform.localScale.x * power, ForceMode2D.Impulse);
    }
    /// <summary>
    /// 上強攻撃
    /// </summary>
    /// <param name="power"></param>
    public virtual void StrongAttackU(float power)
    {
        m_punch.SetPower(power);
        if (m_anim)
            m_anim.SetBool("PunchUp", true);
        m_rb.AddForce(Vector2.up * power * 1.5f, ForceMode2D.Impulse);
    }
    /// <summary>
    /// 下強攻撃
    /// </summary>
    /// <param name="power"></param>
    public virtual void StrongAttackD(float power)
    {
        m_punch.SetPower(power);
        if (m_anim)
            m_anim.SetBool("PunchDown", true);
        m_rb.AddForce(Vector2.down * power, ForceMode2D.Impulse);
    }
    /// <summary>
    /// 被ダメージ
    /// </summary>
    /// <param name="attackVector"></param>
    public virtual void Hit(Vector3 attackVector)
    {
        m_rb.AddForce(attackVector * 2, ForceMode2D.Impulse);
    }
    /// <summary>
    /// アニメーション再生終了時に呼ぶ
    /// </summary>
    public virtual void AttackEnd()
    {
        if (m_anim)
        {
            m_anim.SetBool("Punch", false);
            m_anim.SetBool("StrongPunch",false);
            m_anim.SetBool("PunchUp", false);
            m_anim.SetBool("PunchDown", false);
        }
    }
}
