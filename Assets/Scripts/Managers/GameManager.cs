using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool playerLoaded;
    public bool menuSetUp;
    public bool updateLevels, updateEnergy, updateGold, updateGems;

    public int currentGod;
    public int currentHat;
    public int currentWeapon;
    public bool isLoaded;

    public int sfxMuted;
    public int bgmMuted;

    //FOR TOP CONTAINER
    public int persistantLevel;
    public int persistantExp;
    public int persistantEnergy;
    public int persistantGold;
    public int persistantGems;
    
    //FOR GEAR MENU
    public string unlockedGods;
    public string currentGear;
    public string unlockedHats;
    public string unlockedWeapons;
    public List<int> unlockedGodsList;
    public List<int> currentGearList;
    public List<int> unlockedHatsList;
    public List<int> unlockedWeaponsList;

    //FOR SKILLS MENU
    public int totalUnlockedSkills;
    public int totalTimeRewardUnlocks;
    public string unlockedSkills;
    public List<int> unlockedSkillsList;

    public int minGoldReward;
    public int hourGoldReward;
    public int dayGoldReward;
    public int monthGoldReward;

    public int minEnergyReward;
    public int hourEnergyReward;
    public int dayEnergyReward;
    public int monthEnergyReward;
    public int maxEnergyAmount = 20;
    public float energyRewardTimer;
    public void Init()
    {
        sfxMuted = PlayerPrefs.GetInt("SFXMuted", 1);
        bgmMuted = PlayerPrefs.GetInt("BGMMuted", 1);

        persistantLevel = PlayerPrefs.GetInt("PersistantLevel", 1);
        persistantExp = PlayerPrefs.GetInt("PersistantExp", 0);
        persistantEnergy = PlayerPrefs.GetInt("persistantEnergy", 10);
        persistantGold = PlayerPrefs.GetInt("PersistantGold", 10000);
        persistantGems = PlayerPrefs.GetInt("PersistantGems", 100);

        currentGod = PlayerPrefs.GetInt("CurrentGod", 0);
        currentHat = PlayerPrefs.GetInt("CurrentHat", 0);
        currentWeapon = PlayerPrefs.GetInt("CurrentWeapon", 0);

        unlockedGods = PlayerPrefs.GetString("UnlockedGods", "1,0,0,0,0,0,0,0,0,0,0,0,0");
        unlockedHats = PlayerPrefs.GetString("UnlockedHats", "1,0,0,0,0,0");
        unlockedWeapons = PlayerPrefs.GetString("UnlockedWeapons", "1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
        currentGear = PlayerPrefs.GetString("CurrentGear", "0,0,0,0");

        unlockedSkills = PlayerPrefs.GetString("UnlockedSkills", "0,0,0,0,0,0,0,0,0");
        totalUnlockedSkills = PlayerPrefs.GetInt("TotalUnlockedSkills", 0);
        totalTimeRewardUnlocks = PlayerPrefs.GetInt("TotalTimeRewardUnlocks", 0);

        unlockedGodsList = new List<int>();
        unlockedHatsList = new List<int>();
        unlockedWeaponsList = new List<int>();
        currentGearList = new List<int>();
        unlockedSkillsList = new List<int>();

        int[] gods = Array.ConvertAll(unlockedGods.Split(','), int.Parse);
        for (int x = 0; x < gods.Length; x++)
        {
            //Debug.Log("unlockedGods 1");

            unlockedGodsList.Add(gods[x]);
        }

        int[] hats = Array.ConvertAll(unlockedHats.Split(','), int.Parse);

        for (int x = 0; x < hats.Length; x++)
        {
            unlockedHatsList.Add(hats[x]);
        }

        int[] weapons = Array.ConvertAll(unlockedWeapons.Split(','), int.Parse);

        for (int x = 0; x < weapons.Length; x++)
        {
            unlockedWeaponsList.Add(weapons[x]);
        }

        int[] gear = Array.ConvertAll(currentGear.Split(','), int.Parse);

        for (int x = 0; x < gear.Length; x++)
        {
            currentGearList.Add(gear[x]);
        }

        currentHat = currentGearList[0];
        currentWeapon = currentGearList[1];

        int[] skills = Array.ConvertAll(unlockedSkills.Split(','), int.Parse);

        for (int x = 0; x < skills.Length; x++)
        {
            unlockedSkillsList.Add(skills[x]);
        }

        SetUpPlayer();

        //Debug.Log("LastRewardDay: " + PlayerPrefs.GetInt("LastRewardDay"));
        //Debug.Log("LastRewardHour: " + PlayerPrefs.GetInt("LastRewardHour"));
        //Debug.Log("LastRewardMin: " + PlayerPrefs.GetInt("LastRewardMin"));

        if (totalTimeRewardUnlocks > 0)
        {
            GetTimeRewardTimes();
            Debug.Log("GetTimeRewardTimes");

        }
        energyRewardTimer = Time.time + 600f;
        GetEnergyRewardTimes();
    }

    private void Update()
    {
        if (!playerLoaded && EverythingLoader.Instance.PlayerInfos != null)
        {
            SetUpPlayer();
        }
        if (Time.time > energyRewardTimer)
        {
            DepositEnergy(1);
            energyRewardTimer = Time.time + 600f;
            Debug.Log("Time SET: " + energyRewardTimer);
        }
    }
    public void CheckEnergyReward()
    {
        int diffmonth = DateTime.Now.Month - monthEnergyReward;
        Debug.Log("month: " + diffmonth);
        int diffday = Mathf.Abs(DateTime.Now.Day - dayEnergyReward);
        Debug.Log("day: " + diffday);
        int diffhour = DateTime.Now.Hour - hourEnergyReward;
        Debug.Log("hour: " + diffhour);
        int diffmin = DateTime.Now.Minute - minEnergyReward;
        Debug.Log("min: " + diffmin);

        if (diffmonth > 0)
        {
            diffday = diffday - 30;
        }

        int totalTime = ((diffday * (60 * 24)) + (diffhour * 60) + diffmin);
        Debug.Log("totalTime: " + totalTime);

        if (totalTime > 4320)
        {
            totalTime = 4320;
        }

        int timeEnergyReward = Mathf.Abs(totalTime/10);

        DepositEnergy(timeEnergyReward);
        SaveEnergyRewardTime();
    }
    public void GetEnergyRewardTimes()
    {
        //monthEnergyReward = PlayerPrefs.GetInt("LastEnergyMonth", DateTime.Now.Month);
        //dayEnergyReward = PlayerPrefs.GetInt("LastEnergyDay", DateTime.Now.Day);
        //hourEnergyReward = PlayerPrefs.GetInt("LastEnergyHour", DateTime.Now.Hour);
        //minEnergyReward = PlayerPrefs.GetInt("LastRewardMin", DateTime.Now.Minute);

        monthEnergyReward = PlayerPrefs.GetInt("LastEnergyMonth");
        dayEnergyReward = PlayerPrefs.GetInt("LastEnergyDay");
        hourEnergyReward = PlayerPrefs.GetInt("LastEnergyHour");
        minEnergyReward = PlayerPrefs.GetInt("LastEnergyMin");
        Debug.Log("monthEnergyReward: " + monthEnergyReward);
        Debug.Log("dayEnergyReward: " + dayEnergyReward);
        Debug.Log("hourEnergyReward: " + hourEnergyReward);
        Debug.Log("minEnergyReward: " + minEnergyReward);
        if (monthEnergyReward == 0)
        {
            SaveEnergyRewardTime();

        } else
        {
            CheckEnergyReward();
        }
    }
    public void SaveEnergyRewardTime()
    {
        Debug.Log("SaveEnergyRewardTime: ");
        PlayerPrefs.DeleteKey("LastEnergyMonth");
        PlayerPrefs.DeleteKey("LastEnergyDay");
        PlayerPrefs.DeleteKey("LastEnergyHour");
        PlayerPrefs.DeleteKey("LastRewardMin");

        PlayerPrefs.SetInt("LastRewardMin", DateTime.Now.Minute);
        PlayerPrefs.SetInt("LastEnergyHour", DateTime.Now.Hour);
        PlayerPrefs.SetInt("LastEnergyDay", DateTime.Now.Day);
        PlayerPrefs.SetInt("LastEnergyMonth", DateTime.Now.Month);
        monthEnergyReward  = PlayerPrefs.GetInt("LastEnergyMonth", DateTime.Now.Month);
        dayEnergyReward = PlayerPrefs.GetInt("LastEnergyDay", DateTime.Now.Day);
        hourEnergyReward = PlayerPrefs.GetInt("LastEnergyHour", DateTime.Now.Hour);
        minEnergyReward = PlayerPrefs.GetInt("LastRewardMin", DateTime.Now.Minute);
        Debug.Log("LastEnergyMonth: " + monthEnergyReward);
        Debug.Log("LastEnergyDay: " + dayEnergyReward);
        Debug.Log("LastEnergyHour: " + hourEnergyReward);
        Debug.Log("LastRewardMin: " + minEnergyReward);
    }
    public void GetTimeRewardTimes()
    {
        monthGoldReward = PlayerPrefs.GetInt("LastRewardMonth");
        dayGoldReward = PlayerPrefs.GetInt("LastRewardDay");
        hourGoldReward = PlayerPrefs.GetInt("LastRewardHour");
        minGoldReward = PlayerPrefs.GetInt("LastRewardMin");
        Debug.Log("LastRewardMonth: " + monthGoldReward);
        Debug.Log("LastRewardDay: " + dayGoldReward);
        Debug.Log("LastRewardHour: " + hourGoldReward);
        Debug.Log("LastRewardMin: " + minGoldReward);
    }
    public void SetTimeRewardTimes()
    {
        /*
        int diffday = DateTime.Now.Day;
        Debug.Log("diffday: " + diffday);
        int diffhour = DateTime.Now.Hour;
        Debug.Log("diffhour: " + diffhour);
        int diffmin = DateTime.Now.Minute;
        Debug.Log("diffmin: " + diffmin);
        */
        monthGoldReward = PlayerPrefs.GetInt("LastRewardMonth", DateTime.Now.Month);
        dayGoldReward = PlayerPrefs.GetInt("LastRewardDay", DateTime.Now.Day);
        hourGoldReward = PlayerPrefs.GetInt("LastRewardHour", DateTime.Now.Hour);
        minGoldReward = PlayerPrefs.GetInt("LastRewardMin", DateTime.Now.Minute);
        Debug.Log("LastRewardMonth: " + monthGoldReward);
        Debug.Log("LastRewardDay: " + dayGoldReward);
        Debug.Log("LastRewardHour: " + hourGoldReward);
        Debug.Log("LastRewardMin: " + minGoldReward);

    }

    public void SaveTimeRewardTimes()
    {
        Debug.Log("SaveTimeRewardTimes: ");
        PlayerPrefs.DeleteKey("LastRewardMin");
        PlayerPrefs.DeleteKey("LastRewardHour");
        PlayerPrefs.DeleteKey("LastRewardDay");
        PlayerPrefs.DeleteKey("LastRewardMonth");

        PlayerPrefs.SetInt("LastRewardMin", DateTime.Now.Minute);
        PlayerPrefs.SetInt("LastRewardHour", DateTime.Now.Hour);
        PlayerPrefs.SetInt("LastRewardDay", DateTime.Now.Day);
        PlayerPrefs.SetInt("LastRewardMonth", DateTime.Now.Month);
        monthGoldReward = PlayerPrefs.GetInt("LastRewardMonth", DateTime.Now.Day);
        dayGoldReward = PlayerPrefs.GetInt("LastRewardDay", DateTime.Now.Day);
        hourGoldReward = PlayerPrefs.GetInt("LastRewardHour", DateTime.Now.Hour);
        minGoldReward = PlayerPrefs.GetInt("LastRewardMin", DateTime.Now.Minute);
        Debug.Log("LastRewardMonth: " + monthGoldReward);
        Debug.Log("LastRewardDay: " + dayGoldReward);
        Debug.Log("LastRewardHour: " + hourGoldReward);
        Debug.Log("LastRewardMin: " + minGoldReward);
    }

    private void SetUpPlayer()
    {
        EverythingLoader.Instance.playerStats = new PlayerStats();

        EverythingLoader.Instance.playerStats.CharacterLevel = 0;
        EverythingLoader.Instance.playerStats.ExperiencePoints = 1;
        EverythingLoader.Instance.playerStats.PlayerSprite = EverythingLoader.Instance.PlayerInfos[currentGod].sprite;
        EverythingLoader.Instance.playerStats.Name = EverythingLoader.Instance.PlayerInfos[currentGod].name;
        EverythingLoader.Instance.playerStats.Weapon = currentWeapon;
        EverythingLoader.Instance.playerStats.MaxHealth = EverythingLoader.Instance.PlayerInfos[currentGod].startHP;
        EverythingLoader.Instance.playerStats.HealPercentage = EverythingLoader.Instance.PlayerInfos[currentGod].baseHealPerc;
        EverythingLoader.Instance.playerStats.AttackPower = EverythingLoader.Instance.PlayerInfos[currentGod].atk;
        EverythingLoader.Instance.playerStats.CriticalHitRate = EverythingLoader.Instance.PlayerInfos[currentGod].critHitRate;
        EverythingLoader.Instance.playerStats.CriticalHitDamagePercentage = EverythingLoader.Instance.PlayerInfos[currentGod].critDmgPerc;
        EverythingLoader.Instance.playerStats.MoveSpeed = EverythingLoader.Instance.PlayerInfos[currentGod].moveSpeed;
        EverythingLoader.Instance.playerStats.AttackSpeed = EverythingLoader.Instance.PlayerInfos[currentGod].atkSpeed;
        EverythingLoader.Instance.playerStats.AttackRange = EverythingLoader.Instance.PlayerInfos[currentGod].atkRange;
        EverythingLoader.Instance.playerStats.ReduceRangedDamageAmount = EverythingLoader.Instance.PlayerInfos[currentGod].rangeDmgRed;
        EverythingLoader.Instance.playerStats.ReduceCollisionsDamageAmount = EverythingLoader.Instance.PlayerInfos[currentGod].collideDmgRed;
        EverythingLoader.Instance.playerStats.ReduceCritDamage = EverythingLoader.Instance.PlayerInfos[currentGod].critRed;
        EverythingLoader.Instance.playerStats.ReduceRange = EverythingLoader.Instance.PlayerInfos[currentGod].rangeRed;
        EverythingLoader.Instance.playerStats.MaxNumberOfLevels = EverythingLoader.Instance.PlayerInfos[currentGod].maxNumberOfLevels;
        EverythingLoader.Instance.playerStats.PurchaseCost = EverythingLoader.Instance.PlayerInfos[currentGod].purchaseCost;

        int[] nums = Array.ConvertAll(EverythingLoader.Instance.PlayerInfos[currentGod].level_up_table.Split(','), int.Parse);

        for (int x = 0; x < nums.Length; x++)
        {
            EverythingLoader.Instance.playerStats.experienceTable.Add(nums[x]);
        }
        playerLoaded = true;
        isLoaded = true;
    }
    public void SetSFXMuted(bool mute)
    {
        if (mute)
        {
            sfxMuted = 1;
            PlayerPrefs.SetInt("SFXMuted", sfxMuted);
        } else
        {
            sfxMuted = 0;
            PlayerPrefs.SetInt("SFXMuted", sfxMuted);
        }
    }
    public void SetBGMMuted(bool mute)
    {
        if (mute)
        {
            bgmMuted = 1;
            PlayerPrefs.SetInt("BGMMuted", bgmMuted);
        }
        else
        {
            bgmMuted = 0;
            PlayerPrefs.SetInt("BGMMuted", bgmMuted);
        }
    }
    public void SetCurrentGod(int god)
    {
        currentGod = god;
        unlockedGodsList[god] = 1;
        PlayerPrefs.SetInt("CurrentGod", currentGod);
        SetUpPlayer();
        AddUnlockedGod();
    }
    public void SetCurrentHat(int hat)
    {
        currentHat = hat;
        unlockedHatsList[hat] = 1;
        currentGearList[0] = currentHat;
        PlayerPrefs.SetInt("CurrentHat", currentHat);
        SetUpPlayer();
        AddUnlockedHat();
    }
    public void SetCurrentWeapon(int wID, int aid)
    {
        currentWeapon = wID;
        unlockedWeaponsList[aid] = 1;
        currentGearList[1] = aid;
        PlayerPrefs.SetInt("CurrentWeapon", currentWeapon);
        SetUpPlayer();
        AddUnlockedWeapon();
    }

    ///////////////////////////////////////////
    //UNLOCKING AND SAVING GOD CHARACTERS
    public void AddUnlockedGod()
    {
        unlockedGods = "";

        for (int i = 0; i < unlockedGodsList.Count; i++)
        {
            unlockedGods = unlockedGods + unlockedGodsList[i].ToString();
            if (i < unlockedGodsList.Count - 1)
            {
                unlockedGods = unlockedGods + ",";
            }
        }
        PlayerPrefs.SetString("UnlockedGods", unlockedGods);

    }
    ////////////////////////////////////////////
    //UNLOCKING GEAR AND SAVING THEM
    public void AddUnlockedHat()
    {
        unlockedHats = "";

        for (int i = 0; i < unlockedHatsList.Count; i++)
        {
            unlockedHats = unlockedHats + unlockedHatsList[i].ToString();
            if (i < unlockedHatsList.Count - 1)
            {
                unlockedHats = unlockedHats + ",";
            }
        }
        PlayerPrefs.SetString("UnlockedHats", unlockedHats);
        currentGearList[0] = currentHat;
        SaveCurrentGear();
    }
    public void AddUnlockedWeapon()
    {
        unlockedWeapons = "";

        for (int i = 0; i < unlockedWeaponsList.Count; i++)
        {
            unlockedWeapons = unlockedWeapons + unlockedWeaponsList[i].ToString();
            if (i < unlockedWeaponsList.Count - 1)
            {
                unlockedWeapons = unlockedWeapons + ",";
            }
        }
        PlayerPrefs.SetString("UnlockedWeapons", unlockedWeapons);
        currentGearList[1] = currentWeapon;
        SaveCurrentGear();
    }
    public void SaveCurrentGear()
    {
        currentGear = "";

        for (int i = 0; i < currentGearList.Count; i++)
        {
            currentGear = currentGear + currentGearList[i].ToString();
            if (i < currentGearList.Count - 1)
            {
                currentGear = currentGear + ",";
            }
        }
        PlayerPrefs.SetString("CurrentGear", currentGear);
    }
    /////////////////////////////
    //UNLOCKING SKILLS AND SAVING THEM
    public void AddUnlockedSkill(int aid)
    {
        Debug.Log("Skill added to list: " + aid);
       // Debug.Log("AddUnlockedSkill sid: " + sid);

        unlockedSkillsList[aid] += 1;

        unlockedSkills = "";

        for (int i = 0; i < unlockedSkillsList.Count; i++)
        {
            unlockedSkills = unlockedSkills + unlockedSkillsList[i].ToString();
            if (i < unlockedSkillsList.Count - 1)
            {
                unlockedSkills = unlockedSkills + ",";
            }
            if (unlockedSkillsList[i] != 0)
            {
            }
        }
        totalUnlockedSkills++;

        PlayerPrefs.SetString("UnlockedSkills", unlockedSkills);
        PlayerPrefs.SetInt("TotalUnlockedSkills", totalUnlockedSkills);
        SetUpPlayer();

        //CHECK IF IT WAS TIME REWARD AND START SAVING THAT
        if (aid == 6)
        {
            Debug.Log("Time Reward unlocked");
            SetTimeRewardTimes();
            totalTimeRewardUnlocks++;
            PlayerPrefs.SetInt("TotalTimeRewardUnlocks", totalTimeRewardUnlocks);
        }
    }
    //QUICK REFERENCE
    public void SavePersistantResources()
    {
        PlayerPrefs.SetInt("PersistantLevel", persistantLevel);
        PlayerPrefs.SetInt("PersistantExp", persistantExp);
        PlayerPrefs.SetInt("persistantEnergy", persistantEnergy);
        PlayerPrefs.SetInt("PersistantGold", persistantGold);
        PlayerPrefs.SetInt("PersistantGems", persistantGems);

        PlayerPrefs.SetString("UnlockedSkills", unlockedSkills);

    }
    ///////////////////////////
    ///Gain Energy
    public void DepositEnergy(int energy)
    {
        Debug.Log("earned energy: " + energy);

        persistantEnergy = persistantEnergy + energy;
        if (persistantEnergy > 20)
        {
            persistantEnergy = 20;
        }
        PlayerPrefs.SetInt("persistantEnergy", persistantEnergy);
        updateEnergy = true;
    }
    public bool UseEnergy(int energy)
    {
        Debug.Log("spend energy: " + energy);
        if (persistantEnergy >= 5)
        {
            persistantEnergy = persistantEnergy - energy;
            PlayerPrefs.SetInt("persistantEnergy", persistantEnergy);
            SaveTimeRewardTimes();
            updateEnergy = true;
            return true;
        } else
        {
            return false;
        }
    }
    //GAIN EXP
    public void DepositExp(int xp)
    {
        Debug.Log("earned XP: " + xp);
        persistantExp = persistantExp + xp;
        PlayerPrefs.SetInt("PersistantExp", persistantExp);

        if (persistantExp >= EverythingLoader.Instance.GameStatsInfos[0].GetNextLevelExp(persistantLevel))
        {
            persistantExp = persistantExp - EverythingLoader.Instance.playerStats.GetNextLevelExp(persistantLevel);
            persistantLevel++;
            PlayerPrefs.SetInt("PersistantLevel", persistantLevel);
        }

        updateLevels = true;
    }

    //PURCHASE AND EARNING WITH GOLD AND GEMS
    public void DepositGold(int gold)
    {
        Debug.Log("earned gold: " + gold);
        persistantGold = persistantGold + gold;
        PlayerPrefs.SetInt("PersistantGold", persistantGold);
        updateGold = true;
    }

    public void DepositGems(int gems)
    {
        Debug.Log("earned GEMS: " + gems);
        persistantGems = persistantGems + gems;
        PlayerPrefs.SetInt("PersistantGems", persistantGems);
        updateGems = true;
    }

    public bool MakePurchaseWithGold(int gold)
    {
        Debug.Log("MakePurchaseWithGold 1");
        Debug.Log("cost: " + gold);

        bool canBuy = false;
        if (persistantGold >= gold)
        {
            Debug.Log("MakePurchaseWithGold 1");

            persistantGold = persistantGold - gold;
            Debug.Log("persistantGold: " + persistantGold);
            PlayerPrefs.SetInt("PersistantGold", persistantGold);
            canBuy = true;
            updateGold = true;
            //totalUnlockedSkills++;
            //persistantLevel++;
        }
        return canBuy;
    }
    public bool MakePurchaseWithGems(int gems)
    {
        bool canBuy = false;
        if (persistantGems >= gems)
        {
            persistantGems = persistantGems - gems;
            canBuy = true;
            updateGems = true;
        }
        return canBuy;
    }
    public void FinishedTopContainerUpdate()
    {
        updateEnergy = false;
        updateGems = false;
        updateGold = false;
        updateLevels = false;
    }
}

/*
     public List<float> mainEngineStatValue;
    public List<int> mainEngineCost;

    public List<float> boosterEngineStatValue;
    public List<int> boosterEngineCost;

    public List<float> turnSpeedStatValue;
    public List<int> turnSpeedCost;

    public List<float> damageMultiplierStatValue;
    public List<int> damageMultiplierCost;

    public List<float> maxFuelValue;
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
 * */
