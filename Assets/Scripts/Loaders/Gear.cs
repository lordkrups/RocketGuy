using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

public class Hat
{
    [XmlAttribute("id")]
    public int id;

    [XmlAttribute("name")]
    public string name;

	[XmlAttribute("sprite")]
	public string sprite;

	[XmlAttribute("hat_type")]
    public string hatType;

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

    [XmlAttribute("purchase_cost")]
    public int purchaseCost;

    public Hat Set(XElement e)
    {
        id = e.GetInt("id");
		name = e.GetString("name");
        hatType = e.GetString("hatType");
		sprite = e.GetString("sprite");
		powerDesc = e.GetString("power_desc");
        atkInc = e.GetFloat("atk_inc");
        atkSpdInc = e.GetInt("atk_spd_inc");
        critInc = e.GetFloat("crit_inc");
        hpInc = e.GetInt("hp_inc");
        healPerc = e.GetInt("hp_perc");
        purchaseCost = e.GetInt("purchase_cost");

        return this;
    }
}
