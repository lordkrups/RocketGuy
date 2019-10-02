using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Linq;

public class StoreStatsLoader
{
    public const string path = "store_stats";

    public Dictionary<int, StoreStats> StoreStatInfos { get; private set; }

    public Dictionary<int, StoreStats> GetDict()
    {
        XElement gsc = StoreStatsContainer.Load(path);

        StoreStatInfos = gsc.Element("StoreStat").Elements("Stat").Select(c => new StoreStats().Set(c)).ToDictionary(info => info.id);

        return StoreStatInfos;

    }
}
