using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Linq;

[XmlRoot("EnemyData")]


public class EnemyContainer
{
    [XmlArray("Enemies")]
    [XmlArrayItem("Enemy")]
    public List<Enemy> enemyList = new List<Enemy>();



    public static XElement Load(string path)
    {

        TextAsset xml = Resources.Load<TextAsset>("Xml/" + path);

        var doc = XDocument.Parse(xml.text);
        return doc.Element("EnemyData");
    }
}
