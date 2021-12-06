using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResultManager : MonoBehaviour
{

    [SerializeField] Canvas resultCanvas;
    /// <summary>
    /// メインテキスト、影、アウトラインの三種を登録（順番は問わない）
    /// </summary>
    [SerializeField] GameObject[] resultText = new GameObject[2];
    [SerializeField] Image backgroundImage;
    int winnerNumber = default;
    [SerializeField] Animator animator;
    [SerializeField] GameObject continueButton;

    public void Setup(bool result)
    {
        if (result == true)
        {
            resultText[0].SetActive(true);
        }
        else
        {
            resultText[1].SetActive(true);
        }
        Activate();
    }

    public void Activate()
    {
        resultCanvas.gameObject.SetActive(true);
        animator.SetInteger("Phase",1);
        //アニメーションが終わるのを待つコルーチン
        StartCoroutine(WaitAnimation());
    }

    /// <summary>
    /// アニメーションが終わるのを待つコルーチン
    /// </summary>
    IEnumerator WaitAnimation()
    {
        while (true)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("End") == true)
            {
                break;
            }
            yield return null;
        }
        continueButton.SetActive(true);
    }
}
