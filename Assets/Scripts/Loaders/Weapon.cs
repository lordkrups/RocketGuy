using UnityEngine; using System.Xml; using System.Xml.Serialization; using System.Xml.Linq;  public class Weapon {     [XmlAttribute("id")]     public int id;

    [XmlAttribute("show_store")]     public int show_store;      [XmlAttribute("name")]     public string name;      [XmlAttribute("sprite")]     public string sprite;      [XmlAttribute("speed")]     public float speed;

    [XmlAttribute("dmg_mod")]     public int dmgMod;      [XmlAttribute("pattern")]     public string pattern;      [XmlAttribute("add_pattern")]     public string add_pattern;      [XmlAttribute("wall_passage")]     public int wall_passage;

    [XmlAttribute("purchase_cost")]     public int purchaseCost;      public Weapon Set(XElement e)     {         id = e.GetInt("id");         show_store = e.GetInt("show_store");         speed = e.GetFloat("speed");
        dmgMod = e.GetInt("dmg_mod");         name = e.GetString("name");         sprite = e.GetString("sprite");         pattern = e.GetString("pattern");         add_pattern = e.GetString("add_pattern");         wall_passage = e.GetInt("wall_passage");         purchaseCost = e.GetInt("purchase_cost");          return this;     } }