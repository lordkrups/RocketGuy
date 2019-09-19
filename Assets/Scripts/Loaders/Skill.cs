using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

public class Skill
{
    [XmlAttribute("id")]
    public int id;

    [XmlAttribute("multi_pick")]
    public int multiPick;

    [XmlAttribute("skill_name")]
    public string skillName;

    [XmlAttribute("sprite")]
    public string sprite;

    [XmlAttribute("skill_desc")]
    public string skillDesc;

    [XmlAttribute("atk_inc")]
    public float atkInc;

    [XmlAttribute("atk_spd_inc")]
    public int atkSpdInc;

    [XmlAttribute("hp_inc")]
    public int hpInc;

    [XmlAttribute("hp_perc")]
    public int healPerc;

    [XmlAttribute("bulwark")]
    public int bulwark;

    [XmlAttribute("enhance")]
    public int enhance;

    [XmlAttribute("time_reward")]
    public int timeReward;

    [XmlAttribute("inspire")]
    public int inspire;

    [XmlAttribute("glory")]
    public int glory;

    public Skill Set(XElement e)
    {
        id = e.GetInt("id");
        multiPick = e.GetInt("multi_pick");
        skillName = e.GetString("skill_name");
        sprite = e.GetString("sprite");
        skillDesc = e.GetString("skill_desc");
        atkInc = e.GetFloat("atk_inc");
        atkSpdInc = e.GetInt("atk_spd_inc");
        hpInc = e.GetInt("hp_inc");
        healPerc = e.GetInt("hp_perc");
        bulwark = e.GetInt("bulwark");
        enhance = e.GetInt("enhance");
        timeReward = e.GetInt("time_reward");
        inspire = e.GetInt("inspire");
        glory = e.GetInt("glory");

        return this;
    }
}
