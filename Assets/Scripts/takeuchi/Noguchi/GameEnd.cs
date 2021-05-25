using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    FighterController player;
    FighterController enemy;

    bool gameEnded = false;



    private void Update()
    {
        if (player.transform.localPosition.y < -5.0f)
        {
            GameEndEvent(enemy);
        }

    }

    /// <summary>
    /// ゲーム終了時に呼ぶ関数
    /// </summary>
    public void GameEndEvent(FighterController winner)
    {
        gameEnded = true;

    }
}
