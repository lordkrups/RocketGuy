using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Linq;
using System;

public class EverythingLoader : MonoBehaviour
{
    public static EverythingLoader Instance = null;

    public void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);    // Ensures that there aren't multiple Singletons

        Instance = this;
        DontDestroyOnLoad(this);
        Init();
    }

    public PlayerStats playerStats;
    public GameManager gameManager;

    public Dictionary<int, GameStats> GameStatsInfos { get; private set; }
    public Dictionary<int, Player> PlayerInfos { get; private set; }
    public Dictionary<int, Enemy> EnemyInfos { get; private set; }
    public Dictionary<int, Weapon> WeaponInfos { get; private set; }
    public Dictionary<int, PowerUp> PowerUpInfos { get; private set; }
	public Dictionary<int, Hat> HatInfos { get; private set; }
	public Dictionary<int, Skill> SkillInfos { get; private set; }

	public bool gameStatsLoaded, playersLoaded, enemiesLoaded, weaponsLoaded, powerUpsLoaded, hatsLoaded, skillsLoaded;
    public bool firstPlay;

    public void Init()
    {
        //PlayerPrefs.DeleteAll();
		Debug.Log("LOAD XMLs");
        GameStatsLoader gameStatsLoader = new GameStatsLoader();
        GameStatsInfos = gameStatsLoader.GetDict();

        PlayerLoader playerLoader = new PlayerLoader();
        PlayerInfos = playerLoader.GetDict();

        EnemyLoader enemyLoader = new EnemyLoader();
        EnemyInfos = enemyLoader.GetDict();

        WeaponLoader weaponLoader = new WeaponLoader();
        WeaponInfos = weaponLoader.GetDict();

        PowerUpLoader powerUpLoader = new PowerUpLoader();
        PowerUpInfos = powerUpLoader.GetDict();

        HatLoader hatLoader = new HatLoader();
        HatInfos = hatLoader.GetDict();

		SkillLoader skillLoader = new SkillLoader();
		SkillInfos = skillLoader.GetDict();

        if (GameStatsInfos != null)
        {
            gameStatsLoaded = true;
        }
        if (PlayerInfos != null)
        {
            playersLoaded = true;
        }
        if (EnemyInfos != null)
        {
            enemiesLoaded = true;
        }
        if (WeaponInfos != null)
        {
            weaponsLoaded = true;
        }
        if (PowerUpInfos != null)
        {
            powerUpsLoaded = true;
        }
        if (HatInfos != null)
        {
            hatsLoaded = true;
        }
        if (SkillInfos != null)
        {
            skillsLoaded = true;
        }
    }
    public void SetWeapon(int w)
    {
        playerStats.Weapon = w;
        Debug.Log("EL Weapon: " + playerStats.Weapon);
    }
    public PlayerStats GetPlayerStats()
    {
        return playerStats;
    }

    private void Update()
    {
        if (!playersLoaded || !enemiesLoaded || !weaponsLoaded ||
            !powerUpsLoaded || !hatsLoaded || !skillsLoaded)
        {
            CheckIfLoaded();
        }
        //if (!gmLoaded)
        //{
        //    gameManager.Init();
        //    gmLoaded = true;
        //}
    }

    private void CheckIfLoaded()
    {
        if (!playersLoaded)
        {
            if (PlayerInfos != null)
            {
                playersLoaded = true;
            }
        }
        if (!enemiesLoaded)
        {
            if (EnemyInfos != null)
            {
                enemiesLoaded = true;
            }
        }
        if (!weaponsLoaded)
        {
            if (WeaponInfos != null)
            {
                weaponsLoaded = true;
            }
        }
        if (!hatsLoaded)
        {
            if (HatInfos != null)
            {
                hatsLoaded = true;
            }
        }
        if (!skillsLoaded)
        {
            if (SkillInfos != null)
            {
                skillsLoaded = true;
            }
        }
    }


}
/*{
private static EverythingLoader _instance = null;

public static EverythingLoader Instance {
    get
    {
        if ( _instance == null)
        {
            _instance = new EverythingLoader();
        }
        return _instance;
    }
}
public GameManager gameManager;
public Dictionary<int, Enemy> EnemyInfos { get; private set; }
public Dictionary<int, Weapon> WeaponInfos { get; private set; }
public void Init()
{
    DontDestroyOnLoad(this);
    EnemyLoader enemyLoader = new EnemyLoader();
    EnemyInfos = enemyLoader.GetDict();

    WeaponLoader weaponLoader = new WeaponLoader();
    WeaponInfos = weaponLoader.GetDict();
}
}*/
