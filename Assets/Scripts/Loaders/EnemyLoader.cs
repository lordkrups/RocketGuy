using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Linq;

public class EnemyLoader
{
    public const string path = "enemy_data";

    public Dictionary<int, Enemy> EnemyInfos { get; private set; }

    public void Init()
    {
        XElement ec = EnemyContainer.Load(path);

        EnemyInfos = ec.Element("Enemies").Elements("Enemy").Select(c => new Enemy().Set(c)).ToDictionary(info => info.id);

    }
    public Dictionary<int, Enemy> GetDict()
    {
        XElement ec = EnemyContainer.Load(path);

        EnemyInfos = ec.Element("Enemies").Elements("Enemy").Select(c => new Enemy().Set(c)).ToDictionary(info => info.id);

        return EnemyInfos;

    }
}
