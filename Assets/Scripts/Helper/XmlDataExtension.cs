using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

public static class XmlDataExtension 
{
    public static Color GetColorFromHex(this XElement e, string name)
    {
        var xAttribute = e.Attribute(name);
        if (xAttribute != null) return xAttribute.Value.ToColor();
        return Color.white;
    }
    public static string ToHexCode(this Color c)
    {
        return ToHexCode((Color32)c);
    }
    public static string ToHexCode(this Color32 c)
    {
        var r = Convert.ToString(c.r, 16);
        if (r.Length == 1) r = "0" + r;
        var g = Convert.ToString(c.g, 16);
        if (g.Length == 1) g = "0" + g;
        var b = Convert.ToString(c.b, 16);
        if (b.Length == 1) b = "0" + b;
        var a = Convert.ToString(c.a, 16);
        if (a.Length == 1) a = "0" + a;
        return string.Format("{0}{1}{2}{3}", r, g, b, a);
    }

    public static Color ToColor(this string hc)
    {
        if (hc.Length < 6) return Color.white;
        byte r = byte.Parse(hc.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hc.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hc.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        byte a = hc.Length == 6 ? (byte) 255 : byte.Parse(hc.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, a);
    }

    private static float GetNextFloat(string text, ref int index)
    {
        if (index == -1 || string.IsNullOrEmpty(text))
        {
            index = -1;
            return 0;
        }
        int nextIndex = text.IndexOf(",", index);
        float v;
        if (nextIndex == -1)
        {
            float.TryParse(text.Substring(index, text.Length - index), out v);
            index = -1;
        }
        else
        {
            float.TryParse(text.Substring(index, nextIndex - index), out v);
            index = nextIndex + 1;
        }
        return v;
    }
    private static int GetNextInt(string text, ref int index)
    {
        return (int) GetNextFloat(text, ref index);
    }

    public static Vector4 GetVector4(this XElement e, string name, Vector4 defaultValue = new Vector4())
    {
        string text = e.GetString(name);
        if (string.IsNullOrEmpty(text))
        {
            return defaultValue;
        }
        int index = 0;
        float x = GetNextFloat(text, ref index);
        float y = GetNextFloat(text, ref index);
        float z = GetNextFloat(text, ref index);
        float w = GetNextFloat(text, ref index);
        return new Vector4(x, y, z, w);
    }
    public static void SetAttributeVector(this XElement e, string name, Vector4 vec)
    {
        var sb = new StringBuilder();
        sb.Append(vec.x.ToString());
        sb.Append(",");
        sb.Append(vec.y.ToString());
        sb.Append(",");
        sb.Append(vec.z.ToString());
        sb.Append(",");
        sb.Append(vec.w.ToString());
        e.SetAttributeValue(name, sb.ToString());
    }

    public static Vector3 GetVector3(this XElement e, string name, Vector3 defaultValue = new Vector3())
    {
        string text = e.GetString(name);
        if (string.IsNullOrEmpty(text))
        {
            return defaultValue;
        }
        int index = 0;
        float x = GetNextFloat(text, ref index);
        float y = GetNextFloat(text, ref index);
        float z = GetNextFloat(text, ref index);
        return new Vector3(x, y, z);
    }
    public static void SetAttributeVector(this XElement e, string name, Vector3 vec)
    {
        var sb = new StringBuilder();
        sb.Append(vec.x.ToString());
        sb.Append(",");
        sb.Append(vec.y.ToString());
        sb.Append(",");
        sb.Append(vec.z.ToString());
        e.SetAttributeValue(name, sb.ToString());
    }

    public static Vector2 GetVector2(this XElement e, string name, Vector2 defaultValue = new Vector2())
    {
        string text = e.GetString(name);
        if (string.IsNullOrEmpty(text))
        {
            return defaultValue;
        }
        int index = 0;
        float x = GetNextFloat(text, ref index);
        float y = GetNextFloat(text, ref index);
        return new Vector2(x, y);
    }
    public static void SetAttributeVector(this XElement e, string name, Vector2 vec)
    {
        var sb = new StringBuilder();
        sb.Append(vec.x.ToString());
        sb.Append(",");
        sb.Append(vec.y.ToString());
        e.SetAttributeValue(name, sb.ToString());
    }

    public static string GetValueString(this XElement e)
    {
        return e.Value.Replace("\\n", "\n");
    }
    public static string GetString(this XElement e, string name)
    {
        var xAttribute = e.Attribute(name);
        if (xAttribute != null) return xAttribute.Value.Replace("\\n", "\n");
        return string.Empty;
    }
    public static string GetString(this XElement e, string name, string defaultString)
    {
        var xAttribute = e.Attribute(name);
        if (xAttribute != null) return xAttribute.Value.Replace("\\n", "\n");
        return defaultString;
    }

    public static string GetText(this XElement e)
    {
        return e.Value.Replace("\\n", "\n");
    }
    public static int GetInt(this XElement e, string name, int defaultValue = 0)
    {
        var xAttribute = e.Attribute(name);
        if (xAttribute != null) return int.Parse(xAttribute.Value);
        return defaultValue;
    }

    public static uint GetUInt(this XElement e, string name)
    {
        var xAttribute = e.Attribute(name);
        if (xAttribute != null) return uint.Parse(xAttribute.Value);
        return 0;
    }

    public static Int64 GetInt64(this XElement e, string name)
    {
        var xAttribute = e.Attribute(name);
        if (xAttribute != null) return Int64.Parse(xAttribute.Value);
        return 0;
    }

    public static byte GetByte(this XElement e, string name)
    {
        var xAttribute = e.Attribute(name);
        if (xAttribute != null) return byte.Parse(xAttribute.Value);
        return 0;
    }

    public static float GetFloat(this XElement e, string name, float defaultVal = 0)
    {
        var xAttribute = e.Attribute(name);
        if (xAttribute != null) return float.Parse(xAttribute.Value);
        return defaultVal;
    }

    public static bool GetBool(this XElement e, string name, bool defaultValue = false)
    {
        var xAttribute = e.Attribute(name);
        if (xAttribute != null)
        {
            return bool.Parse(xAttribute.Value);
        }
        return defaultValue;
    }

    public static DateTime GetDateTime(this XElement e, string name)
    {
        DateTime date;
        if (DateTime.TryParse(e.GetString(name), out date))
        {
            return date;
        }
        return new DateTime();
    }

    public static T GetEnum<T>(this XElement e, string name, T defaultValue = default(T))
    {
        var xAttribute = e.Attribute(name);
        if (xAttribute != null) return (T)Enum.Parse(typeof(T), xAttribute.Value, true);
        return defaultValue;
    }

    public static T GetEnum<T>(this XElement e)
    {
        return (T)Enum.Parse(typeof(T), e.Name.ToString(), true);
    }

    public static int[] GetIntArray(this XElement e, string name, bool ignoreZero = false)
    {
        var intList = new List<int>();
        int index = 0;
        int cnt = 0;
        var text = e.GetString(name);
        while (index != -1)
        {
            cnt++;
            if (cnt > 1000)
            {
                throw new Exception("array is too long..");
            }
            intList.Add(GetNextInt(text, ref index));
        }
        return intList.ToArray();
    }

    public static void SetAttributeIntArray(this XElement e, string key, int[] array)
    {
        if (array == null || array.Length < 1)
        {
            e.SetAttributeValue(key, "0");
            return;
        }
        var sb = new StringBuilder();
        sb.Append(array[0]);
        for (int i = 1; i < array.Length; i++)
        {
            sb.Append(", ");
            sb.Append(array[i]);
        }
        e.SetAttributeValue(key, sb.ToString());
    }

    public static DateTime ToDateTime(this XElement e, string name)
    {
        return DateTime.Parse(e.Attribute(name).Value);
    }
    public static TimeSpan ToTimeSpan(this XElement e, string name)
    {
        return TimeSpan.Parse(e.Attribute(name).Value);
    }

    public static string ToDateString(this DateTime date)
    {
        return date.ToString("yyyy-MM-dd HH:mm:ss.fff");
    }


}
