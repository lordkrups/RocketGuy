using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public bool sfxMuted;
    public UILabel sfxMuteStatusLabel;
    public bool bgmMuted;
    public UILabel bgmMuteStatusLabel;

    public UIPanel helpAboutPanel;
    public UIWidget helpInfoWidget;
    public UIWidget aboutInfoWidget;

    // Start is called before the first frame update
    void Start()
    {
        sfxMuted = EverythingLoader.Instance.gameManager.sfxMuted == 1;
        bgmMuted = EverythingLoader.Instance.gameManager.bgmMuted == 1;
        ColourSFXText();
        ColourBGMText();

        helpAboutPanel.Off();
        helpInfoWidget.Off();
        aboutInfoWidget.Off();
    }

    public void SFXMuteToggle()
    {
        sfxMuted = !sfxMuted;
        EverythingLoader.Instance.gameManager.SetSFXMuted(sfxMuted);
        ColourSFXText();
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
    public void BGMMuteToggle()
    {
        bgmMuted = !bgmMuted;
        EverythingLoader.Instance.gameManager.SetBGMMuted(bgmMuted);
        ColourBGMText();
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


    public void OpenHelp()
    {
        helpAboutPanel.On();
        helpInfoWidget.On();
    }
    public void CloseHelpAboutPanel()
    {
        helpAboutPanel.Off();
        helpInfoWidget.Off();
        aboutInfoWidget.Off();
    }
    public void OpenAbout()
    {
        helpAboutPanel.On();
        aboutInfoWidget.On();
    }
}
