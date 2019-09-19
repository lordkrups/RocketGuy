using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfoPanel : MonoBehaviour
{
    public GearMenuManager gearMenuManager;
    public UISprite mainSprite;
    public UILabel nameLabel;

    public int currentWeapon, arrayID;
    public UILabel effectLabelOne;
    public UILabel effectLabelTwo;
    public UILabel costLabel;
    public UILabel descriptionLabel;

    private void Awake()
    {
        this.Off();
    }
    public void SetUp(int id, int aid = 0)
    {
        this.On();
        currentWeapon = id;
        arrayID = aid;
        mainSprite.spriteName = EverythingLoader.Instance.WeaponInfos[currentWeapon].sprite;
        nameLabel.text = EverythingLoader.Instance.WeaponInfos[currentWeapon].name;

        //effectLabelOne.text = EverythingLoader.Instance.PlayerInfos[id].startHP.ToString();
        //effectLabelTwo.text = EverythingLoader.Instance.PlayerInfos[id].atk.ToString();
        costLabel.text = "Purchased";

        if (EverythingLoader.Instance.gameManager.unlockedWeaponsList[arrayID] == 0)
        {
            costLabel.text = EverythingLoader.Instance.WeaponInfos[currentWeapon].purchaseCost.ToString();

        }
        //costLabel.text = EverythingLoader.Instance.PlayerInfos[id].purchase_cost.ToString();

        descriptionLabel.text = EverythingLoader.Instance.WeaponInfos[currentWeapon].name;

    }
    public void Purchase()
    {
        if (EverythingLoader.Instance.gameManager.unlockedWeaponsList[arrayID] == 0)
        {
            gearMenuManager.PurchaseWeapon(currentWeapon, arrayID);
        }
    }
    public void CloseWindow()
    {
        this.Off();
    }
}