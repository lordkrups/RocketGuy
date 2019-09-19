using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public class PowerUpLoader
{
    public const string path = "powerup_data";

    public Dictionary<int, PowerUp> PowerUpInfos { get; private set; }

    public void Init()
    {
        XElement puc = PowerUpContainer.Load(path);

        PowerUpInfos = puc.Element("PowerUps").Elements("PowerUp").Select(c => new PowerUp().Set(c)).ToDictionary(info => info.id);

    }

    public Dictionary<int, PowerUp> GetDict()
    {
        XElement puc = PowerUpContainer.Load(path);

        PowerUpInfos = puc.Element("PowerUps").Elements("PowerUp").Select(c => new PowerUp().Set(c)).ToDictionary(info => info.id);

        return PowerUpInfos;
    }
}
