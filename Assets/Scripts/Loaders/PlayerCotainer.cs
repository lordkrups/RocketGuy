using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Linq;

public class PlayerCotainer
{
    [XmlArray("Players")]
    [XmlArrayItem("Player")]
    public List<Player> playerList = new List<Player>();



    public static XElement Load(string path)
    {

        TextAsset xml = Resources.Load<TextAsset>("Xml/" + path);

        var doc = XDocument.Parse(xml.text);
        return doc.Element("PlayerData");
    }
}
