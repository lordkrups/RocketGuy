using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsHolder : MonoBehaviour
{
    public AudioManager audioManager;

    public List<UISprite> slotSpritesReel;
    public List<UISprite> slotSpritesRandomPosition;
    public int prizeToAward;
    public bool finishedSetting;
    public bool finMoves;
    public bool finAllMoves;
    public bool prizeShown;
    public int spins;
    public int totalPowers;
    public float moveY;
    public float speed;
    public float timeToMove;
    public int indexOfPrize;


    public void Init(int prize, AudioManager am)
    {
        audioManager = am;
        finishedSetting = false;

        prizeToAward = prize;

        totalPowers = slotSpritesReel.Count;
        int ran = 0;

        //for (int i = 0; i < count; i++)
        while (slotSpritesReel.Count > 0)
        {
            ran = Random.Range(0, slotSpritesReel.Count);

            if (slotSpritesReel[ran] != null)
            {
                slotSpritesRandomPosition.Add(slotSpritesReel[ran]);
                slotSpritesReel.Remove(slotSpritesReel[ran]);
            }
        }
        ResetWheels();


    }

    public void ResetWheels()
    {
        finishedSetting = false;

        finMoves = false;
        finAllMoves = false;
        prizeShown = false;
        indexOfPrize = 0;
        timeToMove = 0;
        totalPowers = slotSpritesRandomPosition.Count;
        finishedSetting = false;

        while (slotSpritesRandomPosition.Count > 0)
        {
            slotSpritesReel.Add(slotSpritesRandomPosition[0]);
            slotSpritesRandomPosition.Remove(slotSpritesRandomPosition[0]);
        }
        int ran = 0;

        while (slotSpritesReel.Count > 0)
        {
            ran = Random.Range(0, slotSpritesReel.Count);

            if (slotSpritesReel[ran] != null)
            {
                slotSpritesRandomPosition.Add(slotSpritesReel[ran]);
                slotSpritesReel.Remove(slotSpritesReel[ran]);
            }
        }

        SetPosition();
    }
    public void SetPosition()
    {
        //Debug.Log("set pos");
        for (int i = 0; i < slotSpritesRandomPosition.Count; i++)
        {
            slotSpritesRandomPosition[i].transform.localPosition = new Vector2(0f, 160f * i);
        }
        finishedSetting = true;

    }
    public void Spin()
    {

        StartCoroutine(SpinControl());
        StartCoroutine(ShowPrize());


    }
    public void Repos()
    {
        SetPosition();

    }
    IEnumerator SmoothSpin(UISprite sprite, bool showPrize = false)
    {
        if (showPrize)
        {
            prizeShown = false;

            var currentPos = sprite.transform.localPosition;
            var t = 0f;
            Vector3 newPos = new Vector3(0f, sprite.transform.localPosition.y + (moveY * (indexOfPrize - 1)), 0f);

            while (t < 1)
            {
                t += Time.deltaTime / timeToMove * speed;
                sprite.transform.localPosition = Vector3.Lerp(currentPos, newPos, t);
                yield return 0;
            }
            prizeShown = true;
            finAllMoves = true;

        }
        else
        {
            finMoves = false;

            var currentPos = sprite.transform.localPosition;
            var t = 0f;
            Vector3 newPos = new Vector3(0f, sprite.transform.localPosition.y + (moveY * (totalPowers - 1)), 0f);

            while (t < 1)
            {
                t += Time.deltaTime / timeToMove * speed;
                sprite.transform.localPosition = Vector3.Lerp(currentPos, newPos, t);
                yield return 0;
            }
            finMoves = true;
        }
        yield return null;

    }

    IEnumerator SpinControl()
    {
        finAllMoves = false;

        //Debug.Log("SpinControl");

        for (int i = 0; i < spins; i++)
        {
            timeToMove++;
            for (int x = 0; x < slotSpritesRandomPosition.Count; x++)
            {
                StartCoroutine(SmoothSpin(slotSpritesRandomPosition[x]));
            }

            yield return new WaitUntil(() => finMoves == true);

            slotSpritesReel.Add(slotSpritesRandomPosition[totalPowers-1]);
            slotSpritesRandomPosition.Remove(slotSpritesRandomPosition[totalPowers - 1]);

            slotSpritesRandomPosition.Insert(0, slotSpritesReel[0]);
            slotSpritesReel.Remove(slotSpritesReel[0]);

            SetPosition();
        }

        yield return null;
    }
    IEnumerator ShowPrize()
    {
        yield return new WaitUntil(() => finMoves == true);

        for (int i = 0; i < slotSpritesRandomPosition.Count; i++)
        {
            if (prizeToAward == slotSpritesRandomPosition[i].GetComponent<ValueToPass>().valueToPass)
            {
                audioManager.PlaySFXClip("spinend");

                indexOfPrize = i + 1;

                for (int x = 0; x < indexOfPrize; x++)
                {

                    StartCoroutine(SmoothSpin(slotSpritesRandomPosition[x], true));
                }
                yield return null;
            }
        }

        yield return null;
    }
}
/*
public class SlotsHolder : MonoBehaviour
{
    public List<UISprite> slotSpritesReel;
    public List<UISprite> slotSpritesRandomPosition;
    public bool finMove;
    public int spins;
    public int totalPowers;
    public float moveY;
    public float speed;
    public float timeToMove;

    public void Start()
    {
        totalPowers = slotSpritesReel.Count;
        int ran = 0;

        //for (int i = 0; i < count; i++)
        while (slotSpritesReel.Count > 0)
        {
            ran = Random.Range(0, slotSpritesReel.Count);

            if (slotSpritesReel[ran] != null)
            {
                slotSpritesRandomPosition.Add(slotSpritesReel[ran]);
                slotSpritesReel.Remove(slotSpritesReel[ran]);
            }
        }
        SetPosition();


    }
    public void SetPosition()
    {
        Debug.Log("set pos");
        for (int i = 0; i < slotSpritesRandomPosition.Count; i++)
        {
            slotSpritesRandomPosition[i].transform.localPosition = new Vector2(0f, 160f * i);
        }
    }
    public void Spin()
    {

        StartCoroutine(SpinControl());


    }
    public void Repos()
    {
        SetPosition();

    }
    IEnumerator SmoothSpin(UISprite sprite)
    {
        finMove = false;

        var currentPos = sprite.transform.localPosition;
        var t = 0f;
        Vector3 newPos = new Vector3(0f, sprite.transform.localPosition.y + moveY, 0f);

        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            sprite.transform.localPosition = Vector3.Lerp(currentPos, newPos, t);
            yield return null;
        }
        finMove = true;
    }

    IEnumerator SpinControl()
    {
        for (int i = 0; i < spins; i++)
        {
            for (int x = 0; x < slotSpritesRandomPosition.Count; x++)
            {
                StartCoroutine(SmoothSpin(slotSpritesRandomPosition[x]));
            }

            yield return new WaitUntil(() => finMove == true);

            slotSpritesReel.Add(slotSpritesRandomPosition[8]);
            slotSpritesRandomPosition.Remove(slotSpritesRandomPosition[8]);

            slotSpritesRandomPosition.Insert(0, slotSpritesReel[0]);
            slotSpritesReel.Remove(slotSpritesReel[0]);

            SetPosition();
        }

        yield return null;
    }
}
*/
