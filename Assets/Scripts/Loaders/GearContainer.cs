using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Linq;

[XmlRoot("HatData")]

public class HatContainer
{
    [XmlArray("Hats")]
    [XmlArrayItem("Hat")]
    public List<Hat> hatList = new List<Hat>();



    public static XElement Load(string path)
    {

        TextAsset xml = Resources.Load<TextAsset>("Xml/" + path);

        var doc = XDocument.Parse(xml.text);
        return doc.Element("HatData");
    }
}
