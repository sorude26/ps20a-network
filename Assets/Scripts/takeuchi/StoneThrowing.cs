using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Resouce内の"Stone"プレハブを生成し、指定した方向にに飛ばすクラス
/// </summary>
public class StoneThrowing : MonoBehaviourPunCallbacks
{

    /// <summary>
    /// 石のリジッドボディ
    /// </summary>
    Rigidbody2D stoneRb;

    /// <summary>
    /// 投げる際にImpulseで与える力
    /// </summary>
    [SerializeField] float throwPower = 1.0f;

    /// <summary>
    /// 投げる角度
    /// </summary>
    [SerializeField] Vector2 throwDirection = new Vector2(1, 1);

    /// <summary>
    /// 投げる向き　true: 右　false:左
    /// </summary>
    public bool sideways = true;

    /// <summary>
    /// 投げるキャラクターオブジェクト
    /// </summary>
    [SerializeField] GameObject producer;

    /// <summary>
    /// 投げる向き
    /// </summary>
    float producerScale;

    [PunRPC]
    /// <summary>
    /// 石を投げる
    /// </summary>
    public void ThrowStone()
    {
        producerScale = producer.transform.localScale.x;
        Vector3 throwSetteing = throwDirection * throwPower;
        throwSetteing.x *= producerScale >= 0 ? 1.0f : -1.0f;

        GameObject stoneInstance = PhotonNetwork.Instantiate("Stone", transform.localPosition, Quaternion.identity);
        stoneRb = stoneInstance.GetComponent<Rigidbody2D>();
        stoneRb.AddForce(throwSetteing, ForceMode2D.Impulse);
    }
}
