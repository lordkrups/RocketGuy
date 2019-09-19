using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMenuCharacterObj : MonoBehaviour
{
    public bool locked;
    public int characterID;
    public UISprite mainSprite;
    public UISprite lockedSprite;
    public UILabel nameLabel;

    public void Init()
    {
        mainSprite.spriteName = EverythingLoader.Instance.PlayerInfos[characterID].sprite;
        nameLabel.text = EverythingLoader.Instance.PlayerInfos[characterID].name;

        locked = true;
        mainSprite.Off();
        lockedSprite.On();
    }
    public void Unlock()
    {
        mainSprite.On();
        lockedSprite.Off();
    }
}
