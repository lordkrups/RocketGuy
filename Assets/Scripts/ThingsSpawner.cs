using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingsSpawner : MonoBehaviour
{
    public GameSceneManager gameSceneManager;
    public GameObject obstacleContainer;

    public float spawnOffset;
    public float curHeight;
    public float prevHeight;

    public int numberOfGoodThingsToSpawn;
    public int numberOfBadThingsToSpawn;
    public int xDist;
    public int yDist;

    public List<CollectibleObj> collectibleList;
    public List<PlaneMotor> obstacleList;

    public List<CollectibleObj> spawnedCollectibleList;
    public List<PlaneMotor> spawnedObstacleList;

    private void Update()
    {
        curHeight = gameSceneManager.playerRocket.rb.position.y;
        if (curHeight >= (prevHeight + spawnOffset))
        {
            prevHeight = curHeight;
            SpawnObstaclesInFrontOfPlayer();
            SpawnCollectiblesInFrontOfPlayer();
        }
        Debug.Log("curHeight: " + curHeight);
        Debug.Log("prevHeight + spawnOffset: " + (prevHeight + spawnOffset));

    }

    public void SpawnCollectiblesInFrontOfPlayer()
    {
        Debug.Log("SpawnCollectiblesInFrontOfPlayer");
        for (int i = 0; i < numberOfGoodThingsToSpawn; i++)
        {
            int ran = Random.Range(0, collectibleList.Count);
            var thingToSpawn = collectibleList[ran];

            int xPos = Random.Range(-xDist, xDist + 1);
            int yPos = Random.Range(yDist / 5, yDist + 1);
            Vector3 pos = new Vector3(gameSceneManager.playerRocket.transform.position.x - xPos,
                                        gameSceneManager.playerRocket.transform.position.y + yPos,
                                            0f);

            thingToSpawn.transform.position = pos;
            Instantiate(thingToSpawn, obstacleContainer.transform, true);

            spawnedCollectibleList.Add(thingToSpawn);
        }
    }

    public void SpawnObstaclesInFrontOfPlayer()
    {
        Debug.Log("SpawnObstaclesInFrontOfPlayer");
        for (int i = 0; i < numberOfBadThingsToSpawn; i++)
        {
            int ran = Random.Range(0, obstacleList.Count);
            var thingToSpawn = obstacleList[ran];

            int xPos = Random.Range(-xDist, xDist + 1);
            int yPos = Random.Range(yDist / 5, yDist + 1);
            Vector3 pos = new Vector3(gameSceneManager.playerRocket.transform.position.x - xPos,
                                        gameSceneManager.playerRocket.transform.position.y + yPos,
                                            0f);

            thingToSpawn.transform.position = pos;
            Instantiate(thingToSpawn,obstacleContainer.transform, true);

            spawnedObstacleList.Add(thingToSpawn);
        }
    }
}
