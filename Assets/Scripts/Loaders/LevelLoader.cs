using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Linq;

public class LevelLoader
{
    public const string path = "level_data";

    public Dictionary<int, Level> LevelInfos { get; private set; }

    public void Init()
    {
        XElement lc = LevelContainer.Load(path);

        LevelInfos = lc.Element("Levels").Elements("Level").Select(c => new Level().Set(c)).ToDictionary(info => info.id);

    }
    public Dictionary<int, Level> GetDict()
    {
        XElement lc = EnemyContainer.Load(path);

        LevelInfos = lc.Element("Levels").Elements("Level").Select(c => new Level().Set(c)).ToDictionary(info => info.id);

        return LevelInfos;

    }
}
