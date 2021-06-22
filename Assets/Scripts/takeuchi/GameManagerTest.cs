using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;   // PhotonNetwork を使うため
using Photon.Realtime;  // RaiseEventOptions/ReceiverGroup を使うため
using ExitGames.Client.Photon;  // SendOptions を使うため

public class GameManagerTest : MonoBehaviourPunCallbacks, IOnEventCallback
{
    PhotonView m_view = null;

    public const byte MoveUnitsToTargetPositionEventCode = 150;

    enum GameStatus
    {
        Playing = 1,
        IDied = 10,
        Game_Win = 11,
        Game_Lose = 12
    }
    [SerializeField] GameStatus gamestatus = GameStatus.Playing;

    int numberOfLivingPlayer = 2;

    GameManagerTest masterGameManager;
    List<GameManagerTest> gameManagerList = new List<GameManagerTest>();

    private void Start()
    {
        m_view = GetComponent<PhotonView>();
    }

    /// <summary>
    /// マスター（ホスト）のゲームマネージャーを設定する
    /// </summary>
    /// <param name="masterGameManager">マスターのゲームマネージャー</param>
    public void SetMasterGameManager(GameManagerTest masterGameManager)
    {
        this.masterGameManager = masterGameManager;
    }

    /// <summary>
    /// 他のプレイヤーのゲームマネージャーを自身のリストに追加する
    /// </summary>
    /// <param name="gameManager_Another"></param>
    public void AddAnotherGameManager(GameManagerTest gameManager_Another)
    {
        gameManagerList.Add(gameManager_Another);
        numberOfLivingPlayer++;
    }

    /// <summary>
    /// 自身が死んだ時に、他のプレイヤーに通知する
    /// </summary>
    public void PlayerDied()
    {
        gamestatus = GameStatus.Game_Lose;
        SendMoveUnitsToTargetPositionEvent();
        Debug.Log("You Lose...");
    }

    /// <summary>
    /// 他のプレイヤーが死んだときに、自身で管理している残りプレイヤー数を減らし、
    /// もし自分しか生き残っていなかったら勝利する
    /// </summary>
    public void AnotherPlayerDied()
    {
        numberOfLivingPlayer--;
        if (numberOfLivingPlayer == 1 && gamestatus == GameStatus.Playing)      //生きているプレイヤーが1人かつ、自分がゲーム中の時
        {
            Debug.Log("You Win!!");
        }
    }

    public void SendMoveUnitsToTargetPositionEvent()
    {
        object[] content = new object[] { new Vector3(10.0f, 2.0f, 5.0f), 1, 2, 5, 10 }; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(MoveUnitsToTargetPositionEventCode, null, raiseEventOptions, SendOptions.SendReliable);
    }

    public void OnEvent(EventData e)
    {
        if ((int)e.Code == 150)     //こっちは死んだよって言われたら
        {
            AnotherPlayerDied();
        }
    }
    /*
     *  ◆暫定イベントコード一覧

     *  ・150 = 送信したプレイヤーが死んだ
     * 
     */
}
