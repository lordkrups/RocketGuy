using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public class SkillLoader
{
    public const string path = "skill_data";

    public Dictionary<int, Skill> SkillInfos { get; private set; }

    public void Init()
    {
        XElement sc = SkillContainer.Load(path);

        SkillInfos = sc.Element("Skills").Elements("Skill").Select(c => new Skill().Set(c)).ToDictionary(info => info.id);

    }

    public Dictionary<int, Skill> GetDict()
    {
        XElement sc = SkillContainer.Load(path);

        SkillInfos = sc.Element("Skills").Elements("Skill").Select(c => new Skill().Set(c)).ToDictionary(info => info.id);

        return SkillInfos;
    }
}
