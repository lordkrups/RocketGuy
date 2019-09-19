using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Linq;

public class GameStatsLoader
{
    public const string path = "game_stats";

    public Dictionary<int, GameStats> GameStatInfos { get; private set; }

    public void Init()
    {
        XElement gsc = GameStatsContainer.Load(path);

        GameStatInfos = gsc.Element("GameStat").Elements("Game").Select(c => new GameStats().Set(c)).ToDictionary(info => info.id);

    }
    public Dictionary<int, GameStats> GetDict()
    {
        XElement gsc = GameStatsContainer.Load(path);

        GameStatInfos = gsc.Element("GameStat").Elements("Game").Select(c => new GameStats().Set(c)).ToDictionary(info => info.id);

        return GameStatInfos;

    }
}
