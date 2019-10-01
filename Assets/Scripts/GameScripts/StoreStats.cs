using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StoreStats : MonoBehaviour
{
    [XmlAttribute("id")]
    public int id;

    [XmlAttribute("max_health")]
    public int max_health;

    [XmlAttribute("engine_upgrades")]
    public float engine_upgrades;

    [XmlAttribute("booster_engine_upgrades")]
    public int booster_engine_upgrades;

    [XmlAttribute("turn_control")]
    public float turn_control;

    [XmlAttribute("damage_multiplier")]
    public int damage_multiplier;

    [XmlAttribute("fuel_tank")]
    public int fuel_tank;

    [XmlAttribute("booster_fuel_tank")]
    public int booster_fuel_tank;

    [XmlAttribute("air_resistance")]
    public float air_resistance;

    [XmlAttribute("fuel_consumption")]
    public float fuel_consumption;

    [XmlAttribute("booster_fuel_consumption")]
    public float booster_fuel_consumption;

    [XmlAttribute("store_discount")]
    public float store_discount;

    [XmlAttribute("diamond_value")]
    public float diamond_value;
}
