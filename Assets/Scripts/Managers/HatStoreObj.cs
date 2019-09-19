using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatStoreObj : MonoBehaviour
{
    public int hatID;
    public UISprite mainSprite;
    public UISprite lockedSprite;
    public UILabel nameLabel;
    public UILabel priceLabel;


    public void Init(int id, bool unlocked)
    {
        hatID = id;

        mainSprite.spriteName = EverythingLoader.Instance.HatInfos[id].sprite;
        lockedSprite.spriteName = "plus";
        nameLabel.text = EverythingLoader.Instance.HatInfos[id].name;
        priceLabel.text = EverythingLoader.Instance.HatInfos[id].purchaseCost.ToString();
        if (unlocked)
        {
            mainSprite.On();
            lockedSprite.Off();
            priceLabel.text = "Purcahsed";
        }
    }
}
