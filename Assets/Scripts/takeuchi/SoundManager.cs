using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// 音のリスト
    /// </summary>
    public List<AudioClip> m_audioClips;
    AudioSource m_audioSource;
    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// 音を鳴らす関数
    /// </summary>
    /// <param name="soundNumber">音リストのインデックス番号</param>
    /// <param name="soundVolume">音の大きさ</param>
    public void PlaySound(int soundNumber,float soundVolume = 1f)
    {
        m_audioSource.PlayOneShot(m_audioClips[soundNumber]);
        m_audioSource.volume = soundVolume;
    }
}
