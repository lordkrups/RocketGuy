using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

public class Enemy
{
    [XmlAttribute("id")]
    public int id;

    [XmlAttribute("animal")]
    public string animal;

    [XmlAttribute("hp")]
    public int hp;

    [XmlAttribute("atk")]
    public int atk;

    [XmlAttribute("moving_type")]
    public int moving_type;

    [XmlAttribute("moving_value")]
    public float moving_value;

    [XmlAttribute("moving_speed")]
    public float moving_speed;

    [XmlAttribute("moving_delay")]
    public float moving_delay;

    [XmlAttribute("atk_type")]
    public int atk_type;

    [XmlAttribute("weapon")]
    public int weapon;

    [XmlAttribute("shots")]
    public int shots;

    [XmlAttribute("atk_spd")]
    public int atk_spd;

    [XmlAttribute("atk_range")]
    public int atk_range;

    [XmlAttribute("target_delay")]
    public float targetdelay;

    [XmlAttribute("exp")]
    public int exp;

    [XmlAttribute("reward")]
    public int reward;

    [XmlAttribute("hp_reward")]
    public int hpReward;

    [XmlAttribute("sprite")]
    public string sprite;

    [XmlAttribute("move_ability")]
    public string moveAbility;

    [XmlAttribute("shoot_ability")]
    public string shootAbility;

    [XmlAttribute("size")]
    public int size;

    public Enemy Set(XElement e)
    {
        id = e.GetInt("id");
        animal = e.GetString("name");
        hp = e.GetInt("hp");
        atk = e.GetInt("atk");
        moving_type = e.GetInt("moving_type");
        moving_value = e.GetFloat("moving_value");
        moving_speed = e.GetFloat("moving_speed");
        moving_delay = e.GetFloat("moving_delay");
        atk_type = e.GetInt("atk_type");
        weapon = e.GetInt("weapon");
        shots = e.GetInt("shots");
        atk_spd = e.GetInt("atk_spd");
        atk_range = e.GetInt("atk_range");
        targetdelay = e.GetFloat("target_delay");
        exp = e.GetInt("exp");
        reward = e.GetInt("reward");
        hpReward = e.GetInt("hp_reward");
        sprite = e.GetString("sprite");
        moveAbility = e.GetString("move_ability");
        shootAbility = e.GetString("shoot_ability");
        size = e.GetInt("size");


        return this;
    }

}
