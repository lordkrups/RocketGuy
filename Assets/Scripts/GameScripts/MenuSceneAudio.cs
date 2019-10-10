using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneAudio : MonoBehaviour
{
    public AudioClip bgmClip;
    public AudioSource bgmSource;
    // Start is called before the first frame update
    void Start()
    {
        bgmClip = Resources.Load<AudioClip>("Audio/bgm");

        //if (EverythingLoader.Instance.gameManager.bgmMuted == 1)
        //{
            PlayBGM();
        //}
    }

    public void PlayBGM()
    {
        bgmSource.Play();
    }
}
