using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightSchoolManager : MonoBehaviour
{
    public MainMenuManager mainMenuManager;
    public UIGrid gridContainer;
    public TrainingLevelWidget trainingLevelWidget;
    public List<TrainingLevelWidget> levelsList;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < RocketGameManager.Instance.ObjectiveInfos.Count; i++)
        {
            var lvlWidg = Instantiate(trainingLevelWidget, gridContainer.transform);
            lvlWidg.Init(RocketGameManager.Instance.ObjectiveInfos[i].id,
                RocketGameManager.Instance.ObjectiveInfos[i].obj,
                RocketGameManager.Instance.ObjectiveInfos[i].objreward,
                mainMenuManager);

            levelsList.Add(lvlWidg);
        }
        gridContainer.Reposition();

    }
}
