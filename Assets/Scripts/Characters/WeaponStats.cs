using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats
{
    [SerializeField]
    private int _weaponId;
    [SerializeField]
    private string _sprite;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private int _dmgMod;
    [SerializeField]
    private string _pattern;
    [SerializeField]
    private string _add_pattern;
    [SerializeField]
    private bool wall_passage;

    public int WeaponId { get => _weaponId; private set => _weaponId = value; }
    public string Sprite { get => _sprite; set => _sprite = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public int DmgMod { get => _dmgMod; private set => _dmgMod = value; }
    public string Pattern { get => _pattern; set => _pattern = value; }
    public string Add_pattern { get => _add_pattern; set => _add_pattern = value; }
    public bool Wall_passage { get => wall_passage; set => wall_passage = value; }

    public void Init(int wId)
    {
        WeaponId = wId;
        LoadSettings();
    }

    public void LoadSettings()
    {
        Sprite = EverythingLoader.Instance.WeaponInfos[WeaponId].sprite;
        Speed = EverythingLoader.Instance.WeaponInfos[WeaponId].speed / 10;
        DmgMod = EverythingLoader.Instance.WeaponInfos[WeaponId].dmgMod;
        Pattern = EverythingLoader.Instance.WeaponInfos[WeaponId].pattern;
        Add_pattern = EverythingLoader.Instance.WeaponInfos[WeaponId].add_pattern;
        Wall_passage = (EverythingLoader.Instance.WeaponInfos[WeaponId].wall_passage != 0);
        //Wall_passage &= levelManager.weaponLoader.WeaponInfos[WeaponId].wall_passage != 0;
    }
}
