using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingsSpawnerNGUI : MonoBehaviour
{
    public GameSceneManagerNGUI gameSceneManager;
    public GameObject collectibleContainer;
    public GameObject obstacleContainer;

    public bool firstSpawned;
    public float spawnOffset;
    public float curHeight;
    public float curWidth;
    public float prevHeight;
    public float prevWidth;

    public int numberOfGoodThingsToSpawn;
    public int numberOfBadThingsToSpawn;
    public int xDist;
    public int yDist;

    public List<CollectibleObj> collectibleList;
    public List<PlaneMotor> lowObstacleList;
    public List<PlaneMotor> medObstacleList;

    public List<CollectibleObj> spawnedCollectibleList;
    public List<PlaneMotor> spawnedObstacleList;
    private void Start()
    {
        gameSceneManager = GetComponent<GameSceneManagerNGUI>();
    }
    private void Update()
    {
        curHeight = gameSceneManager.playerRocket.rb.position.y;
        curWidth = gameSceneManager.playerRocket.rb.position.x;

        if (curHeight > (prevHeight + spawnOffset) || curHeight < (prevHeight - spawnOffset))
        {
            prevHeight = curHeight;
            SpawnObstaclesInFrontOfPlayer();
            SpawnCollectiblesInFrontOfPlayer();
            int x = spawnedCollectibleList.Count + spawnedObstacleList.Count;
            Debug.Log(x);

            if (x > 50)
            {
                CullThings();
            }
        }
        if (curWidth > (prevWidth + spawnOffset) || curWidth < (prevWidth - spawnOffset))
        {
            prevWidth = curWidth;
            SpawnObstaclesInFrontOfPlayer();
            SpawnCollectiblesInFrontOfPlayer();

            int x = spawnedCollectibleList.Count + spawnedObstacleList.Count;
            Debug.Log(x);

            if (x > 50)
            {
                CullThings();
            }
        }
        //Debug.Log("curHeight: " + curHeight);
        //Debug.Log("prevHeight + spawnOffset: " + (prevHeight + spawnOffset));

    }
    public void CullThings()
    {
        int x = spawnedCollectibleList.Count - numberOfGoodThingsToSpawn;
        int y = spawnedObstacleList.Count - numberOfBadThingsToSpawn;

        while (spawnedCollectibleList.Count > x)
        {
            spawnedCollectibleList[0].timeToDie = true;
            spawnedCollectibleList.Remove(spawnedCollectibleList[0]);
        }
        while (spawnedObstacleList.Count > y)
        {
            spawnedObstacleList[0].timeToDie = true;
            spawnedObstacleList.Remove(spawnedObstacleList[0]);
        }

    }

    public void SpawnCollectiblesInFrontOfPlayer()
    {
        Debug.Log("SpawnCollectiblesInFrontOfPlayer");
        if (gameSceneManager.currentPhase == "Low")
        {
            for (int i = 0; i < numberOfGoodThingsToSpawn; i++)
            {
                int ran = Random.Range(0, collectibleList.Count);
                var thingToSpawn = collectibleList[ran];

                int xPos = Random.Range(-xDist, xDist + 1);
                int yPos = Random.Range(-yDist, yDist + 1);
                Vector3 pos = new Vector3(gameSceneManager.playerRocket.transform.position.x + xPos,
                                            gameSceneManager.playerRocket.transform.position.y + yPos,
                                                0f);

                thingToSpawn.transform.position = pos;
                Instantiate(thingToSpawn, collectibleContainer.transform, true);

                spawnedCollectibleList.Add(thingToSpawn);
            }
        }
        firstSpawned = true;
    }

    public void SpawnObstaclesInFrontOfPlayer()
    {
        Debug.Log("SpawnObstaclesInFrontOfPlayer");
        if (gameSceneManager.currentPhase == "Low")
        {
            Debug.Log("Low Spawns");
            for (int i = 0; i < numberOfBadThingsToSpawn; i++)
            {
                int ran = Random.Range(0, lowObstacleList.Count);
                var thingToSpawn = lowObstacleList[ran];

                int xPos = Random.Range(-xDist, xDist + 1);
                int yPos = Random.Range(-yDist, yDist + 1);
                Vector3 pos = new Vector3(gameSceneManager.playerRocket.transform.position.x + xPos,
                                            gameSceneManager.playerRocket.transform.position.y + yPos,
                                                0f);

                thingToSpawn.transform.position = pos;
                Instantiate(thingToSpawn, obstacleContainer.transform, true);

                spawnedObstacleList.Add(thingToSpawn);
            }
        }

        if (gameSceneManager.currentPhase == "Medium")
        {
            Debug.Log("Medium Spawns");
            for (int i = 0; i < numberOfBadThingsToSpawn; i++)
            {
                int ran = Random.Range(0, medObstacleList.Count);
                var thingToSpawn = medObstacleList[ran];

                int xPos = Random.Range(-xDist, xDist + 1);
                int yPos = Random.Range(yDist / 5, yDist + 1);
                Vector3 pos = new Vector3(gameSceneManager.playerRocket.transform.position.x - xPos,
                                            gameSceneManager.playerRocket.transform.position.y + yPos,
                                                0f);

                thingToSpawn.transform.position = pos;
                Instantiate(thingToSpawn, obstacleContainer.transform, true);

                spawnedObstacleList.Add(thingToSpawn);
            }
        }
        firstSpawned = true;
    }
}

