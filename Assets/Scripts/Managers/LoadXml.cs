using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class LoadXml : MonoBehaviour
{
    public TextAsset xmlRawFile;

    // Start is called before the first frame update
    void Start()
    {
        string data = xmlRawFile.text;
        ParseXMLFile(data);
    }

    private void ParseXMLFile(string xmlData)
    {
        string totVal = "";

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        string xmlPathPattern = "/EnemyData/Enemy";

        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);

        foreach (XmlNode node in myNodeList )
        {
            XmlNode enemyid = node.FirstChild;
            XmlNode animal = node.NextSibling;
            XmlNode sprite = node.NextSibling;

            totVal += "EnemyID: " + enemyid.InnerXml + " Animal: " + animal + " Sprite: " + sprite;
        }

        Debug.Log(totVal);
    }
}
