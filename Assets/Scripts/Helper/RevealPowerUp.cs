using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealPowerUp : MonoBehaviour
{
    public LevelManager levelManager;

    public UILabel powerUpOneText;
    public UILabel powerUpTwoText;
    public UILabel powerUpThreeText;

    public SlotsHolder leftSlot;
    public SlotsHolder centerSlot;
    public SlotsHolder rightSlot;

    public int powerUpOne;
    public int powerUpTwo;
    public int powerUpThree;

    public bool powerUpSelected;

    public void Init()
    {
        powerUpSelected = false;


        powerUpOne = 0;
        powerUpOne = 0;
        powerUpOne = 0;

    }
    public void ResetWheels()
    {
        ClearPowerUpText();
        leftSlot.ResetWheels();
        centerSlot.ResetWheels();
        rightSlot.ResetWheels();
    }
    public void RemovePowerUp(int x)
    {
        Debug.Log("RemovePowerUp" + x);

        for (int i = 0; i < leftSlot.slotSpritesRandomPosition.Count; i++)
        {
            if (x == leftSlot.slotSpritesRandomPosition[i].GetComponent<ValueToPass>().valueToPass)
            {
                leftSlot.slotSpritesRandomPosition[i].Off();
                Debug.Log("RemovePowerUp leftSlot " + leftSlot.slotSpritesRandomPosition[i].name);
                leftSlot.slotSpritesRandomPosition.Remove(leftSlot.slotSpritesRandomPosition[i]);
            }
        }
        for (int i = 0; i < centerSlot.slotSpritesRandomPosition.Count; i++)
        {
            if (x == centerSlot.slotSpritesRandomPosition[i].GetComponent<ValueToPass>().valueToPass)
            {
                centerSlot.slotSpritesRandomPosition[i].Off();
                Debug.Log("RemovePowerUp centerSlot " + centerSlot.slotSpritesRandomPosition[i].name);
                centerSlot.slotSpritesRandomPosition.Remove(centerSlot.slotSpritesRandomPosition[i]);
            }
        }
        for (int i = 0; i < rightSlot.slotSpritesRandomPosition.Count; i++)
        {
            if (x == rightSlot.slotSpritesRandomPosition[i].GetComponent<ValueToPass>().valueToPass)
            {
                rightSlot.slotSpritesRandomPosition[i].Off();
                Debug.Log("RemovePowerUp rightSlot " + rightSlot.slotSpritesRandomPosition[i].name);
                rightSlot.slotSpritesRandomPosition.Remove(rightSlot.slotSpritesRandomPosition[i]);
            }
        }
        ResetWheels();
    }
    public void SetPowerUp(int pu1, int pu2, int pu3)
    {
        powerUpOne = pu1;
        powerUpTwo = pu2;
        powerUpThree = pu3;

        leftSlot.Init(powerUpOne, levelManager.audioManager);
        centerSlot.Init(powerUpTwo, levelManager.audioManager);
        rightSlot.Init(powerUpThree, levelManager.audioManager);

        StartCoroutine(Spin());

    }

    IEnumerator Spin()
    {

        yield return new WaitUntil(() => leftSlot.finishedSetting == true);
        yield return new WaitUntil(() => centerSlot.finishedSetting == true);
        yield return new WaitUntil(() => rightSlot.finishedSetting == true);


        leftSlot.Spin();
        centerSlot.Spin();
        rightSlot.Spin();
        levelManager.audioManager.PlaySFXClip("spin");

        yield return new WaitUntil(() => leftSlot.finAllMoves == true);
        yield return new WaitUntil(() => centerSlot.finAllMoves == true);
        yield return new WaitUntil(() => rightSlot.finAllMoves == true);

        ExplainPowerUpText();

        yield return 0;
    }

    public void SelectPowerUp(int box)
    {
        if (leftSlot.finAllMoves && centerSlot.finAllMoves && rightSlot.finAllMoves)
        {
            //Debug.Log("selected power: " + box);
            levelManager.playerBrain.AddPowerUp(box);
            powerUpSelected = true;

            leftSlot.finAllMoves = false;
            centerSlot.finAllMoves = false;
            rightSlot.finAllMoves = false;
        }


    }
    public void ClearPowerUpText()
    {
        powerUpOneText.text = "";
        powerUpTwoText.text = "";
        powerUpThreeText.text = "";
    }
    public void ExplainPowerUpText()
    {
        powerUpOneText.text = EverythingLoader.Instance.PowerUpInfos[powerUpOne].powerDesc;
        powerUpTwoText.text = EverythingLoader.Instance.PowerUpInfos[powerUpTwo].powerDesc;
        powerUpThreeText.text = EverythingLoader.Instance.PowerUpInfos[powerUpThree].powerDesc;
    }
}
