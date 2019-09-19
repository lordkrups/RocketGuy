using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    [SerializeField]
    private int _characterLevel;
    [SerializeField]
    private string _name;
    [SerializeField]
    private string _sprite;
    [SerializeField]
    private int _experiencePoints;
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _currentHealth;
    [SerializeField]
    private int _currentMoney;
    [SerializeField]
    private int _attackPower;
    [SerializeField]
    private float _criticalHitRate;
    [SerializeField]
    private float _criticalHitDamage;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _attackSpeed;
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private int _reduceRangedDamageAmount;
	[SerializeField]
	private int _reduceCollisionsDamageAmount;
	[SerializeField]
    private float _reduceCritDamage;
    [SerializeField]
    private float _reduceCollisionDamage;
    [SerializeField]
    private float _reduceRange;
    [SerializeField]
    private bool _isMaxLevelReached;
    [SerializeField]
    private int _weapon;
    [SerializeField]
    private int _maxNumberOfLevels;
    public List<int> experienceTable = new List<int>();
    [SerializeField]
    private int _purchaseCost;
    [SerializeField]
    private string _description;

    private bool _rageMode;
    private int _healPercentage;
    private bool _bloodThirst;
    private bool _invincible;
    private int _invincibletime;
    private bool _smart;
    private int _healOnLevelUp;
    private int _enhance = 1;

    public int CharacterLevel { get => _characterLevel; set => _characterLevel = value; }
    public string Name { get => _name; set => _name = value; }
    public string PlayerSprite { get => _sprite; set => _sprite = value; }
    public int ExperiencePoints { get => _experiencePoints; set => _experiencePoints = value; }
    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public int AttackPower { get => _attackPower; set => _attackPower = value; }
    public float CriticalHitRate { get => _criticalHitRate; set => _criticalHitRate = value; }
    public float CriticalHitDamagePercentage { get => _criticalHitDamage; set => _criticalHitDamage = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float AttackSpeed { get => _attackSpeed; set => _attackSpeed = value; }
    public float AttackRange { get => _attackRange; set => _attackRange = value; }
	public int ReduceRangedDamageAmount { get => _reduceRangedDamageAmount; set => _reduceRangedDamageAmount = value; }
	public int ReduceCollisionsDamageAmount { get => _reduceCollisionsDamageAmount; set => _reduceCollisionsDamageAmount = value; }
    public float ReduceCritDamage { get => _reduceCritDamage; set => _reduceCritDamage = value; }
    public float ReduceRange { get => _reduceRange; set => _reduceRange = value; }
    public int CurrentMoney { get => _currentMoney; set => _currentMoney = value; }
    public int Weapon { get => _weapon; set => _weapon = value; }
    public int MaxNumberOfLevels { get => _maxNumberOfLevels; set => _maxNumberOfLevels = value; }
    public int PurchaseCost { get => _purchaseCost; set => _purchaseCost = value; }
    public string Description { get => _description; set => _description = value; }

    public bool Rage { get => _rageMode; set => _rageMode = value; }
    public int HealPercentage { get => _healPercentage; set => _healPercentage = value; }
    public bool BloodThirst { get => _bloodThirst; set => _bloodThirst = value; }
    public bool Invincible { get => _invincible; set => _invincible = value; }
    public int Invincibletime { get => _invincibletime; set => _invincibletime = value; }
    public bool Smart { get => _smart; set => _smart = value; }
    public int HealOnLevelUp { get => _healOnLevelUp; set => _healOnLevelUp = value; }
    public int Enhance { get => _enhance; set => _enhance = value; }

    /*
    [SerializeField]
    private int levelOneExperience = 100;
    [SerializeField]
    private int levelTwoExperience = 300;
    [SerializeField]
    private int levelThreeExperience = 600;
    [SerializeField]
    private int levelFourExperience = 1000;
    [SerializeField]
    private int levelFiveExperience = 1500;
    [SerializeField]
    private int levelSixExperience =2100;
    [SerializeField]
    private int levelSevenExperience = 2800;
    [SerializeField]
    private int levelEightExperience = 3600;
    [SerializeField]
    private int levelNineExperience = 4500;
    [SerializeField]
    private int levelTenExperience = 5500;
    [SerializeField]
    private int levelElevenExperience = 6600;
    */
    public bool CheckMaxlevelReached()
    {
        if (CharacterLevel == _maxNumberOfLevels - 1)
            _isMaxLevelReached = true;

        return _isMaxLevelReached;
    }

    public int GetNextLevelExp(int level)
    {
        int value = 0;

        value = experienceTable[level];

        return value;
    }

}