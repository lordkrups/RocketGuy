using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameStats
{
    [XmlAttribute("id")]
    public int id;

    [XmlAttribute("play_cost")]
    public int play_cost;

    [XmlAttribute("regen_energy_time")]
    public int regen_energy_time;

    [XmlAttribute("regen_energy_amount")]
    public int regen_energy_amount;

    [XmlAttribute("coin_reward_time")]
    public int coin_reward_time;

    [XmlAttribute("coin_reward_amount")]
    public int coin_reward_amount;

    [XmlAttribute("level_up_experience")]
    public string level_up_table;

    public List<int> persistantExpTable;

    public GameStats Set(XElement e)
    {
        id = e.GetInt("id");
        play_cost = e.GetInt("play_cost");
        regen_energy_time = e.GetInt("regen_energy_time");
        regen_energy_amount = e.GetInt("regen_energy_amount");
        coin_reward_time = e.GetInt("coin_reward_time");
        coin_reward_amount = e.GetInt("coin_reward_amount");
        level_up_table = e.GetString("level_up_experience");

        persistantExpTable = new List<int>();

        return this;
    }

    public bool CheckMaxlevelReached(int level)
    {
        bool _isMaxLevelReached = false;
        if (level == persistantExpTable.Count)
            _isMaxLevelReached = true;

        return _isMaxLevelReached;
    }

    public int GetNextLevelExp(int level)
    {
        Debug.Log("level: " + level);

        if (persistantExpTable.Count == 0)
        {
            int[] expTab = Array.ConvertAll(EverythingLoader.Instance.GameStatsInfos[0].level_up_table.Split(','), int.Parse);
            for (int x = 0; x < expTab.Length; x++)
            {
                //Debug.Log("unlockedGods 1");

                persistantExpTable.Add(expTab[x]);
            }
        }

        int value = 0;

        value = persistantExpTable[level];

        return value;
    }
}
