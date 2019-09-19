using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuManager : MonoBehaviour
{

    public int playGameEnergyCost = 5;
    public GameObject[] menuPanels;
    public GearMenuManager gearMenu;
    public SkillsMenuManager skillsMenuManager;
    public TimeRewardManager timeMenu;
    public UISprite timeMenuButtonBlind;

    public int currentFocus = 2;
    public int timeCoinReward;

    public UIScrollBar menuScrollBar;
    public UIScrollView menuScrollView;
    public UISlider bottomSlider;

    public UISprite[] bottomButtons;

    public UILabel weaponSelectLable;

    public UILabel persistantLevelLabel;
    public UISlider expSlider;
    public UISlider energySlider;
    public UILabel persistantEnergyabel;
    public UILabel persistantGoldLabel;
    public UILabel persistantGemLabel;


    public int diffmonth;
    public int diffday;
    public int diffhour;
    public int diffmin;

    public int tday;
    public int thour;
    public int tmin;

    public int totalTime;

    // Start is called before the first frame update
    void Start()
    {
        ChangeMenuWidget(currentFocus);
        UpdateTopContainerValues();
        gearMenu.Init();

    }
    void Update()
    {
        if (EverythingLoader.Instance.gameManager.updateLevels || EverythingLoader.Instance.gameManager.updateGold ||
            EverythingLoader.Instance.gameManager.updateGems || EverythingLoader.Instance.gameManager.updateEnergy)
        {
            UpdateTopContainerValues();
            EverythingLoader.Instance.gameManager.FinishedTopContainerUpdate();
        }

    }
    public void CollectTimeReward()
    {
        EverythingLoader.Instance.gameManager.DepositGold(timeCoinReward);
        timeCoinReward = 0;
        EverythingLoader.Instance.gameManager.SaveTimeRewardTimes();
        timeMenu.ClosePanel();
    }
    public void OpenTimeReward()
    {
        if (EverythingLoader.Instance.gameManager.totalTimeRewardUnlocks > 0)
        {
            diffmonth = DateTime.Now.Month - EverythingLoader.Instance.gameManager.monthGoldReward;
            Debug.Log("month: " + diffmonth);
            diffday = Mathf.Abs(DateTime.Now.Day - EverythingLoader.Instance.gameManager.dayGoldReward);
            Debug.Log("day: " + diffday);
            diffhour = DateTime.Now.Hour - EverythingLoader.Instance.gameManager.hourGoldReward;
            Debug.Log("hour: " + diffhour);
            diffmin = DateTime.Now.Minute - EverythingLoader.Instance.gameManager.minGoldReward;
            Debug.Log("min: " + diffmin);

            if (diffmonth > 0)
            {
                diffday = diffday - 30;
            }

            totalTime = ((diffday * (60 * 24)) + (diffhour * 60) + diffmin);
            Debug.Log("totalTime: " + totalTime);

            if (totalTime > 4320)
            {
                totalTime = 4320;
            }

            tday = Mathf.Abs(totalTime / 1440);
            
            Debug.Log("tday: " + tday);

            thour = Mathf.Abs(totalTime / 60);
            Debug.Log("thour: " + thour);

            tmin = Mathf.Abs((totalTime - (diffday * (60 * 24))) - (diffhour * 60));

            timeCoinReward = Mathf.Abs((totalTime / 10) * EverythingLoader.Instance.gameManager.totalTimeRewardUnlocks);

            timeMenu.SetTimeReward(0, 0, thour, tmin, timeCoinReward, 0);
        }

    }
    public void UpdateTopContainerValues()
    {
        //expSlider.value = (EverythingLoader.Instance.gameManager.persistantExp / 100f) * EverythingLoader.Instance.playerStats.GetNextLevelExp();
        expSlider.value = EverythingLoader.Instance.gameManager.persistantExp / (float)EverythingLoader.Instance.playerStats.GetNextLevelExp(EverythingLoader.Instance.gameManager.persistantLevel);
        energySlider.value = EverythingLoader.Instance.gameManager.persistantEnergy / 20f;
        persistantLevelLabel.text = EverythingLoader.Instance.gameManager.persistantLevel.ToString();
        persistantEnergyabel.text = EverythingLoader.Instance.gameManager.persistantEnergy.ToString();
        persistantGoldLabel.text = EverythingLoader.Instance.gameManager.persistantGold.ToString();
        persistantGemLabel.text = EverythingLoader.Instance.gameManager.persistantGems.ToString();
    }
    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(0.1f);
    }

    public void SetWeapon(int wid)
    {
        PlayerPrefs.SetInt("selectedWeaponID", wid);
        Debug.Log("SetWeapon: " + wid);
        EverythingLoader.Instance.SetWeapon(wid);
        if (wid == 1)
        {
            //EverythingLoader.Instance.playerStats.Weapon = wid;
            //EverythingLoader.Instance.SetWeapon(wid);
            weaponSelectLable.text = ("CURRENT: LASER");
        }
        if (wid == 22)
        {
            //EverythingLoader.Instance.playerStats.Weapon = wid;

            weaponSelectLable.text = ("CURRENT: MISSILE");
        }
        if (wid == 30)
        {

            //EverythingLoader.Instance.playerStats.Weapon = wid;

            weaponSelectLable.text = ("CURRENT: BOOMERANG");
        }
    }
    public void UpdateOnChange()
    {
        if (EverythingLoader.Instance.gameManager.totalTimeRewardUnlocks == 0)
        {
            timeMenuButtonBlind.On();
        }
        else
        {
            timeMenuButtonBlind.Off();
        }
    }
    // Update is called once per frame
    public void ChangeMenuWidget(int changeTo)
    {
        //menuScrollBar.ste
        float newPos = 0.5f;

        switch (changeTo)
        {
            case 0:
                newPos = 0f;
                newPos = 0f;
                currentFocus = 0;
                break;
            case 1:
                newPos = 0.25f;
                newPos = 0.25f;
                currentFocus = 1;
                break;
            case 2:
                newPos = 0.5f;
                newPos = 0.5f;
                currentFocus = 2;
                break;
            case 3:
                newPos = 0.75f;
                newPos = 0.75f;
                skillsMenuManager.UpdateSkillInfos();
                currentFocus = 3;
                break;
            case 4:
                newPos = 1f;
                newPos = 1f;
                currentFocus = 4;
                break;
            default:
                break;
        }

        //Maybe do this in update, by adding small amount to smoothly move the scroll view.
        //Use the current focus :D
        //menuScrollView.SetDragAmount(newPos, 0, true);

        menuScrollBar.value = newPos;
        bottomSlider.value = newPos;
        UpdateOnChange();
        UpdateTopContainerValues();

    }

    public void WorldOneLoad()
    {
        if (EverythingLoader.Instance.gameManager.UseEnergy(5))
        {
            SceneManager.LoadScene("GameScene");
        }
    }

}
