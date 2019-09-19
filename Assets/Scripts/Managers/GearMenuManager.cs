using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMenuManager : MonoBehaviour
{
    public Toast toast;
    public MenuManager menuManager;
    public CharacterStorePanel characterStorePanel;
    public CharacterInfoPanel characterInfoPanel;
    public HatStorePanel hatStorePanel;
    public HatInfoPanel hatInfoPanel;
    public WeaponStorePanel weaponStorePanel;
    public WeaponInfoPanel weaponInfoPanel;

    public int currentGod;
    public int currentHat;
    public int currentWeapon;
    public UISprite characterSprite;
    public UILabel nameLabel;
    public UISprite hatSprite;
    public UISprite weaponSprite;

    public void Init()
    {

		UpdateGodInfo();
		UpdateHatinfo();
		UpdateWeaponsinfo();
		characterStorePanel.Init();
		hatStorePanel.Init();
		weaponStorePanel.Init();
	}
    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(0.5f);


    }


    public void OpenCharacterStore()
    {
        characterStorePanel.On();

    }
    public void CloseCharacterStore()
    {
        characterStorePanel.Off();
        characterInfoPanel.Off();
    }
    public void ShowCharacterInfo(int cID)
    {
        characterInfoPanel.SetUp(cID);
        characterInfoPanel.On();
    }
    public void CloseCharacterInfo()
    {
        characterInfoPanel.Off();
        Init();
    }

    public void OpenHatStore()
    {
        hatStorePanel.On();

    }
    public void CloseHatStore()
    {
        hatStorePanel.Off();
        hatInfoPanel.Off();
    }
    public void ShowHatInfo(int hID)
    {
        hatInfoPanel.SetUp(hID);
        hatInfoPanel.On();
    }
    public void CloseHatInfo()
    {
        hatInfoPanel.Off();
        Init();
    }

    public void OpenWeaponsStore()
    {
        weaponStorePanel.On();
        //weaponStorePanel.grid.Reposition();
    }
    public void CloseWeaponsStore()
    {
        weaponStorePanel.Off();
        weaponInfoPanel.Off();
    }
    public void ShowWeaponsInfo(int wID, int aID)
    {
        weaponInfoPanel.SetUp(wID, aID);
        //weaponInfoPanel.On();
    }
    public void CloseWeaponsInfo()
    {
        weaponInfoPanel.Off();
        //Init();
    }


    public void UpdateGodInfo()
    {
        currentGod = EverythingLoader.Instance.gameManager.currentGod;
        characterSprite.spriteName = EverythingLoader.Instance.playerStats.PlayerSprite;
        nameLabel.text = EverythingLoader.Instance.playerStats.Name;
    }
    public void UpdateHatinfo()
    {
        currentHat = EverythingLoader.Instance.gameManager.currentHat;
        hatSprite.spriteName = EverythingLoader.Instance.HatInfos[currentHat].sprite;
        //EverythingLoader.Instance.HatInfos[currentHat].sprite;
    }
    public void UpdateWeaponsinfo()
    {
        currentWeapon = EverythingLoader.Instance.gameManager.currentWeapon;
        weaponSprite.spriteName = EverythingLoader.Instance.WeaponInfos[currentWeapon].sprite;
        weaponStorePanel.UpdatePurchase(currentWeapon);
        //EverythingLoader.Instance.HatInfos[currentHat].sprite;
    }

    public void PurchaseGod(int god)
    {

        if (EverythingLoader.Instance.gameManager.MakePurchaseWithGold(EverythingLoader.Instance.PlayerInfos[god].purchaseCost))
        {
            //Something to purchase that character
            EverythingLoader.Instance.gameManager.SetCurrentGod(god);
            UpdateGodInfo();
            CloseCharacterInfo();
            CloseCharacterStore();
            menuManager.UpdateTopContainerValues();

        }
        else
        {
            toast.ToastSetting("Not enough gold!");
            Debug.Log("Not enough gold!");
        }
    }
    public void PurchaseHat(int hat)
    {
        if (EverythingLoader.Instance.gameManager.MakePurchaseWithGold(EverythingLoader.Instance.HatInfos[hat].purchaseCost))
        {
            //Something to purchase that character
            EverythingLoader.Instance.gameManager.SetCurrentHat(hat);
            UpdateHatinfo();
            CloseHatInfo();
            CloseHatStore();
            menuManager.UpdateTopContainerValues();

        }
        else
        {
            toast.ToastSetting("Not enough gold!");
            Debug.Log("Not enough gold!");
        }
    }
    public void PurchaseWeapon(int wID, int aID)
    {

        if (EverythingLoader.Instance.gameManager.MakePurchaseWithGold(EverythingLoader.Instance.WeaponInfos[wID].purchaseCost))
        {
            //Something to purchase that character
            EverythingLoader.Instance.gameManager.SetCurrentWeapon(wID, aID);
            UpdateWeaponsinfo();
            CloseWeaponsInfo();
            CloseWeaponsStore();
            menuManager.UpdateTopContainerValues();
        }
        else
        {
            toast.ToastSetting("Not enough gold!");
            Debug.Log("Not enough gold!");
        }
    }
}
