using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

public class Level
{
    [XmlAttribute("id")]
    public int id;

    [XmlAttribute("level_length")]
    public int levelLength;

    [XmlAttribute("mobs_list")]
    public string mobList;

    [XmlAttribute("show_store")]
    public int showStore;

    public Level Set(XElement e)
    {
        id = e.GetInt("id");
        levelLength = e.GetInt("level_length");
        mobList = e.GetString("show_store");
        showStore = e.GetInt("show_store");

        return this;
    }
}
