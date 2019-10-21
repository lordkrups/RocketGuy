using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingLevelWidget : MonoBehaviour
{
    public MainMenuManager mainMenuManager;

    public int objLevel;

    public UILabel levelTitle;
    public UILabel levelDescription;
    public UILabel rewardTotal;
    public UILabel playButtonLabel;

    // Start is called before the first frame update
    public void Init(int lvlTitle, string lvlDesc, int rwdTotal, MainMenuManager mMM)
    {
        mainMenuManager = mMM;
        objLevel = lvlTitle;

        levelTitle.text = "Flight plan " + lvlTitle.ToString();
        levelDescription.text = lvlDesc;
        rewardTotal.text = "reward " +  rwdTotal.ToString() + "gp";

        if (objLevel < RocketGameManager.Instance.persistantObjectiveCounter)
        {
            rewardTotal.text = "completed";
            playButtonLabel.text = "completed";

        }

        if (objLevel > RocketGameManager.Instance.persistantObjectiveCounter)
        {
            rewardTotal.text = "locked";
            playButtonLabel.text = "locked";
        }
    }

    public void BeginFlight()
    {
        if (objLevel <= RocketGameManager.Instance.persistantObjectiveCounter)
        {
            RocketGameManager.Instance.currentObjectiveCounter = objLevel;
            mainMenuManager.StartTrainingLevel();
        }
    }
}
