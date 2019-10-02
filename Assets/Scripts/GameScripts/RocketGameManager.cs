using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketGameManager : MonoBehaviour
{

    public static RocketGameManager Instance = null;

    public void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);    // Ensures that there aren't multiple Singletons

        Instance = this;
        DontDestroyOnLoad(this);
        Init();
    }
    public Dictionary<int, StoreStats> StoreStatsInfos { get; private set; }

    public int persistantMaxHealth;
    public int persistantEngineUpgrade;
    public int persistantBoosterEngineUpgrade;
    public int persistantTurnSpeed;
    public int persistantDamageMultiplier;
    public int persistantMaxFuel;
    public int persistantMaxBoosterFuel;
    public int persistantAirReistance;
    public int persistantFuelConsumptionRate;
    public int persistantBoosterFuelConsumptionRate;
    public int persistantNummyMultiplier;
    public int persistantDiamonValue;

    public List<int> healthStatValues;
    public List<int> healthStatCost;

    public List<float> mainEngineStatValue;
    public List<int> mainEngineCost;

    public List<float> boosterEngineStatValue;
    public List<int> boosterEngineCost;

    public List<float> turnSpeedStatValue;
    public List<int> turnSpeedCost;

    public List<float> damageMultiplierStatValue;
    public List<int> damageMultiplierCost;

    public List<float> maxFuelStatValue;
    public List<int> maxFuelCost;

    public List<float> maxBoosterFuelStatValue;
    public List<int> maxBoosterFuelCost;

    public List<float> airReistanceStatValue;
    public List<int> airReistanceCost;

    public List<float> fuelConsumptionRateStatValue;
    public List<int> fuelConsumptionRateCost;

    public List<float> boosterFuelConsumptionRateStatValue;
    public List<int> boosterFuelConsumptionRateCost;

    public List<float> nummyMultiplierStatValue;
    public List<int> nummyMultiplierCost;

    public List<float> diamonValueStatValue;
    public List<int> diamonValueCost;


    // Start is called before the first frame update
    void Init()
    {

        Debug.Log("LOAD XMLs");
        StoreStatsLoader storeStatsLoader = new StoreStatsLoader();
        StoreStatsInfos = storeStatsLoader.GetDict();

        RetrievePersistatStats();

    }
    private void SavePersistatStats()
    {
        PlayerPrefs.SetInt("persistantMaxHealth", persistantMaxHealth);
        PlayerPrefs.SetInt("persistantEngineUpgrade", persistantEngineUpgrade);
        PlayerPrefs.SetInt("persistantBoosterEngineUpgrade", persistantBoosterEngineUpgrade);
        PlayerPrefs.SetInt("persistantTurnSpeed", persistantTurnSpeed);
        PlayerPrefs.SetInt("persistantDamageMultiplier", persistantDamageMultiplier);
        PlayerPrefs.SetInt("persistantMaxFuel", persistantMaxFuel);
        PlayerPrefs.SetInt("persistantMaxBoosterFuel", persistantMaxBoosterFuel);
        PlayerPrefs.SetInt("persistantAirReistance", persistantAirReistance);
        PlayerPrefs.SetInt("persistantFuelConsumptionRate", persistantFuelConsumptionRate);
        PlayerPrefs.SetInt("persistantBoosterFuelConsumptionRate", persistantBoosterFuelConsumptionRate);
        PlayerPrefs.SetInt("persistantNummyMultiplier", persistantNummyMultiplier);
        PlayerPrefs.SetInt("persistantDiamonValue", persistantDiamonValue);
    }
    private void RetrievePersistatStats()
    {
        PlayerPrefs.GetInt("persistantMaxHealth", persistantMaxHealth);
        PlayerPrefs.GetInt("persistantEngineUpgrade", persistantEngineUpgrade);
        PlayerPrefs.GetInt("persistantBoosterEngineUpgrade", persistantBoosterEngineUpgrade);
        PlayerPrefs.GetInt("persistantTurnSpeed", persistantTurnSpeed);
        PlayerPrefs.GetInt("persistantDamageMultiplier", persistantDamageMultiplier);
        PlayerPrefs.GetInt("persistantMaxFuel", persistantMaxFuel);
        PlayerPrefs.GetInt("persistantMaxBoosterFuel", persistantMaxBoosterFuel);
        PlayerPrefs.GetInt("persistantAirReistance", persistantAirReistance);
        PlayerPrefs.GetInt("persistantFuelConsumptionRate", persistantFuelConsumptionRate);
        PlayerPrefs.GetInt("persistantBoosterFuelConsumptionRate", persistantBoosterFuelConsumptionRate);
        PlayerPrefs.GetInt("persistantNummyMultiplier", persistantNummyMultiplier);
        PlayerPrefs.GetInt("persistantDiamonValue", persistantDiamonValue);


        int[] hpVals = Array.ConvertAll(StoreStatsInfos[0].stat.Split(','), int.Parse);
        int[] hpCosts = Array.ConvertAll(StoreStatsInfos[0].cost.Split(','), int.Parse);
        for (int x = 0; x < hpVals.Length; x++)
        {
            //Debug.Log("unlockedGods 1");

            healthStatValues.Add(hpVals[x]);
            healthStatCost.Add(hpCosts[x]);
        }
        
        float[] meVals = Array.ConvertAll(StoreStatsInfos[1].stat.Split(','), float.Parse);
        int[] meCost = Array.ConvertAll(StoreStatsInfos[1].cost.Split(','), int.Parse);
        for (int x = 0; x < meVals.Length; x++)
        {
            //Debug.Log("unlockedGods 1");

            mainEngineStatValue.Add(meVals[x]);
            mainEngineCost.Add(meCost[x]);
        }

        float[] beVals = Array.ConvertAll(StoreStatsInfos[2].stat.Split(','), float.Parse);
        int[] beCost = Array.ConvertAll(StoreStatsInfos[2].cost.Split(','), int.Parse);
        for (int x = 0; x < beVals.Length; x++)
        {
            //Debug.Log("unlockedGods 1");

            boosterEngineStatValue.Add(beVals[x]);
            boosterEngineCost.Add(beCost[x]);
        }

        float[] tsVals = Array.ConvertAll(StoreStatsInfos[3].stat.Split(','), float.Parse);
        int[] tsCost = Array.ConvertAll(StoreStatsInfos[3].cost.Split(','), int.Parse);
        for (int x = 0; x < tsVals.Length; x++)
        {
            //Debug.Log("unlockedGods 1");

            turnSpeedStatValue.Add(tsVals[x]);
            turnSpeedCost.Add(tsCost[x]);
        }

        float[] dmVals = Array.ConvertAll(StoreStatsInfos[4].stat.Split(','), float.Parse);
        int[] dmCost = Array.ConvertAll(StoreStatsInfos[4].cost.Split(','), int.Parse);
        for (int x = 0; x < dmVals.Length; x++)
        {
            //Debug.Log("unlockedGods 1");

            damageMultiplierStatValue.Add(dmVals[x]);
            damageMultiplierCost.Add(dmCost[x]);
        }

        float[] mfVals = Array.ConvertAll(StoreStatsInfos[5].stat.Split(','), float.Parse);
        int[] mfCost = Array.ConvertAll(StoreStatsInfos[5].cost.Split(','), int.Parse);
        for (int x = 0; x < mfVals.Length; x++)
        {
            //Debug.Log("unlockedGods 1");

            maxFuelStatValue.Add(mfVals[x]);
            maxFuelCost.Add(mfCost[x]);
        }

        float[] mbfVals = Array.ConvertAll(StoreStatsInfos[6].stat.Split(','), float.Parse);
        int[] mbfCost = Array.ConvertAll(StoreStatsInfos[6].cost.Split(','), int.Parse);
        for (int x = 0; x < mbfVals.Length; x++)
        {
            //Debug.Log("unlockedGods 1");

            maxBoosterFuelStatValue.Add(mbfVals[x]);
            maxBoosterFuelCost.Add(mbfCost[x]);
        }

        float[] arVals = Array.ConvertAll(StoreStatsInfos[7].stat.Split(','), float.Parse);
        int[] arCost = Array.ConvertAll(StoreStatsInfos[7].cost.Split(','), int.Parse);
        for (int x = 0; x < arVals.Length; x++)
        {
            //Debug.Log("unlockedGods 1");

            airReistanceStatValue.Add(arVals[x]);
            airReistanceCost.Add(arCost[x]);
        }

        float[] fcrVals = Array.ConvertAll(StoreStatsInfos[8].stat.Split(','), float.Parse);
        int[] fcrCost = Array.ConvertAll(StoreStatsInfos[8].cost.Split(','), int.Parse);
        for (int x = 0; x < fcrVals.Length; x++)
        {
            //Debug.Log("unlockedGods 1");

            fuelConsumptionRateStatValue.Add(fcrVals[x]);
            fuelConsumptionRateCost.Add(fcrCost[x]);
        }

        float[] bfcrVals = Array.ConvertAll(StoreStatsInfos[9].stat.Split(','), float.Parse);
        int[] bfcrCost = Array.ConvertAll(StoreStatsInfos[9].cost.Split(','), int.Parse);
        for (int x = 0; x < bfcrVals.Length; x++)
        {
            //Debug.Log("unlockedGods 1");

            boosterFuelConsumptionRateStatValue.Add(bfcrVals[x]);
            boosterFuelConsumptionRateCost.Add(bfcrCost[x]);
        }

        float[] nmVals = Array.ConvertAll(StoreStatsInfos[10].stat.Split(','), float.Parse);
        int[] nmCost = Array.ConvertAll(StoreStatsInfos[10].cost.Split(','), int.Parse);
        for (int x = 0; x < nmVals.Length; x++)
        {
            //Debug.Log("unlockedGods 1");

            nummyMultiplierStatValue.Add(nmVals[x]);
            nummyMultiplierCost.Add(nmCost[x]);
        }

        float[] dvVals = Array.ConvertAll(StoreStatsInfos[11].stat.Split(','), float.Parse);
        int[] dvCost = Array.ConvertAll(StoreStatsInfos[11].cost.Split(','), int.Parse);
        for (int x = 0; x < dvVals.Length; x++)
        {
            //Debug.Log("unlockedGods 1");

            diamonValueStatValue.Add(dvVals[x]);
            diamonValueCost.Add(dvCost[x]);
        }
        /*

         */
    }
}
