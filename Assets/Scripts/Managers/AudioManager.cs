using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip bgmClip, crashSoundClip, enemyShotSoundClip, getItemSoundClip,
        missileHitSoundClip, playerShotSoundClip, skillSpinSoundClip, skillSpinEndSoundClip;
    public AudioSource bgmSource, sfxSource;

    // Start is called before the first frame update
    void Start()
    {
        bgmClip = Resources.Load<AudioClip>("Audio/bgm");

        crashSoundClip = Resources.Load<AudioClip>("Audio/crash");
        enemyShotSoundClip = Resources.Load<AudioClip>("Audio/enemyshot");
        getItemSoundClip = Resources.Load<AudioClip>("Audio/getitem");
        missileHitSoundClip = Resources.Load<AudioClip>("Audio/missilehit");
        playerShotSoundClip = Resources.Load<AudioClip>("Audio/playershot");

        skillSpinSoundClip = Resources.Load<AudioClip>("Audio/spin");
        skillSpinEndSoundClip = Resources.Load<AudioClip>("Audio/spinend");

        if (EverythingLoader.Instance.gameManager.bgmMuted == 1)
        {
            PlayBGM(); 
        }
    }
    public void PlayBGM()
    {
        bgmSource.Play();      
    }
    public void StopBGM()
    {
        bgmSource.Stop();
    }
    // Update is called once per frame
    public void PlaySFXClip(string clip)
    {
        Debug.Log("PlaySFXClip: " + clip);
        if (EverythingLoader.Instance.gameManager.sfxMuted == 1)
        {
            switch (clip)
            {
                case "crash":
                    //sfxSource.pit
                    sfxSource.PlayOneShot(crashSoundClip);
                    break;
                case "enemyShoot":
                    sfxSource.PlayOneShot(enemyShotSoundClip);
                    break;
                case "getItem":
                    sfxSource.PlayOneShot(getItemSoundClip);
                    break;
                case "missileHit":
                    sfxSource.PlayOneShot(missileHitSoundClip);
                    break;
                case "playerShoot":
                    sfxSource.PlayOneShot(playerShotSoundClip);
                    break;
                case "spin":
                    sfxSource.PlayOneShot(skillSpinSoundClip);
                    break;
                case "spinend":
                    sfxSource.PlayOneShot(skillSpinEndSoundClip);
                    break;
                default:
                    break;
            }
        }
    }
}
