using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    public LevelManager levelManager;
    public bool sfxMuted;
    public UILabel sfxMuteStatusLabel;
    public bool bgmMuted;
    public UILabel bgmMuteStatusLabel;



    public UIGrid puGrid;
    public List<PUMenuObj> puList = new List<PUMenuObj>();
    public PUMenuObj pUMenuObj;

    public void OnOpen()
    {

        sfxMuted = EverythingLoader.Instance.gameManager.sfxMuted == 1;
        bgmMuted = EverythingLoader.Instance.gameManager.bgmMuted == 1;
        ColourSFXText();
        ColourBGMText();

        for (int i = 0; i < levelManager.playerBrain.activePowerUps.Count; i++)
        {
            if (i > puList.Count-1)
            {
                var pUObj = pUMenuObj.MakeInstance(puGrid.gameObject);
                pUObj.mainSprite.spriteName = levelManager.playerBrain.activePowerUps[levelManager.playerBrain.activatePowersCount - 1].spriteName;
                puList.Add(pUObj);
                puGrid.Reposition();
            }
        }
    }
    public void ColourSFXText()
    {
        if (sfxMuted)
        {
            sfxMuteStatusLabel.text = "[00FF09]on[-]";
        }
        else
        {
            sfxMuteStatusLabel.text = "[FF0026]off[-]";
        }
    }
    public void ColourBGMText()
    {
        if (bgmMuted)
        {
            bgmMuteStatusLabel.text = "[00FF09]on[-]";
        }
        else
        {
            bgmMuteStatusLabel.text = "[FF0026]off[-]";
        }
    }
    public void SFXMuteToggle()
    {
        sfxMuted = !sfxMuted;
        EverythingLoader.Instance.gameManager.SetSFXMuted(sfxMuted);

        ColourSFXText();
    }
    public void BGMMuteToggle()
    {
        bgmMuted = !bgmMuted;
        EverythingLoader.Instance.gameManager.SetBGMMuted(bgmMuted);
        if (!bgmMuted)
        {
            levelManager.audioManager.StopBGM();
        }
        if (bgmMuted)
        {
            levelManager.audioManager.PlayBGM();
        }
        ColourBGMText();
    }
}
