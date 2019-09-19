using System;
using System.Text;
using UnityEngine;
using System.Collections;

public static class S2NguiExtension 
{
    public static IEnumerable PlayTextLabelEnumerable(this UILabel label, string fullText, float speed)
    {
        yield return label.StartCoroutine(_PlayTextLabel(label, fullText, speed, null)); 
    }
    public static void PlayTextLabel(this UILabel label, string fullText, float speed, Action finishAction = null)
    {
        label.StartCoroutine(_PlayTextLabel(label, fullText, speed, finishAction));
    }
    private static IEnumerator _PlayTextLabel(UILabel label, string fullText, float speed, Action finishAction)
    {
        label.text = string.Empty;
        var sb = new StringBuilder();
        int length = fullText.Length;
        int index = 0;
        float progress = 0;
        while (sb.Length < length)
        {
            yield return null;
            progress += (speed*Time.smoothDeltaTime);
            if (progress >= 1)
            {
                int p = (int) progress;
                if (index + p > length) p = length - index;
                sb.Append(fullText.Substring(index, p));
                progress -= p;
                index += p;
                label.text = sb.ToString();
            }
        }
        if (finishAction != null) finishAction();
    }

    public static Coroutine PlayColor(this UIWidget widget, Color toColor, float time, Action finishAction)
    {
        return widget.StartCoroutine(_PlayColor(widget, toColor, time, finishAction));
    }
    private static IEnumerator _PlayColor(UIWidget widget, Color toColor, float time, Action finishAction)
    {
        Color fromColor = widget.color;
        float playingTime = 0;
        while (playingTime < time)
        {
            widget.color = Color.Lerp(fromColor, toColor, playingTime/time);
            yield return null;
            playingTime += Time.smoothDeltaTime;
        }
        widget.color = toColor;
        if (finishAction != null)
        {
            finishAction();
        }
    }
    public static void SetDepth(this UIWidget obj, int depth)
    {
        int d = depth - obj.depth;
        if (d == 0) return;
        foreach (var w in obj.GetComponentsInChildren<UIWidget>())
        {
            w.depth = w.depth + d;
        }
    }

    public static T MakeInstance<T>(this T targetObj, GameObject parent) where T : Component
    {
        return NGUITools.AddChild(parent, targetObj.gameObject).GetComponent<T>();
    }


}
