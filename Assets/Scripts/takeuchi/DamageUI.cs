using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DamageUI : MonoBehaviour
{
    /// <summary>
    /// HPゲージのImage
    /// </summary>
    [SerializeField] Image hpGauge;
    /// <summary>
    /// 既に受けているダメージの総量
    /// </summary>
    [SerializeField] int currentDamage = 0;
    /// <summary>
    /// 既に受けているダメージ総量の限界値
    /// </summary>
    [SerializeField] int maxDamage = 100;

    /// <summary>
    /// キャンバス
    /// </summary>
    [SerializeField] Canvas canvas;
    
    private void Start()
    {
        //currentHpをインスペクターで変更した際に対応できるようにここでHPゲージの更新をする
        UpdateHpGauge();
    }

    /// <summary>
    /// DamageUI生成時の設定
    /// </summary>
    /// <param name="canvasCamera"></param>
    public void Setup(Camera canvasCamera)
    {
        //worldSpaceのcanvasにcameraを設定する
        canvas.worldCamera = canvasCamera;
    }

    /// <summary>
    /// ダメージを受けた時にダメージ総量を増やす
    /// </summary>
    /// <param name="damage">受けたダメージ値</param>
    public void TakeDamage(int damage)
    {
        currentDamage += damage;
        if (currentDamage > maxDamage)
        {
            currentDamage = maxDamage;
        }
        UpdateHpGauge();
    }
    /// <summary>
    /// 既に受けているダメージの量に合わせて、HPゲージを伸ばす
    /// </summary>
    public void UpdateHpGauge()
    {
        hpGauge.transform.localScale = new Vector3(currentDamage / maxDamage, 1, 1);
    }
}
