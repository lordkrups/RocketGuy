using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStoreObj : MonoBehaviour
{
    public int characterID;
    public UISprite mainSprite;
    public UISprite lockedSprite;
    public UILabel nameLabel;
    public UILabel priceLabel;

    public void Init(int id, bool unlocked)
    {
        characterID = id;
        mainSprite.spriteName = EverythingLoader.Instance.PlayerInfos[id].sprite;
        lockedSprite.spriteName = mainSprite.spriteName + "_dead";
        nameLabel.text = EverythingLoader.Instance.PlayerInfos[id].name;
        priceLabel.text = EverythingLoader.Instance.PlayerInfos[id].purchaseCost.ToString();
        if (unlocked)
        {
            mainSprite.On();
            lockedSprite.Off();
            priceLabel.text = "Purchased";
        }
    }
}
