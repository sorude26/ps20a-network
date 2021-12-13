using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange
{
    private static bool roadNow = false;
    private static void Title()
    {
        roadNow = false;
        SceneManager.LoadScene("TitleScene");
    }
    /// <summary>
    /// Titleシーンに移行する
    /// </summary>
    public static void RoadTitle()
    {
        if (roadNow)
        {
            return;
        }
        roadNow = true;
        FadeControl.StartFadeOut(Title);
    }

    private static void Game()
    {
        roadNow = false;
        SceneManager.LoadScene("GameScene");
    }
    /// <summary>
    /// Gameシーンに移行する
    /// </summary>
    public static void RoadGame()
    {
        if (roadNow)
        {
            return;
        }
        roadNow = true;
        FadeControl.StartFadeOut(Game);
    }
    private static void Target(string target)
    {
        roadNow = false;
        SceneManager.LoadScene(target);
    }
    /// <summary>
    /// 目標シーンに移行する
    /// </summary>
    public static void RoadTarget(string target)
    {
        if (roadNow)
        {
            return;
        }
        roadNow = true;
        FadeControl.StartFadeOut(() => Target(target));
    }
}
