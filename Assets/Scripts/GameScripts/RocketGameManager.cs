using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class RocketGameManager : MonoBehaviour
{

    public static RocketGameManager Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (this != Instance)
                Destroy(this.gameObject);
        }
        Init();

    }
    private string APP_ID = "ca-app-pub-1031182463803224~9625329531";
    private string BANNER_ID = "ca-app-pub-1031182463803224/8336298598";
    private string INTERSTITIAL_ID = "ca-app-pub-1031182463803224/3881708953";
    private string REWARDVIDEOAD_ID = "ca-app-pub-1031182463803224/5103630597";
    private BannerView bannerAd;
    private InterstitialAd interstitialAd;
    private RewardBasedVideoAd rewardBasedVideoAd;

    public Dictionary<int, StoreStats> StoreStatsInfos { get; private set; }
    public Dictionary<int, Objectives> ObjectiveInfos { get; private set; }

    public int numberOfPlays;
    public bool showTutorial;
    public bool isFlightSchool;

    public int persistantObjectiveCounter;
    public int currentObjectiveCounter;

    public int persistantPlayerCoins;
    public int persistantMaxHealth = 1;
    public int persistantEngineUpgrade = 1;
    public int persistantBoosterEngineUpgrade = 1;
    public int persistantTurnSpeed = 1;
    public int persistantDamageMultiplier = 1;
    public int persistantMaxFuel = 1;
    public int persistantMaxBoosterFuel = 1;
    public int persistantAirReistance = 1;
    public int persistantNummyMultiplier = 1;
    public int persistantFuelConsumptionRate = 1;
    public int persistantBoosterFuelConsumptionRate = 1;
    public int persistantDiamonValue = 1;

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

    public List<float> nummyMultiplierStatValue;
    public List<int> nummyMultiplierCost;

    public List<float> fuelConsumptionRateStatValue;
    public List<int> fuelConsumptionRateCost;

    public List<float> boosterFuelConsumptionRateStatValue;
    public List<int> boosterFuelConsumptionRateCost;

    public List<float> diamonValueStatValue;
    public List<int> diamonValueCost;


    // Start is called before the first frame update
    void Init()
    {
        //MobileAds.Initialize(APP_ID);
        RequestBanner();
        RequestInterstitial();
        RequestVideoAd();

        Debug.Log("LOAD XMLs");
        StoreStatsLoader storeStatsLoader = new StoreStatsLoader();
        StoreStatsInfos = storeStatsLoader.GetDict();
        ObjectivesLoader objectivesLoader = new ObjectivesLoader();
        ObjectiveInfos = objectivesLoader.GetDict();


        numberOfPlays = PlayerPrefs.GetInt("NumberOfPlays");

        if (numberOfPlays == 0)
        {
            Debug.Log("FIRST PLAY");

            PlayerPrefs.SetInt("persistantObjectiveCounter", 0);

            PlayerPrefs.SetInt("persistantPlayerCoins", 0);

            PlayerPrefs.SetInt("persistantMaxHealth", 1);
            PlayerPrefs.SetInt("persistantEngineUpgrade", 1);
            PlayerPrefs.SetInt("persistantBoosterEngineUpgrade", 1);
            PlayerPrefs.SetInt("persistantTurnSpeed", 1);
            PlayerPrefs.SetInt("persistantDamageMultiplier", 1);
            PlayerPrefs.SetInt("persistantMaxFuel", 1);
            PlayerPrefs.SetInt("persistantMaxBoosterFuel", 1);
            PlayerPrefs.SetInt("persistantAirReistance", 1);
            PlayerPrefs.SetInt("persistantFuelConsumptionRate", 1);
            PlayerPrefs.SetInt("persistantBoosterFuelConsumptionRate", 1);
            PlayerPrefs.SetInt("persistantNummyMultiplier", 1);
            PlayerPrefs.SetInt("persistantDiamonValue", 1);
        }
        RetrievePersistatStats();

    }
    public void SavePersistatStats()
    {
        PlayerPrefs.SetInt("persistantObjectiveCounter", persistantObjectiveCounter);
        PlayerPrefs.SetInt("NumberOfPlays", numberOfPlays);
        PlayerPrefs.SetInt("persistantPlayerCoins", persistantPlayerCoins);
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
        numberOfPlays = PlayerPrefs.GetInt("persistantObjectiveCounter", numberOfPlays);
        persistantObjectiveCounter = PlayerPrefs.GetInt("persistantObjectiveCounter", persistantObjectiveCounter);
        persistantPlayerCoins = PlayerPrefs.GetInt("persistantPlayerCoins", persistantPlayerCoins);
        persistantMaxHealth = PlayerPrefs.GetInt("persistantMaxHealth", persistantMaxHealth);
        persistantEngineUpgrade = PlayerPrefs.GetInt("persistantEngineUpgrade", persistantEngineUpgrade);
        persistantBoosterEngineUpgrade = PlayerPrefs.GetInt("persistantBoosterEngineUpgrade", persistantBoosterEngineUpgrade);
        persistantTurnSpeed = PlayerPrefs.GetInt("persistantTurnSpeed", persistantTurnSpeed);
        persistantDamageMultiplier =PlayerPrefs.GetInt("persistantDamageMultiplier", persistantDamageMultiplier);
        persistantMaxFuel =PlayerPrefs.GetInt("persistantMaxFuel", persistantMaxFuel);
        persistantMaxBoosterFuel = PlayerPrefs.GetInt("persistantMaxBoosterFuel", persistantMaxBoosterFuel);
        persistantAirReistance = PlayerPrefs.GetInt("persistantAirReistance", persistantAirReistance);
        persistantFuelConsumptionRate = PlayerPrefs.GetInt("persistantFuelConsumptionRate", persistantFuelConsumptionRate);
        persistantBoosterFuelConsumptionRate = PlayerPrefs.GetInt("persistantBoosterFuelConsumptionRate", persistantBoosterFuelConsumptionRate);
        persistantNummyMultiplier = PlayerPrefs.GetInt("persistantNummyMultiplier", persistantNummyMultiplier);
        persistantDiamonValue = PlayerPrefs.GetInt("persistantDiamonValue", persistantDiamonValue);


        int[] hpVals = Array.ConvertAll(StoreStatsInfos[0].stat.Split(','), int.Parse);
        int[] hpCosts = Array.ConvertAll(StoreStatsInfos[0].cost.Split(','), int.Parse);
        for (int x = 0; x < hpVals.Length; x++)
        {
            healthStatValues.Add(hpVals[x]);
            healthStatCost.Add(hpCosts[x]);
        }
        
        float[] meVals = Array.ConvertAll(StoreStatsInfos[1].stat.Split(','), float.Parse);
        int[] meCost = Array.ConvertAll(StoreStatsInfos[1].cost.Split(','), int.Parse);
        for (int x = 0; x < meVals.Length; x++)
        {
            mainEngineStatValue.Add(meVals[x]);
            mainEngineCost.Add(meCost[x]);
        }

        float[] beVals = Array.ConvertAll(StoreStatsInfos[2].stat.Split(','), float.Parse);
        int[] beCost = Array.ConvertAll(StoreStatsInfos[2].cost.Split(','), int.Parse);
        for (int x = 0; x < beVals.Length; x++)
        {
            boosterEngineStatValue.Add(beVals[x]);
            boosterEngineCost.Add(beCost[x]);
        }

        float[] tsVals = Array.ConvertAll(StoreStatsInfos[3].stat.Split(','), float.Parse);
        int[] tsCost = Array.ConvertAll(StoreStatsInfos[3].cost.Split(','), int.Parse);
        for (int x = 0; x < tsVals.Length; x++)
        {
            turnSpeedStatValue.Add(tsVals[x]);
            turnSpeedCost.Add(tsCost[x]);
        }

        float[] dmVals = Array.ConvertAll(StoreStatsInfos[4].stat.Split(','), float.Parse);
        int[] dmCost = Array.ConvertAll(StoreStatsInfos[4].cost.Split(','), int.Parse);
        for (int x = 0; x < dmVals.Length; x++)
        {
            damageMultiplierStatValue.Add(dmVals[x]);
            damageMultiplierCost.Add(dmCost[x]);
        }

        float[] mfVals = Array.ConvertAll(StoreStatsInfos[5].stat.Split(','), float.Parse);
        int[] mfCost = Array.ConvertAll(StoreStatsInfos[5].cost.Split(','), int.Parse);
        for (int x = 0; x < mfVals.Length; x++)
        {
            maxFuelStatValue.Add(mfVals[x]);
            maxFuelCost.Add(mfCost[x]);
        }

        float[] mbfVals = Array.ConvertAll(StoreStatsInfos[6].stat.Split(','), float.Parse);
        int[] mbfCost = Array.ConvertAll(StoreStatsInfos[6].cost.Split(','), int.Parse);
        for (int x = 0; x < mbfVals.Length; x++)
        {
            maxBoosterFuelStatValue.Add(mbfVals[x]);
            maxBoosterFuelCost.Add(mbfCost[x]);
        }

        float[] arVals = Array.ConvertAll(StoreStatsInfos[7].stat.Split(','), float.Parse);
        int[] arCost = Array.ConvertAll(StoreStatsInfos[7].cost.Split(','), int.Parse);
        for (int x = 0; x < arVals.Length; x++)
        {
            airReistanceStatValue.Add(arVals[x]);
            airReistanceCost.Add(arCost[x]);
        }

        float[] nmVals = Array.ConvertAll(StoreStatsInfos[8].stat.Split(','), float.Parse);
        int[] nmCost = Array.ConvertAll(StoreStatsInfos[8].cost.Split(','), int.Parse);
        for (int x = 0; x < nmVals.Length; x++)
        {
            nummyMultiplierStatValue.Add(nmVals[x]);
            nummyMultiplierCost.Add(nmCost[x]);
        }

        float[] fcrVals = Array.ConvertAll(StoreStatsInfos[9].stat.Split(','), float.Parse);
        int[] fcrCost = Array.ConvertAll(StoreStatsInfos[9].cost.Split(','), int.Parse);
        for (int x = 0; x < fcrVals.Length; x++)
        {
            fuelConsumptionRateStatValue.Add(fcrVals[x]);
            fuelConsumptionRateCost.Add(fcrCost[x]);
        }

        float[] bfcrVals = Array.ConvertAll(StoreStatsInfos[10].stat.Split(','), float.Parse);
        int[] bfcrCost = Array.ConvertAll(StoreStatsInfos[10].cost.Split(','), int.Parse);
        for (int x = 0; x < bfcrVals.Length; x++)
        {
            boosterFuelConsumptionRateStatValue.Add(bfcrVals[x]);
            boosterFuelConsumptionRateCost.Add(bfcrCost[x]);
        }

        float[] dvVals = Array.ConvertAll(StoreStatsInfos[11].stat.Split(','), float.Parse);
        int[] dvCost = Array.ConvertAll(StoreStatsInfos[11].cost.Split(','), int.Parse);
        for (int x = 0; x < dvVals.Length; x++)
        {
            diamonValueStatValue.Add(dvVals[x]);
            diamonValueCost.Add(dvCost[x]);
        }
    }
    public void SaveEarnedCoins(int coins)
    {
        Debug.Log("SaveEarnedCoins: " + coins);

        persistantPlayerCoins += coins;
        PlayerPrefs.SetInt("persistantPlayerCoins", persistantPlayerCoins);
    }
    public bool PurchaseWithCoins(int cost)
    {
        bool canPurchase = false;

        if (cost <= persistantPlayerCoins)
        {
            persistantPlayerCoins -= cost;

            canPurchase = true;
            PlayerPrefs.SetInt("persistantPlayerCoins", persistantPlayerCoins);
        }

        return canPurchase;
    }

    public void ObjectiveComplete()
    {
        persistantObjectiveCounter++;
        PlayerPrefs.SetInt("persistantObjectiveCounter", persistantObjectiveCounter);
    }
    public void ResetAllStats()
    {
        PlayerPrefs.DeleteAll();

        numberOfPlays = 0;

        persistantObjectiveCounter = 0;
        currentObjectiveCounter = 0;

        persistantPlayerCoins = 0;

        persistantMaxHealth = 1;
        persistantEngineUpgrade = 1;
        persistantBoosterEngineUpgrade = 1;
        persistantTurnSpeed = 1;
        persistantDamageMultiplier = 1;
        persistantMaxFuel = 1;
        persistantMaxBoosterFuel = 1;
        persistantAirReistance = 1;
        persistantNummyMultiplier = 1;
        persistantFuelConsumptionRate = 1;
        persistantBoosterFuelConsumptionRate = 1;
        persistantDiamonValue = 1;

        SavePersistatStats();
        Init();
    } 

    void RequestBanner()
    {
        //RELEASE
        //string banner_ID = BANNER_ID;
        string banner_ID = "ca-app-pub-3940256099942544/6300978111";
        bannerAd = new BannerView(banner_ID, AdSize.Banner,AdPosition.Bottom);

        //RELEASE
        //AdRequest adRequest = new AdRequest.Builder().Build();

        //FOR TESTING
        AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();

        bannerAd.LoadAd(adRequest);
    }
    void RequestInterstitial()
    {
        //RELEASE
        //string interstitial_ID = INTERSTITIAL_ID;
        string interstitial_ID = "ca-app-pub-3940256099942544/1033173712";
        interstitialAd = new InterstitialAd(interstitial_ID);

        //RELEASE
        //AdRequest adRequest = new AdRequest.Builder().Build();

        //FOR TESTING
        AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();

        interstitialAd.LoadAd(adRequest);
    }

    void RequestVideoAd()
    {
        //RELEASE
        //string video_ID = REWARDVIDEOAD_ID;
        string video_ID = "ca-app-pub-3940256099942544/5224354917";
        rewardBasedVideoAd = RewardBasedVideoAd.Instance;

        //RELEASE
        //AdRequest adRequest = new AdRequest.Builder().Build();

        //FOR TESTING
        AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();

        rewardBasedVideoAd.LoadAd(adRequest, video_ID);
    }

    public void Display_Banner()
    {
        bannerAd.Show();
    }
    public void Hide_Banner()
    {
        bannerAd.Hide();
    }
    public void Display_Interstitial()
    {
        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
    }
    public void Display_Reward_Video()
    {
        if (rewardBasedVideoAd.IsLoaded())
        {
            rewardBasedVideoAd.Show();
        }
    }

    //Handle Events
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        Display_Banner();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
        //Ad failed to load
        RequestBanner();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    void HandleBannerAdEvents(bool subcribe)
    {
        if (subcribe)
        {
            // Called when an ad request has successfully loaded.
            bannerAd.OnAdLoaded += HandleOnAdLoaded;
            // Called when an ad request failed to load.
            bannerAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            bannerAd.OnAdOpening += HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            bannerAd.OnAdClosed += HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            bannerAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        }
        else
        {
            // Called when an ad request has successfully loaded.
            bannerAd.OnAdLoaded -= HandleOnAdLoaded;
            // Called when an ad request failed to load.
            bannerAd.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            bannerAd.OnAdOpening -= HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            bannerAd.OnAdClosed -= HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            bannerAd.OnAdLeavingApplication -= HandleOnAdLeavingApplication;
        }
    }

    private void OnEnable()
    {
        HandleBannerAdEvents(true);
    }
    private void OnDisable()
    {
        HandleBannerAdEvents(false);
    }
}
