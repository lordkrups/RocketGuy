using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

public class Player
{
    [XmlAttribute("id")]
    public int id;

    [XmlAttribute("name")]
    public string name;

    [XmlAttribute("sprite")]
    public string sprite;

    [XmlAttribute("experience_points")]
    public int startXP;

    [XmlAttribute("base_weapon")]
    public int weapon;

    [XmlAttribute("base_health")]
    public int startHP;

    [XmlAttribute("base_heal_perc")]
    public int baseHealPerc;

    [XmlAttribute("base_attack")]
    public int atk;

    [XmlAttribute("crit_hit_rate")]
    public float critHitRate;

    [XmlAttribute("crit_hit_dmg_percentage")]
    public float critDmgPerc;

    [XmlAttribute("move_speed")]
    public float moveSpeed;

    [XmlAttribute("attack_speed")]
    public float atkSpeed;

    [XmlAttribute("attack_range")]
    public float atkRange;

    [XmlAttribute("range_dmg_red")]
    public int rangeDmgRed;

    [XmlAttribute("collide_dmg_red")]
    public int collideDmgRed;

    [XmlAttribute("crit_red")]
    public float critRed;

    [XmlAttribute("range_red")]
    public float rangeRed;

    [XmlAttribute("no_of_levels")]
    public int maxNumberOfLevels;

    [XmlAttribute("level_up_experience")]
    public string level_up_table;

    [XmlAttribute("purchase_cost")]
    public int purchaseCost;

    [XmlAttribute("description")]
    public string description;

    public Player Set(XElement e)
    {
        id = e.GetInt("id");
        name = e.GetString("name");
        sprite = e.GetString("sprite");
        weapon = e.GetInt("base_weapon");
        startXP = e.GetInt("experience_points");
        startHP = e.GetInt("base_health");
        baseHealPerc = e.GetInt("base_heal_perc");
        atk = e.GetInt("base_attack");
        critHitRate = e.GetFloat("crit_hit_rate");
        critDmgPerc = e.GetFloat("crit_hit_dmg_percentage");
        moveSpeed = e.GetFloat("move_speed");
        atkSpeed = e.GetFloat("attack_speed");
        atkRange = e.GetFloat("attack_range");
        rangeDmgRed = e.GetInt("range_dmg_red");
        collideDmgRed = e.GetInt("collide_dmg_red");
        critRed = e.GetFloat("crit_red");
        rangeRed = e.GetFloat("range_red");
        maxNumberOfLevels = e.GetInt("no_of_levels");
        level_up_table = e.GetString("level_up_experience");
        purchaseCost = e.GetInt("purchase_cost");
        description = e.GetString("description");

        return this;
    }
}
