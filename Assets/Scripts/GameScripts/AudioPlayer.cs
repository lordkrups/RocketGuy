using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip bgmClip, rocketClip, crashClip, getItemClip;

    public AudioSource bgmSource, sfxSource, rocketSource;

    // Start is called before the first frame update
    void Start()
    {
        bgmClip = Resources.Load<AudioClip>("Audio/bgm");

        rocketClip = Resources.Load<AudioClip>("Audio/rocket");
        crashClip = Resources.Load<AudioClip>("Audio/crash");
        getItemClip = Resources.Load<AudioClip>("Audio/getItem");

        //if (EverythingLoader.Instance.gameManager.bgmMuted == 1)
        //{
        PlayBGM();
        //}
        //rocketSource.Play();
    }

    public void PlayBGM()
    {
        bgmSource.Play();
    }

    public void PlayRocketSound()
    {
        rocketSource.Play();
    }
    public void PauseRocketSound()
    {
        rocketSource.Stop();
    }

    public void PlaySFXClip(string clip)
    {
        Debug.Log("PlaySFXClip: " + clip);
        //if (EverythingLoader.Instance.gameManager.sfxMuted == 1)
        //{
            switch (clip)
            {
                case "rocketBlast":
                    //sfxSource.pit
                    sfxSource.PlayOneShot(rocketClip);
                    break;
                case "crash":
                    sfxSource.PlayOneShot(crashClip);
                    break;
                case "getItem":
                    sfxSource.PlayOneShot(getItemClip);
                    break;
                default:
                    break;
            }
        //}
    }
}
