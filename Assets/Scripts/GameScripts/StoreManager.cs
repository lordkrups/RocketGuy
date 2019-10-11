using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public Toast toast;
    public UILabel playerMoneyLabel;

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
        playerMoneyLabel.text = RocketGameManager.Instance.persistantPlayerCoins.ToString();

        healthLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantMaxHealth.ToString() + "/" + (RocketGameManager.Instance.healthStatValues.Count-1).ToString();
        engineLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantEngineUpgrade.ToString() + "/" + (RocketGameManager.Instance.mainEngineStatValue.Count-1).ToString();
        boosterEngineLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantBoosterEngineUpgrade.ToString() + "/" + (RocketGameManager.Instance.boosterEngineStatValue.Count-1).ToString();
        controlLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantTurnSpeed.ToString() + "/" + (RocketGameManager.Instance.turnSpeedStatValue.Count-1).ToString();
        armourLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantDamageMultiplier.ToString() + "/" + (RocketGameManager.Instance.damageMultiplierStatValue.Count-1).ToString();
        fuelTankLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantMaxFuel.ToString() + "/" + (RocketGameManager.Instance.maxFuelStatValue.Count-1).ToString();
        boosterTankLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantMaxBoosterFuel.ToString() + "/" + (RocketGameManager.Instance.maxBoosterFuelStatValue.Count-1).ToString(); 
        airResistanceLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantAirReistance.ToString() + "/" + (RocketGameManager.Instance.airReistanceStatValue.Count-1).ToString(); 
        nummmyLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantNummyMultiplier.ToString() + "/" + (RocketGameManager.Instance.nummyMultiplierStatValue.Count-1).ToString(); 
        fuelConsumptionLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantFuelConsumptionRate.ToString() + "/" + (RocketGameManager.Instance.fuelConsumptionRateStatValue.Count-1).ToString(); 
        boosterFuelConsumptionLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantBoosterFuelConsumptionRate.ToString() + "/" + (RocketGameManager.Instance.boosterFuelConsumptionRateStatValue.Count-1).ToString(); 
        diamondValueLvlLabel.text = "Lv: " + RocketGameManager.Instance.persistantDiamonValue.ToString() + "/" + (RocketGameManager.Instance.diamonValueStatValue.Count-1).ToString(); 

        UpdateItemInfoBox();
    }

    public void UpdateItemInfoBox()
    {
        playerMoneyLabel.text = RocketGameManager.Instance.persistantPlayerCoins.ToString();

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

        itemName.text = RocketGameManager.Instance.StoreStatsInfos[highlightedItem].statname;
        itemImage.spriteName = RocketGameManager.Instance.StoreStatsInfos[highlightedItem].sprite;
        itemLevel.text = "LVL " + statLevel.ToString() + "/" + (RocketGameManager.Instance.StoreStatsInfos[highlightedItem].itemStat.Count-1).ToString();

        itemCost.text = "Maxed Out";

        if ((statLevel) < (RocketGameManager.Instance.StoreStatsInfos[highlightedItem].itemCost.Count - 1))
        {
            itemCost.text = RocketGameManager.Instance.StoreStatsInfos[highlightedItem].itemCost[statLevel + 1].ToString();
        }
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

    public bool MakeStorePurchase()
    {
        bool canBuy = false;



        return canBuy;
    }

    public void BuyItemUpgrade()
    {
            switch (highlightedItem)
            {
                case 0:
                    if (RocketGameManager.Instance.persistantMaxHealth < RocketGameManager.Instance.healthStatValues.Count - 1)
                    {
                        if (RocketGameManager.Instance.PurchaseWithCoins(RocketGameManager.Instance.StoreStatsInfos[highlightedItem].itemCost[statLevel + 1]))
                        {
                            RocketGameManager.Instance.persistantMaxHealth += 1;
                        }
                        else
                        {
                            toast.ToastSetting("Not enough coins!");
                        }
                    }
                    break;
                case 1:
                    if (RocketGameManager.Instance.persistantEngineUpgrade < RocketGameManager.Instance.mainEngineStatValue.Count - 1)
                    {
                        if (RocketGameManager.Instance.PurchaseWithCoins(RocketGameManager.Instance.StoreStatsInfos[highlightedItem].itemCost[statLevel + 1]))
                        {
                            RocketGameManager.Instance.persistantEngineUpgrade += 1;
                        }
                        else
                        {
                            toast.ToastSetting("Not enough coins!");
                        }
                    }
                break;
                case 2:
                    if (RocketGameManager.Instance.persistantBoosterEngineUpgrade < RocketGameManager.Instance.boosterEngineStatValue.Count - 1)
                    {
                        if (RocketGameManager.Instance.PurchaseWithCoins(RocketGameManager.Instance.StoreStatsInfos[highlightedItem].itemCost[statLevel + 1]))
                        {
                            RocketGameManager.Instance.persistantBoosterEngineUpgrade += 1;
                        }
                        else
                        {
                            toast.ToastSetting("Not enough coins!");
                        }
                    }
                break;
                case 3:
                    if (RocketGameManager.Instance.persistantTurnSpeed < RocketGameManager.Instance.turnSpeedStatValue.Count - 1)
                    {
                        if (RocketGameManager.Instance.PurchaseWithCoins(RocketGameManager.Instance.StoreStatsInfos[highlightedItem].itemCost[statLevel + 1]))
                        {
                            RocketGameManager.Instance.persistantTurnSpeed += 1;
                        }
                    else
                        {
                            toast.ToastSetting("Not enough coins!");
                        }
                    }
                break;
                case 4:
                    if (RocketGameManager.Instance.persistantDamageMultiplier < RocketGameManager.Instance.damageMultiplierStatValue.Count - 1)
                    {
                        if (RocketGameManager.Instance.PurchaseWithCoins(RocketGameManager.Instance.StoreStatsInfos[highlightedItem].itemCost[statLevel + 1]))
                        {
                            RocketGameManager.Instance.persistantDamageMultiplier += 1;
                        }
                        else
                        {
                            toast.ToastSetting("Not enough coins!");
                        }
                    }
                break;
                case 5:
                    if (RocketGameManager.Instance.persistantMaxFuel < RocketGameManager.Instance.maxFuelStatValue.Count - 1)
                    {
                        if (RocketGameManager.Instance.PurchaseWithCoins(RocketGameManager.Instance.StoreStatsInfos[highlightedItem].itemCost[statLevel + 1]))
                        {
                            RocketGameManager.Instance.persistantMaxFuel += 1;
                        }
                        else
                        {
                            toast.ToastSetting("Not enough coins!");
                        }
                    }
                    break;
                case 6:
                    if (RocketGameManager.Instance.persistantMaxBoosterFuel < RocketGameManager.Instance.maxBoosterFuelStatValue.Count - 1)
                    {
                        if (RocketGameManager.Instance.PurchaseWithCoins(RocketGameManager.Instance.StoreStatsInfos[highlightedItem].itemCost[statLevel + 1]))
                        {
                            RocketGameManager.Instance.persistantMaxBoosterFuel += 1;
                        }
                        else
                        {
                            toast.ToastSetting("Not enough coins!");
                        }
                    }
                    break;
                case 7:
                    if (RocketGameManager.Instance.persistantAirReistance < RocketGameManager.Instance.airReistanceStatValue.Count - 1)
                    {
                        if (RocketGameManager.Instance.PurchaseWithCoins(RocketGameManager.Instance.StoreStatsInfos[highlightedItem].itemCost[statLevel + 1]))
                        {
                            RocketGameManager.Instance.persistantAirReistance += 1;
                        }
                        else
                        {
                            toast.ToastSetting("Not enough coins!");
                        }
                    }
                    break;
                case 8:
                    if (RocketGameManager.Instance.persistantNummyMultiplier < RocketGameManager.Instance.nummyMultiplierStatValue.Count - 1)
                    {
                        if (RocketGameManager.Instance.PurchaseWithCoins(RocketGameManager.Instance.StoreStatsInfos[highlightedItem].itemCost[statLevel + 1]))
                        {
                            RocketGameManager.Instance.persistantNummyMultiplier += 1;
                        }
                        else
                        {
                            toast.ToastSetting("Not enough coins!");
                        }
                    }
                    break;
                case 9:
                    if (RocketGameManager.Instance.persistantFuelConsumptionRate < RocketGameManager.Instance.fuelConsumptionRateStatValue.Count - 1)
                    {
                        if (RocketGameManager.Instance.PurchaseWithCoins(RocketGameManager.Instance.StoreStatsInfos[highlightedItem].itemCost[statLevel + 1]))
                        {
                            RocketGameManager.Instance.persistantFuelConsumptionRate += 1;
                        }
                        else
                        {
                            toast.ToastSetting("Not enough coins!");
                        }
                    }
                    break;
                case 10:
                    if (RocketGameManager.Instance.persistantBoosterFuelConsumptionRate < RocketGameManager.Instance.boosterFuelConsumptionRateStatValue.Count - 1)
                    {
                        if (RocketGameManager.Instance.PurchaseWithCoins(RocketGameManager.Instance.StoreStatsInfos[highlightedItem].itemCost[statLevel + 1]))
                        {
                            RocketGameManager.Instance.persistantBoosterFuelConsumptionRate += 1;
                        }
                        else
                        {
                            toast.ToastSetting("Not enough coins!");
                        }
                    }
                    break;
                case 11:
                    if (RocketGameManager.Instance.persistantDiamonValue < RocketGameManager.Instance.diamonValueStatValue.Count - 1)
                    {
                        if (RocketGameManager.Instance.PurchaseWithCoins(RocketGameManager.Instance.StoreStatsInfos[highlightedItem].itemCost[statLevel + 1]))
                        {
                            RocketGameManager.Instance.persistantDiamonValue += 1;
                        }
                        else
                        {
                            toast.ToastSetting("Not enough coins!");
                        }
                    }
                    break;
            }
        Init();
        //UpdateItemInfoBox();
    }
}
