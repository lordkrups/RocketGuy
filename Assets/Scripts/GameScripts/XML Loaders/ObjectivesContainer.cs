using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Linq;

[XmlRoot("ObjectiveList")]

public class ObjectivesContainer
{
    [XmlArray("Objectives")]
    [XmlArrayItem("Objective")]
    public List<Objectives> objectivesList = new List<Objectives>();

    public static XElement Load(string path)
    {

        TextAsset xml = Resources.Load<TextAsset>("Xml/" + path);

        var doc = XDocument.Parse(xml.text);
        return doc.Element("ObjectiveList");
    }
}
