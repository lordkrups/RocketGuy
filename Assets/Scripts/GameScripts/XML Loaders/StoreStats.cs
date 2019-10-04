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

    public List<float> itemStat = new List<float>();
    public List<int> itemCost = new List<int>();

    public StoreStats Set(XElement e)
    {
        id = e.GetInt("id");
        statname = e.GetString("name");
        stat = e.GetString("stat");
        cost = e.GetString("cost");
        sprite = e.GetString("sprite");
        description = e.GetString("description");


        float[] Vals = Array.ConvertAll(stat.Split(','), float.Parse);
        int[] Costs = Array.ConvertAll(cost.Split(','), int.Parse);
        for (int x = 0; x < Vals.Length; x++)
        {
            //Debug.Log("unlockedGods 1");

            itemStat.Add(Vals[x]);
            itemCost.Add(Costs[x]);
        }
        
        return this;
    }
}
