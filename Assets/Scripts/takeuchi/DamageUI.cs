using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DamageUI : MonoBehaviour
{
    /// <summary>
    /// damageゲージのImage
    /// </summary>
    [SerializeField] Image damageGauge;

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

    /// <summary>
    /// プレイヤーに追従するためのtransform
    /// </summary>
    [SerializeField] Transform playerTransform;

    private void Start()
    {
        //currentHpをインスペクターで変更した際に対応できるようにここでHPゲージの更新をする
        UpdateDamageGauge();
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            UpdatePosition();
        }
    }

    /// <summary>
    ///  DamageUI生成時の設定
    /// </summary>
    /// <param name="canvasCamera">canvasに設定するcamera</param>
    /// <param name="playerTransform">UIが追従するplayerのtransform</param>
    public void Setup(Camera canvasCamera, Transform playerTransform)
    {
        //worldSpaceのcanvasにcameraを設定する
        this.canvas.worldCamera = canvasCamera;
        //追従するプレイヤーのtransformを設定する
        this.playerTransform = playerTransform;
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
        UpdateDamageGauge();
    }

    /// <summary>
    /// 既に受けているダメージの量に合わせて、Damageゲージを伸ばす
    /// </summary>
    public void UpdateDamageGauge()
    {
        damageGauge.transform.localScale = new Vector3((float)currentDamage / maxDamage, 1, 1);
    }

    /// <summary>
    /// 表示位置をプレイヤーの真上にする
    /// </summary>
    public void UpdatePosition()
    {
        this.transform.position = playerTransform.position;
        this.transform.position += new Vector3(0, 1.2f, 0);
    }
}
