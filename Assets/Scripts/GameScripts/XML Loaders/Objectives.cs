using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Collections.Generic;
using System;

public class Objectives
{ 
    [XmlAttribute("id")]
    public int id;

    [XmlAttribute("obj")]
    public string obj;

    [XmlAttribute("objtype")]
    public string objtype;

    [XmlAttribute("objvalue")]
    public int objvalue;

    [XmlAttribute("objdescription")]
    public string objdescription;

    [XmlAttribute("objreward")]
    public int objreward;


    public Objectives Set(XElement e)
    {
        id = e.GetInt("id");

        obj = e.GetString("obj");
        objtype = e.GetString("objtype");
        objvalue = e.GetInt("objvalue");
        objdescription = e.GetString("objdescription");
        objreward = e.GetInt("objreward");

        return this;
    }
}
