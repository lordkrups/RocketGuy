using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public class ObjectivesLoader
{
    public const string path = "objectives";

    public Dictionary<int, Objectives> ObjectivesInfo { get; private set; }

    public Dictionary<int, Objectives> GetDict()
    {
        XElement gsc = ObjectivesContainer.Load(path);

        ObjectivesInfo = gsc.Element("Objectives").Elements("Objective").Select(c => new Objectives().Set(c)).ToDictionary(info => info.id);

        return ObjectivesInfo;

    }
}
