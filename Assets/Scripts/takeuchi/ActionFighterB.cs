using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFighterB : ActionControlBase
{

    public override void LightAttack(float power)
    {
        if (m_actionNow) return;
        m_punch.SetPower(power * 2);
        if (m_anim)
            m_anim.Play("Punch");
        m_rb.AddForce(Vector2.right * 2 * this.transform.localScale.x * power, ForceMode2D.Impulse);
        //m_stoneThrow.ThrowStone(new Vector2(1, 1), power);
        m_actionNow = true;
    }
    public override void LightAttackU(float power)
    {
        if (m_actionNow) return;
        m_punch.SetPower(power * 2);
        if (m_anim)
            m_anim.Play("PunchUp");
        m_rb.AddForce(new Vector2(1, 3).normalized * this.transform.localScale.x * power, ForceMode2D.Impulse);
        m_stoneThrow.ThrowStone(new Vector2(1, 3).normalized * 2f, power);
        m_actionNow = true;
    }
    public override void LightAttackD(float power)
    {
        if (m_actionNow) return;
        m_punch.SetPower(power * 2);
        if (m_anim)
            m_anim.Play("PunchDown");
        m_rb.AddForce(new Vector2(1, -3).normalized * this.transform.localScale.x * power, ForceMode2D.Impulse);
        //m_stoneThrow.ThrowStone(new Vector2(1, -1).normalized, power);
        m_actionNow = true;
    }
}
