using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats
{
    [SerializeField]
    private int _enemyId;
    [SerializeField]
    private string _animalName;
    [SerializeField]
    private int _maxHp;
    [SerializeField]
    private int _moveType;
    [SerializeField]
    private float _moveAmount;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _moveDelay;
    [SerializeField]
    private int _atk;
    [SerializeField]
    private int _attackType;
    [SerializeField]
    private int _weapon;
    [SerializeField]
    private int _shots;
    [SerializeField]
    private float _atkRange;
    [SerializeField]
    private float _atkSpeed;
    [SerializeField]
    private float _targetDelay;
    [SerializeField]
    private int _exp;
    [SerializeField]
    private int _reward;
    [SerializeField]
    private int _hpReward;
    [SerializeField]
    private string _sprite;
    [SerializeField]
    private int _size;
    [SerializeField]
    private string _moveAbility;
    [SerializeField]
    private string _shootAbility;

    public int EnemyId { get => _enemyId; private set => _enemyId = value; }
    public string AnimalName { get => _animalName; private set => _animalName = value; }
    public int MaxHealth { get => _maxHp; private set => _maxHp = value; }
    public int Atk { get => _atk; private set => _atk = value; }
    public int MoveType { get => _moveType; private set => _moveType = value; }
    public float MoveAmount { get => _moveAmount; private set => _moveAmount = value; }
    public float MoveSpeed { get => _moveSpeed; private set => _moveSpeed = value; }
    public float MoveDelay { get => _moveDelay; private set => _moveDelay = value; }
    public int AttackType { get => _attackType; private set => _attackType = value; }
    public int Weapon { get => _weapon; private set => _weapon = value; }
    public int Shots { get => _shots; private set => _shots = value; }
    public float AtackRange { get => _atkRange; private set => _atkRange = value; }
    public float AtkSpeed { get => _atkSpeed; private set => _atkSpeed = value; }
    public float TargetDelay { get => _targetDelay; private set => _targetDelay = value; }
    public int Exp { get => _exp; private set => _exp = value; }
    public int Reward { get => _reward; private set => _reward = value; }
    public int HpReward { get => _hpReward; private set => _hpReward = value; }
    public string Sprite { get => _sprite; private set => _sprite = value; }
    public int Size { get => _size; private set => _size = value; }
    public string MoveAbility { get => _moveAbility; private set => _moveAbility = value; }
    public string ShootAbility { get => _shootAbility; private set => _shootAbility = value; }

    public bool isLoaded;

    public void Init(int id)
    {
        EnemyId = id;
        LoadSettings();
    }

    public void LoadSettings()
    {
        //EnemyId = levelManager.enemyLoader.EnemyInfos[EnemyId].id;
        AnimalName = EverythingLoader.Instance.EnemyInfos[EnemyId].animal;
        MaxHealth = EverythingLoader.Instance.EnemyInfos[EnemyId].hp;
        Atk = EverythingLoader.Instance.EnemyInfos[EnemyId].atk;
        MoveType = EverythingLoader.Instance.EnemyInfos[EnemyId].moving_type;
        MoveAmount = EverythingLoader.Instance.EnemyInfos[EnemyId].moving_value / 10;
        MoveSpeed = EverythingLoader.Instance.EnemyInfos[EnemyId].moving_speed / 10;
        MoveDelay = EverythingLoader.Instance.EnemyInfos[EnemyId].moving_delay / 100;
        AttackType = EverythingLoader.Instance.EnemyInfos[EnemyId].atk_type;
        Weapon = EverythingLoader.Instance.EnemyInfos[EnemyId].weapon;
        Shots = EverythingLoader.Instance.EnemyInfos[EnemyId].shots;
        AtackRange = (float)EverythingLoader.Instance.EnemyInfos[EnemyId].atk_range / 10;
        AtkSpeed = (float)EverythingLoader.Instance.EnemyInfos[EnemyId].atk_spd / 100;
        TargetDelay = (float)EverythingLoader.Instance.EnemyInfos[EnemyId].targetdelay / 100;
        Exp = EverythingLoader.Instance.EnemyInfos[EnemyId].exp;
        Reward = EverythingLoader.Instance.EnemyInfos[EnemyId].reward;
        HpReward = EverythingLoader.Instance.EnemyInfos[EnemyId].hpReward;
        Sprite = EverythingLoader.Instance.EnemyInfos[EnemyId].sprite;
        Size = EverythingLoader.Instance.EnemyInfos[EnemyId].size;
        MoveAbility = EverythingLoader.Instance.EnemyInfos[EnemyId].moveAbility;
        ShootAbility = EverythingLoader.Instance.EnemyInfos[EnemyId].shootAbility;

        isLoaded = true;
    }
}
