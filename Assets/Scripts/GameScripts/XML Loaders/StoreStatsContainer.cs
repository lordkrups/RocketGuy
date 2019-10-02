using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Linq;

[XmlRoot("StoreStats")]

public class StoreStatsContainer
{
    [XmlArray("StoreStat")]
    [XmlArrayItem("Stat")]
    public List<StoreStats> storeStatsList = new List<StoreStats>();

    public static XElement Load(string path)
    {

        TextAsset xml = Resources.Load<TextAsset>("Xml/" + path);

        var doc = XDocument.Parse(xml.text);
        return doc.Element("StoreStats");
    }
}
