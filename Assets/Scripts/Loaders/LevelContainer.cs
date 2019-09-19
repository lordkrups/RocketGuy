using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Linq;

[XmlRoot("LevelData")]

public class LevelContainer
{
    [XmlArray("Levels")]
    [XmlArrayItem("Level")]
    public List<Level> levelList = new List<Level>();



    public static XElement Load(string path)
    {

        TextAsset xml = Resources.Load<TextAsset>("Xml/" + path);

        var doc = XDocument.Parse(xml.text);
        return doc.Element("LevelData");
    }
}
