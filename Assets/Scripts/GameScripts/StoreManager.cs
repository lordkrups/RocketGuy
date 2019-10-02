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

    // Start is called before the first frame update
    void Start()
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

    }

    // Update is called once per frame
    void Update()
    {
        //RocketGameManager.Instance.StoreStatsInfos[0].name
    }
}
