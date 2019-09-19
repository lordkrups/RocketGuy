using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Collections;

public class S2VerticalLabel : MonoBehaviour
{
    public UILabel basicLabel;
    private List<UILabel> labelList;

    public int fontSize { get; private set; }

    void Awake()
    {
        labelList = new List<UILabel>();
        labelList.Add(basicLabel);
        fontSize = basicLabel.fontSize;
    }

    public void SetString(string t)
    {
        char[] chars = t.ToCharArray();
        int lineIndex = 0;

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] == '\n')
            {
                SetLine(lineIndex, sb.ToString());
                lineIndex++;
                sb = new StringBuilder();
            }
            else
            {
                sb.Append(chars[i]);
                sb.AppendLine();
            }
        }
        if (sb.Length > 0)
        {
            SetLine(lineIndex, sb.ToString());
            lineIndex++;
        }
        for (int i = lineIndex; i < labelList.Count; i++)
        {
            labelList[i].enabled = false;
        }

    }

    public void SetFontSize(int size)
    {
        fontSize = size;
        for (int i = 0; i < labelList.Count; i++)
        {
            labelList[i].fontSize = fontSize;
        }
    }

    public void SetPos(int x, int y)
    {
        for (int i = 0; i < labelList.Count; i++)
        {
            labelList[i].transform.localPosition = new Vector3(x, y, 0);
            x -= fontSize;
        }
    }


    private void SetLine(int lineIndex, string lineText)
    {
        for (int i = labelList.Count; i < lineIndex + 1; i++)
        {
            var newLabel = NGUITools.AddChild(gameObject, basicLabel.gameObject).GetComponent<UILabel>();
            newLabel.fontSize = fontSize;
            labelList.Add(newLabel);
        }
        labelList[lineIndex].enabled = true;
        labelList[lineIndex].text = lineText;
    }





}
