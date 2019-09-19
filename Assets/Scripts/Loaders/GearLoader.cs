using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public class HatLoader
{
    public const string path = "hat_data";

    public Dictionary<int, Hat> HatInfos { get; private set; }

    public void Init()
    {
        XElement hc = HatContainer.Load(path);

        HatInfos = hc.Element("Hats").Elements("Hat").Select(c => new Hat().Set(c)).ToDictionary(info => info.id);

    }

    public Dictionary<int, Hat> GetDict()
    {
        XElement hc = HatContainer.Load(path);

        HatInfos = hc.Element("Hats").Elements("Hat").Select(c => new Hat().Set(c)).ToDictionary(info => info.id);

        return HatInfos;
    }
}
