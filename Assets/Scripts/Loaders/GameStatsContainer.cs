using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Linq;

[XmlRoot("GameStats")]

public class GameStatsContainer
{
    [XmlArray("GameStat")]
    [XmlArrayItem("Game")]
    public List<GameStats> gameStatsList = new List<GameStats>();



    public static XElement Load(string path)
    {

        TextAsset xml = Resources.Load<TextAsset>("Xml/" + path);

        var doc = XDocument.Parse(xml.text);
        return doc.Element("GameStats");
    }
}
