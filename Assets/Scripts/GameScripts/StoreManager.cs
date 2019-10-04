using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{

    public UILabel healthLvlLabel;
    public UILabel engineLvlLabel;
    public UILabel boosterEngineLvlLabel;
    public UILabel controlLvlLabel;
    public UILabel armourLvlLabel;
    public UILabel fuelTankLvlLabel;
    public UILabel boosterTankLvlLabel;
    public UILabel airResistanceLvlLabel;
    public UILabel nummmyLvlLabel;
    public UILabel fuelConsumptionLvlLabel;
    public UILabel boosterFuelConsumptionLvlLabel;
    public UILabel diamondValueLvlLabel;

    public int highlightedItem;
    int statLevel;

    public UILabel itemName;
    public UISprite itemImage;
    public UILabel itemLevel;
    public UILabel itemCost;
    public UILabel itemDescription;

    // Start is called before the first frame update
    public void Init()
    {
        healthLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantMaxHealth.ToString() + "/" + RocketGameManager.Instance.healthStatValues.Count.ToString();

        engineLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantEngineUpgrade.ToString() + "/" + RocketGameManager.Instance.mainEngineStatValue.Count.ToString();
        boosterEngineLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantBoosterEngineUpgrade.ToString() + "/" + RocketGameManager.Instance.boosterEngineStatValue.Count.ToString();
        controlLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantTurnSpeed.ToString() + "/" + RocketGameManager.Instance.turnSpeedStatValue.Count.ToString();
        armourLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantDamageMultiplier.ToString() + "/" + RocketGameManager.Instance.damageMultiplierStatValue.Count.ToString();
        fuelTankLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantMaxFuel.ToString() + "/" + RocketGameManager.Instance.maxFuelStatValue.Count.ToString();
        boosterTankLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantMaxBoosterFuel.ToString() + "/" + RocketGameManager.Instance.maxBoosterFuelStatValue.Count.ToString(); ;
        airResistanceLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantAirReistance.ToString() + "/" + RocketGameManager.Instance.airReistanceStatValue.Count.ToString(); ;
        nummmyLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantNummyMultiplier.ToString() + "/" + RocketGameManager.Instance.nummyMultiplierStatValue.Count.ToString(); ;
        fuelConsumptionLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantFuelConsumptionRate.ToString() + "/" + RocketGameManager.Instance.fuelConsumptionRateStatValue.Count.ToString(); ;
        boosterFuelConsumptionLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantBoosterFuelConsumptionRate.ToString() + "/" + RocketGameManager.Instance.boosterFuelConsumptionRateStatValue.Count.ToString(); ;
        diamondValueLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantDiamonValue.ToString() + "/" + RocketGameManager.Instance.diamonValueStatValue.Count.ToString(); ;

        UpdateItemInfoBox();
    }

    public void UpdateItemInfoBox()
    {
        switch (highlightedItem)
        {
            case 0:
                statLevel = RocketGameManager.Instance.persistantMaxHealth;
                break;
            case 1:
                statLevel = RocketGameManager.Instance.persistantEngineUpgrade;
                break;
            case 2:
                statLevel = RocketGameManager.Instance.persistantBoosterEngineUpgrade;
                break;
            case 3:
                statLevel = RocketGameManager.Instance.persistantTurnSpeed;
                break;
            case 4:
                statLevel = RocketGameManager.Instance.persistantDamageMultiplier;
                break;
            case 5:
                statLevel = RocketGameManager.Instance.persistantMaxFuel;
                break;
            case 6:
                statLevel = RocketGameManager.Instance.persistantMaxBoosterFuel;
                break;
            case 7:
                statLevel = RocketGameManager.Instance.persistantAirReistance;
                break;
            case 8:
                statLevel = RocketGameManager.Instance.persistantNummyMultiplier;
                break;
            case 9:
                statLevel = RocketGameManager.Instance.persistantFuelConsumptionRate;
                break;
            case 10:
                statLevel = RocketGameManager.Instance.persistantBoosterFuelConsumptionRate;
                break;
            case 11:
                statLevel = RocketGameManager.Instance.persistantDiamonValue;
                break;
        }

        //itemName.text = RocketGameManager.Instance.StoreStatsInfos[highlightedItem].name;
        itemImage.spriteName = RocketGameManager.Instance.StoreStatsInfos[highlightedItem].sprite;
        itemLevel.text = "LVL " + statLevel.ToString() + "/" + (RocketGameManager.Instance.StoreStatsInfos[highlightedItem].itemStat.Count - 1).ToString();
        if ((statLevel + 1) > RocketGameManager.Instance.StoreStatsInfos[highlightedItem].itemCost.Count)
        {
            statLevel -= 1;
        }
        itemCost.text = RocketGameManager.Instance.StoreStatsInfos[highlightedItem].itemCost[statLevel + 1].ToString();
        itemDescription.text = RocketGameManager.Instance.StoreStatsInfos[highlightedItem].description;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectItem(int item)
    {
        highlightedItem = item;
        UpdateItemInfoBox();

    }

    public void BuyItemUpgrade()
    {
        switch (highlightedItem)
        {
            case 0:
                RocketGameManager.Instance.persistantMaxHealth += 1;
                break;
            case 1:
                RocketGameManager.Instance.persistantEngineUpgrade += 1;
                break;
            case 2:
                RocketGameManager.Instance.persistantBoosterEngineUpgrade += 1;
                break;
            case 3:
                RocketGameManager.Instance.persistantTurnSpeed += 1;
                break;
            case 4:
                RocketGameManager.Instance.persistantDamageMultiplier += 1;
                break;
            case 5:
                RocketGameManager.Instance.persistantMaxFuel += 1;
                break;
            case 6:
                RocketGameManager.Instance.persistantMaxBoosterFuel += 1;
                break;
            case 7:
                RocketGameManager.Instance.persistantAirReistance += 1;
                break;
            case 8:
                RocketGameManager.Instance.persistantNummyMultiplier += 1;
                break;
            case 9:
                RocketGameManager.Instance.persistantFuelConsumptionRate += 1;
                break;
            case 10:
                RocketGameManager.Instance.persistantBoosterFuelConsumptionRate += 1;
                break;
            case 11:
                RocketGameManager.Instance.persistantDiamonValue += 1;
                break;
        }
        UpdateItemInfoBox();
    }
}
