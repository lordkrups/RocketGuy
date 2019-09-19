using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoPanel : MonoBehaviour
{
    public GearMenuManager gearMenuManager;
    public UISprite mainSprite;
    public UILabel nameLabel;

    public int currentGod;
    public UILabel hpLabel;
    public UILabel attackLabel;
    public UILabel weaponLabel;
    public UILabel attackSpeedLabel;
    public UILabel moveSpeedLabel;
    public UILabel costLabel;
    public UILabel descriptionLabel;

    private void Awake()
    {
        this.Off();
    }
    public void SetUp(int id)
    {
        this.On();
        currentGod = id;
        mainSprite.spriteName = EverythingLoader.Instance.PlayerInfos[id].sprite;
        nameLabel.text = EverythingLoader.Instance.PlayerInfos[id].name;
        hpLabel.text = EverythingLoader.Instance.PlayerInfos[id].startHP.ToString();
        attackLabel.text = EverythingLoader.Instance.PlayerInfos[id].atk.ToString();
        //weaponLabel.text = EverythingLoader.Instance.WeaponInfos[EverythingLoader.Instance.PlayerInfos[id].weapon].sprite;
        attackSpeedLabel.text = (2f - EverythingLoader.Instance.PlayerInfos[id].atkSpeed).ToString();
        moveSpeedLabel.text = EverythingLoader.Instance.PlayerInfos[id].moveSpeed.ToString();
        descriptionLabel.text = EverythingLoader.Instance.PlayerInfos[id].description;
        costLabel.text = "Purchased";

        if (EverythingLoader.Instance.gameManager.unlockedGodsList[currentGod] == 0)
        {
            costLabel.text = EverythingLoader.Instance.PlayerInfos[id].purchaseCost.ToString();

        }

    }
    public void Purchase()
    {
        if (EverythingLoader.Instance.gameManager.unlockedGodsList[currentGod] == 0)
        {
            gearMenuManager.PurchaseGod(currentGod);
        }
    }
    public void CloseWindow()
    {
        this.Off();
    }
}