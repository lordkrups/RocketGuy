using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelParameters : MonoBehaviour
{
    public LevelManager levelManager;
    public Transform camStopRight;
    public AStarManager aStarManager;

    public bool isComplete = false;
    public bool isActive = false;
    public int levelNumber;

    public UISprite bgSprite;
    public UISprite goalSprite;
    public UISprite goalLock;
    
    public List<EnemyMultiBrain> enemiesList;
    public List<Transform> sceneryList;

    // Start is called before the first frame update
    public void Init(LevelManager lM)
    {
        levelManager = lM;

        for (int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].Init(levelManager);
            //enemiesList[i].enemyStats.Init(enemiesList[i].enemyID);

            levelManager.AddEnemy(enemiesList[i].transform);
            Physics2D.IgnoreCollision(enemiesList[i].GetComponent<CapsuleCollider2D>(), goalSprite.GetComponent<CapsuleCollider2D>());
            enemiesList[i].transform.SetParent(levelManager.CharacterPanel.transform);
        }
        for (int i = 0; i < sceneryList.Count; i++)
        {
            sceneryList[i].transform.SetParent(levelManager.SceneryPanel.transform);
        }
        aStarManager.grid.Init();
        goalSprite.Off();
        Reset();
    }

    public void Destruct()
    {
        for (int i = 0; i < sceneryList.Count; i++)
        {
            Destroy(sceneryList[i].gameObject);
        }
    }

    public void OpenGoal()
    {
        goalLock.Off();
        goalSprite.On();

    }

    public void Reset()
    {
        isComplete = false;
        isActive = true;
    }

}
