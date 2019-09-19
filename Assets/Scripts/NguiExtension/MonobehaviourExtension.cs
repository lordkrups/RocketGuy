using System;
using UnityEngine;
using System.Collections;

public static class MonobehaviourExtension
{
    public static void PlayOpen(this GameObject obj)
    {
        obj.On();
        obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        LeanTween.scale(obj, Vector3.one, 0.15f).setEase(LeanTweenType.easeOutBack);
    }

    public static void On(this MonoBehaviour obj)
    {
        obj.gameObject.On();
    }
    public static void Off(this MonoBehaviour obj)
    {
        obj.gameObject.Off();
    }
    public static void On(this GameObject obj)
    {
        obj.SetActive(true);
    }
    public static void Off(this GameObject obj)
    {
        obj.SetActive(false);
    }


    public static IEnumerable PlayUiAlphaEnumerable(this MonoBehaviour obj, float from, float to, float time, float delayTime = 0)
    {
        var widget = obj.GetComponent<UIRect>();
        yield return obj.StartCoroutine(PlayingUiAlpha(widget, from, to, time, delayTime, null));
    }
    public static void PlayUiAlpha(this MonoBehaviour obj, float from, float to, float time, float delayTime = 0, Action finishAction = null)
    {
        var widget = obj.GetComponent<UIRect>();
        obj.StartCoroutine(PlayingUiAlpha(widget, from, to, time, delayTime, finishAction));
    }
    private static IEnumerator PlayingUiAlpha(UIRect ui, float @from, float to, float time, float delayTime, Action finishAction)
    {
        if (delayTime > 0) yield return new WaitForSeconds(delayTime);
        float a = from;
        float spd = (to - from) / time;
        while (time > 0)
        {
            ui.alpha = Mathf.Clamp(a, 0, 1);
            yield return null;
            time -= Time.smoothDeltaTime;
            a += (spd * Time.smoothDeltaTime);
        }
        ui.alpha = Mathf.Clamp(to, 0, 1);
        if (finishAction != null) finishAction();
    }


    public static void PlayUiAlphaPingPong(this MonoBehaviour obj, float from, float to, float final, float frequency,
        float totalTime)
    {
        var ui = obj.GetComponent<UIRect>();
        obj.StartCoroutine(PlayingUiAlphaPingPong(ui, from, to, final, frequency, totalTime));
    }
    private static IEnumerator PlayingUiAlphaPingPong(UIRect ui, float from, float to, float final, float frequency,
        float totalTime)
    {
        float a = from;
        float spd = (to - from) * frequency;
        ui.alpha = a;
        while (totalTime > 0)
        {
            yield return null;
            totalTime -= Time.smoothDeltaTime;
            a += (spd * Time.smoothDeltaTime);
            if (a >= 1)
            {
                a = 1;
                spd = -spd;
            }
            else if (a <= 0)
            {
                a = 0;
                spd = -spd;
            }
            ui.alpha = a;
        }
        ui.alpha = final;
    }

    public static void PlayShakeObject(this MonoBehaviour obj, GameObject target, float amount, float time, Action finishAction)
    {
        obj.StartCoroutine(PlayingShakeObject(target, amount, time, finishAction));
    }
    private static IEnumerator PlayingShakeObject(GameObject target, float amount, float time, Action finishAction)
    {
        var tran = target.transform;
        Vector3 oriPos = tran.localPosition;
        while (time > 0)
        {
            tran.localPosition = oriPos +
                                 new Vector3(UnityEngine.Random.Range(-amount, amount),
                                     UnityEngine.Random.Range(-amount, amount), 0);
            yield return null;
            time -= Time.smoothDeltaTime;
        }
        tran.localPosition = oriPos;
        if (finishAction != null)
        {
            finishAction();
        }
    }


    public static Transform FindChildByName(this Transform tran, string name)
    {
        if (tran.name.Equals(name))
        {
            return tran;
        }
        for (int i = 0; i < tran.childCount; i++)
        {
            var result = FindChildByName(tran.GetChild(i), name);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }

    public static void ResetTransform(this Transform tran, Transform parent = null)
    {
        if (parent != null)
        {
            tran.parent = parent;
        }
        tran.localPosition = Vector3.zero;
        tran.localScale = Vector3.one;
        tran.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public static void SetLayerWithChlidObjects(this Transform tran, int layer)
    {
        for (int i = 0; i < tran.childCount; i++)
        {
            tran.GetChild(i).gameObject.layer = layer;
        }
    }

    public static void ResetAllShaders(this GameObject obj)
    {
        foreach (var r in obj.GetComponentsInChildren<Renderer>())
        {
            if (r.material != null)
            {
                r.material.shader = Shader.Find(r.material.shader.name);
            }
        }
    }

/*
    public static bool IsPrefabObject(this GameObject obj)
    {
        return PrefabUtility.GetPrefabParent(obj) == null && PrefabUtility.GetPrefabObject(obj) != null;
    }
*/

}
