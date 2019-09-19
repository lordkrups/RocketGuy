using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsMenuObj : MonoBehaviour
{
    public UISprite infoSprite;
    public UILabel infoLabel;

    public UILabel skillLevelLabel;
    public UILabel mainLabel;
    public UISprite mainSprite;

    public UISprite blindSprite;
    public UISprite highlightSprite;

    public bool unlocked;
    public int skilID;


    public void Init(int sid, bool unlocked)
    {
        skilID = sid;
        blindSprite.On();
        highlightSprite.Off();

        infoSprite.Off();
        infoLabel.text = EverythingLoader.Instance.SkillInfos[sid].skillDesc;

        skillLevelLabel.text = EverythingLoader.Instance.gameManager.unlockedSkillsList[sid].ToString();
        mainSprite.spriteName = EverythingLoader.Instance.SkillInfos[sid].sprite;
        mainLabel.text = EverythingLoader.Instance.SkillInfos[sid].skillName;
        infoLabel.text = EverythingLoader.Instance.SkillInfos[sid].skillDesc;

        if (unlocked)
        {
            blindSprite.Off();
        }
    }
    public void UnlockSkill()
    {
        blindSprite.Off();
    }
    public void ShowInfo()
    {
        StartCoroutine(ShowInfoForTime());
    }
    public void Flash(bool finalPick)
    {
        StartCoroutine(FlashForTime(finalPick));
    }
    IEnumerator ShowInfoForTime()
    {
        infoSprite.On();

        yield return new WaitForSeconds(2f);

        infoSprite.Off();
    }
    IEnumerator FlashForTime(bool finalPick)
    {
        highlightSprite.On();
        yield return new WaitForSeconds(0.2f);

        if (finalPick)
        {
            yield return new WaitForSeconds(0.8f);
        }

        highlightSprite.Off();
    }
}
