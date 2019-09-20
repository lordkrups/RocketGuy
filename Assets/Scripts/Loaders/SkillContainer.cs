﻿using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Linq;

[XmlRoot("SkillData")]

public class SkillContainer
{
    [XmlArray("Skills")]
    [XmlArrayItem("Skill")]
    public List<Skill> powerUpList = new List<Skill>();



    public static XElement Load(string path)
    {

        TextAsset xml = Resources.Load<TextAsset>("Xml/" + path);

        var doc = XDocument.Parse(xml.text);
        return doc.Element("SkillData");
    }
}