using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpObj : MonoBehaviour
{
    public PowerUp powerUpStats;

    public int id;
    public int multiPick;
    public string powerTitle;
    public string spriteName;
    public string powerDescription;
    public float atkIncrease;
    public int atkSpdIncrease;
    public float critIncrease;
    public int hpBoost;
    public int healPerc;
    public bool rage;
    public bool strongHeart;
    public bool bloodThirst;
    public bool invincibility;
    public int invincibleTime;
    public bool smart;
    public bool ricochet;
    public bool bouncyWall;
    public bool piercing;
    public bool multiArrow;
    public bool frontArrow;
    public bool diagonalArrow;
    public bool sideArrow;
    public bool rearArrow;




    public void Init(PowerUp pu)
    {
        id = pu.id;
        multiPick = pu.multiPick;
        powerTitle = pu.powerName;
        spriteName = pu.sprite;
        powerDescription = pu.powerDesc;
        atkIncrease = pu.atkInc;
        atkSpdIncrease = pu.atkSpdInc;
        critIncrease = pu.critInc;
        hpBoost = pu.hpInc;
        healPerc = pu.healPerc;
        rage = (EverythingLoader.Instance.PowerUpInfos[id].rage != 0);
        bloodThirst = (EverythingLoader.Instance.PowerUpInfos[id].bloodthirst != 0);
        invincibility = (EverythingLoader.Instance.PowerUpInfos[id].invincible != 0);
        invincibleTime = pu.invincible;
        smart = (EverythingLoader.Instance.PowerUpInfos[id].smart != 0);

    }
}
