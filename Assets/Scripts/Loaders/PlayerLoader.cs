using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Linq;


public class PlayerLoader
{
    public const string path = "player_data";

    public Dictionary<int, Player> PlayerInfos { get; private set; }

    public Dictionary<int, Player> GetDict()
    {
        XElement pc = PlayerCotainer.Load(path);

        PlayerInfos = pc.Element("Players").Elements("Player").Select(c => new Player().Set(c)).ToDictionary(info => info.id);

        return PlayerInfos;

    }
}
