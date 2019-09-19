using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatInfoPanel : MonoBehaviour
{
    public GearMenuManager gearMenuManager;
    public UISprite mainSprite;
    public UILabel nameLabel;

    public int currentHat;
    public UILabel effectLabelOne;
    public UILabel effectLabelTwo;
    public UILabel costLabel;
    public UILabel descriptionLabel;

    private void Awake()
    {
        this.Off();
    }
    public void SetUp(int id)
    {
        this.On();
        currentHat = id;
        mainSprite.spriteName = EverythingLoader.Instance.HatInfos[id].sprite;
        nameLabel.text = EverythingLoader.Instance.HatInfos[id].name;

        //effectLabelOne.text = EverythingLoader.Instance.PlayerInfos[id].startHP.ToString();
        //effectLabelTwo.text = EverythingLoader.Instance.PlayerInfos[id].atk.ToString();

        //costLabel.text = EverythingLoader.Instance.PlayerInfos[id].purchase_cost.ToString();

        descriptionLabel.text = EverythingLoader.Instance.HatInfos[id].powerDesc;
        costLabel.text = "Purchased";

        if (EverythingLoader.Instance.gameManager.unlockedHatsList[currentHat] == 0)
        {
            costLabel.text = EverythingLoader.Instance.HatInfos[id].purchaseCost.ToString();

        }
    }
    public void Purchase()
    {
        if (EverythingLoader.Instance.gameManager.unlockedHatsList[currentHat] == 0)
        {
            gearMenuManager.PurchaseHat(currentHat);

        }
    }
    public void CloseWindow()
    {
        this.Off();
    }
}
