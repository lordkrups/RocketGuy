using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public UIWidget blind;

    public UISprite healthInfo;
    public UISprite fuelInfo;
    public UISprite heightInfo;
    public UISprite timeinfo;
    public UISprite boosterFuelInfo;
    public UISprite speedInfo;
    public UISprite objectiveInfo;
    public UISprite upInfo;
    public UISprite upSidesInfo;
    public UISprite downInfo;
    public UISprite finalInfo;

    public int tutorialStage;

    // Start is called before the first frame update
    void Start()
    {
        if (RocketGameManager.Instance.currentObjectiveCounter == 0)
        {
            blind.On();
            ProgressTutorial();
        }
    }

    public void ProgressTutorial()
    {
        tutorialStage++;

        if (tutorialStage == 1)
        {
            healthInfo.On();
        }
        if (tutorialStage == 2)
        {
            healthInfo.Off();
            fuelInfo.On();
        }
        if (tutorialStage == 3)
        {
            fuelInfo.Off();
            heightInfo.On();
        }
        if (tutorialStage == 4)
        {
            heightInfo.Off();
            timeinfo.On();
        }
        if (tutorialStage == 5)
        {
            timeinfo.Off();
            boosterFuelInfo.On();
        }
        if (tutorialStage == 6)
        {
            boosterFuelInfo.Off();
            speedInfo.On();
        }
        if (tutorialStage == 7)
        {
            speedInfo.Off();
            objectiveInfo.On();
        }
        if (tutorialStage == 8)
        {
            objectiveInfo.Off();
            upInfo.On();
        }
        if (tutorialStage == 9)
        {
            upInfo.Off();
            upSidesInfo.On();
        }
        if (tutorialStage == 10)
        {
            upSidesInfo.Off();
            downInfo.On();
        }
        if (tutorialStage == 11)
        {
            downInfo.Off();
            finalInfo.On();
        }
        if (tutorialStage == 12)
        {
            finalInfo.Off();
            blind.Off();
        }
    }
}
