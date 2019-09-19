using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsMenuManager : MonoBehaviour
{
    public Toast toast;
    public UILabel priceLabel;
    public bool currentlyPurchasing;
    public List<SkillsMenuObj> skillsList;

    public void UpdateSkillInfos()
    {
        for (int i = 0; i < skillsList.Count; i++)
        {
            bool unlocked = false;
            if (EverythingLoader.Instance.gameManager.unlockedSkillsList[i] != 0)
            {
                unlocked = true;
            }

            skillsList[i].Init(EverythingLoader.Instance.SkillInfos[i].id, unlocked);


        }

        for (int i = skillsList.Count; i > 0; i--)
        {
            if (EverythingLoader.Instance.gameManager.unlockedSkillsList[i-1] >= EverythingLoader.Instance.SkillInfos[i-1].multiPick)
            {
                skillsList.Remove(skillsList[i-1]);
            }
        }
        PriceUpdate();
    }

    public void PriceUpdate()
    {
        if (EverythingLoader.Instance.gameManager.totalUnlockedSkills < EverythingLoader.Instance.gameManager.persistantLevel)
        {
            priceLabel.text = (1000 * (EverythingLoader.Instance.gameManager.totalUnlockedSkills + 1)).ToString();
        }
        else
        {
            priceLabel.text = "Increase level to purchase!";
        }
    }
    public void PurchasePowerUp()
    {
        if (!currentlyPurchasing)
        {
            if (EverythingLoader.Instance.gameManager.totalUnlockedSkills < EverythingLoader.Instance.gameManager.persistantLevel)
            {
                if (EverythingLoader.Instance.gameManager.MakePurchaseWithGold(1000 * (EverythingLoader.Instance.gameManager.totalUnlockedSkills + 1)))
                {
                    currentlyPurchasing = true;

                    StartCoroutine(StartPurchase());
                }
                else
                {
                    toast.ToastSetting("Not enough gold!");
                    Debug.Log("Not enough gold!");
                }
            }
            else
            {
                toast.ToastSetting("Increase level to purchase!");
                Debug.Log("Increase level to purchase!");
            }
        }
    }

    IEnumerator StartPurchase()
    {
        currentlyPurchasing = true;
        int ran;

        for (int i = 0; i < 10; i++)
        {
            ran = Random.Range(0, skillsList.Count);
            Debug.Log("ran 1: " + ran);
            skillsList[ran].Flash(false);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.5f);

        int win = Random.Range(0, skillsList.Count);
        Debug.Log("Skill unlocked: " + EverythingLoader.Instance.SkillInfos[win].skillName);

        skillsList[win].Flash(true);
        skillsList[win].UnlockSkill();

        //Debug.Log("unlocked: " + EverythingLoader.Instance.SkillInfos[win].skillName);

        EverythingLoader.Instance.gameManager.AddUnlockedSkill(skillsList[win].skilID);

        yield return new WaitForSeconds(0.3f);

        if (EverythingLoader.Instance.gameManager.unlockedSkillsList[win] >=
            EverythingLoader.Instance.SkillInfos[skillsList[win].skilID].multiPick)
        {
            Debug.Log("Removed: " + EverythingLoader.Instance.SkillInfos[win].skillName);

            skillsList.Remove(skillsList[win]);
        }

        currentlyPurchasing = false;
        PriceUpdate();
        UpdateSkillInfos();

        //priceLabel.text = (1000 * EverythingLoader.Instance.gameManager.persistantLevel).ToString();
    }
}
