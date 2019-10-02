using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StoreStats : MonoBehaviour
{
    [XmlAttribute("id")]
    public int id;

    [XmlAttribute("name")]
    public string statname;

    [XmlAttribute("stat")]
    public string stat;

    [XmlAttribute("cost")]
    public string cost;

    [XmlAttribute("sprite")]
    public string sprite;

    [XmlAttribute("description")]
    public string description;


    public StoreStats Set(XElement e)
    {
        id = e.GetInt("id");
        statname = e.GetString("name");
        stat = e.GetString("stat");
        cost = e.GetString("cost");
        sprite = e.GetString("sprite");
        description = e.GetString("description");


        return this;
    }
}
