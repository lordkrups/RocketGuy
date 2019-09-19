using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStoreObj : MonoBehaviour
{
    public GearMenuManager gearMenuManager;
    public int weaponID, arrayID;
    public UISprite mainSprite;
    public UISprite lockedSprite;
    public UILabel nameLabel;
    public UILabel priceLabel;


    public void Init(int wID, int aID, bool unlocked, GearMenuManager gm)
    {
        gearMenuManager = gm;
        weaponID = wID;
        arrayID = aID;
        mainSprite.spriteName = EverythingLoader.Instance.WeaponInfos[wID].sprite;
        //mainSprite.spriteName = EverythingLoader.Instance.WeaponInfos.;
        lockedSprite.spriteName = "plus";
        nameLabel.text = EverythingLoader.Instance.WeaponInfos[wID].name;
        priceLabel.text = EverythingLoader.Instance.WeaponInfos[wID].purchaseCost.ToString();
        if (unlocked)
        {
            mainSprite.On();
            lockedSprite.Off();
            priceLabel.text = "Purchased";
        }
    }
    public void Unlock()
    {
        mainSprite.On();
        lockedSprite.Off();
        priceLabel.text = "Purchased";
    }
    public void ShowWeaponInfo(int wID, int aID)
    {
        gearMenuManager.ShowWeaponsInfo(wID, aID);
    }
}
