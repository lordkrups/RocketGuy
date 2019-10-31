using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip bgmClip, rocketClip, crashClip, getItemClip, explosion;

    public AudioSource bgmSource, sfxSource, rocketSource;

    // Start is called before the first frame update
    void Start()
    {
        bgmClip = Resources.Load<AudioClip>("Audio/bgm");

        rocketClip = Resources.Load<AudioClip>("Audio/rocket");
        crashClip = Resources.Load<AudioClip>("Audio/crash");
        getItemClip = Resources.Load<AudioClip>("Audio/getItem");

        if (RocketGameManager.Instance.persistantBGM == 1)
        {
            PlayBGM();
        }
        rocketSource.volume = 1f;
    }

    public void PlayBGM()
    {
        bgmSource.Play();
    }

    public void PlayRocketSound()
    {
        rocketSource.Play();
    }
    public void StopRocketSound()
    {
        rocketSource.Stop();
    }
    public void StopRocketSFX()
    {
        sfxSource.Stop();
    }
    public void EndAllSound()
    {
        sfxSource.Stop();
        rocketSource.Stop();
        bgmSource.Stop();
    }
    public void PlaySFXClip(string clip)
    {
        if (RocketGameManager.Instance.persistantSFX == 1)
        {
            switch (clip)
            {
                case "rocketBlast":
                    //sfxSource.pit
                    if (!rocketSource.isPlaying)
                    {
                        rocketSource.PlayOneShot(rocketClip);
                    }
                    break;
                case "crash":
                    sfxSource.PlayOneShot(crashClip);
                    break;
                case "getItem":
                    sfxSource.PlayOneShot(getItemClip);
                    break;
                case "explosion":
                    //rocketSource.volume = 1f;
                    sfxSource.PlayOneShot(explosion);
                    break;
                default:
                    break;
            }
        }
    }
}
