using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Linq;

[XmlRoot("PowerUpData")]


public class PowerUpContainer
{
    [XmlArray("PowerUps")]
    [XmlArrayItem("PowerUp")]
    public List<PowerUp> powerUpList = new List<PowerUp>();



    public static XElement Load(string path)
    {

        TextAsset xml = Resources.Load<TextAsset>("Xml/" + path);

        var doc = XDocument.Parse(xml.text);
        return doc.Element("PowerUpData");
    }
}