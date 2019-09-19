using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

public class PowerUp
{
    [XmlAttribute("id")]
    public int id;

    [XmlAttribute("multi_pick")]
    public int multiPick;

    [XmlAttribute("power_name")]
    public string powerName;

    [XmlAttribute("sprite")]
    public string sprite;

    [XmlAttribute("power_desc")]
    public string powerDesc;

    [XmlAttribute("atk_inc")]
    public float atkInc; 

     [XmlAttribute("atk_spd_inc")]
    public int atkSpdInc;

    [XmlAttribute("crit_inc")]
    public float critInc;

    [XmlAttribute("hp_inc")]
    public int hpInc;

    [XmlAttribute("hp_perc")]
    public int healPerc;

    [XmlAttribute("rage")]
    public int rage;

    [XmlAttribute("bloodthirst")]
    public int bloodthirst;

    [XmlAttribute("invincible")]
    public int invincible;

    [XmlAttribute("smart")]
    public int smart;

    public PowerUp Set(XElement e)
    {
        id = e.GetInt("id");
        multiPick = e.GetInt("multi_pick");
        powerName = e.GetString("power_name");
        sprite = e.GetString("sprite");
        powerDesc = e.GetString("power_desc");
        atkInc = e.GetFloat("atk_inc");
        atkSpdInc = e.GetInt("atk_spd_inc");
        critInc = e.GetFloat("crit_inc");
        hpInc = e.GetInt("hp_inc");
        healPerc = e.GetInt("hp_perc");
        rage = e.GetInt("rage");
        bloodthirst = e.GetInt("bloodthirst");
        invincible = e.GetInt("invincible");
        smart = e.GetInt("smart");
        return this;
    }
}
