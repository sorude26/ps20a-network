using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StoneThrowing : MonoBehaviourPunCallbacks
{

    /// <summary>
    /// 投げる石のオブジェクト
    /// </summary>
    [SerializeField] GameObject stone;

    /// <summary>
    /// 石のリジッドボディ
    /// </summary>
    Rigidbody2D stoneRb;

    /// <summary>
    /// 投げる際にImpulseで与える力
    /// </summary>
    [SerializeField] float throwPower = 1.0f;

    [SerializeField] Vector2 throwDirection = new Vector2(1, 1);

    [SerializeField] Transform CreatePos;

    /// <summary>
    /// 投げる向き　true: 右　false:左
    /// </summary>
    public bool sideways = true;

    PhotonView m_view;

    private void Start()
    {
        m_view = GetComponent<PhotonView>();
    }


    [PunRPC]
    /// <summary>
    /// 石を投げる
    /// </summary>
    public void ThrowStone()
    {
        if ((sideways && throwDirection.x < 0) || (!sideways && throwDirection.x > 0))
        {
            throwDirection.x *= -1;
        }

        //GameObject stoneInstance = Instantiate(stone);
        GameObject stoneInstance = PhotonNetwork.Instantiate("Stone",Vector3.zero,Quaternion.identity);
        //stoneInstance.transform.position = CreatePos.position;
        
        /*
        stoneRb = stoneInstance.GetComponent<Rigidbody2D>();
        stoneRb.AddForce(throwDirection * throwPower, ForceMode2D.Impulse);
        if (m_view && m_view.IsMine)
        {
            m_view.RPC("ThrowStone", RpcTarget.Others);
        }
        */
    }
}
