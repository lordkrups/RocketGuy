﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingsSpawnerNGUI : MonoBehaviour
{
    public GameSceneManagerNGUI gameSceneManager;
    public GameObject cloudsContainer;
    public GameObject collectibleContainer;
    public GameObject obstacleContainer;
    public float despawnHeight = 3f;
    public int maxDespawnDistance;
    public bool firstSpawned;
    public float spawnOffset;
    public float curHeight;
    public float curWidth;
    public float prevHeight;
    public float prevWidth;

    public int numberOfCloudsToSpawn;
    public int numberOfGoodThingsToSpawn;
    public int numberOfBadThingsToSpawn;
    public int xDist;
    public int yDist;

    public CloudObj cloudObj;

    public List<CollectibleObj> lowCollectibleList;
    public List<CollectibleObj> medCollectibleList;
    public List<CollectibleObj> highCollectibleList;

    public List<PlaneMotor> lowObstacleList;
    public List<PlaneMotor> medObstacleList;
    public List<PlaneMotor> highObstacleList;

    public List<CollectibleObj> spawnedCollectibleList;
    public List<PlaneMotor> spawnedObstacleList;
    public List<CloudObj> spawnedCloudsList;

    private void Start()
    {
        gameSceneManager = GetComponent<GameSceneManagerNGUI>();
    }
    private void Update()
    {
        curHeight = gameSceneManager.playerRocket.rb.position.y;
        curWidth = gameSceneManager.playerRocket.rb.position.x;

        if (curHeight > (prevHeight + spawnOffset))
        {
            //StartCoroutine(SpawnObstaclesInFrontOfPlayer("Up"));
            SpawnObstaclesInFrontOfPlayer("Up");
            prevHeight = curHeight;
        }
        if (curHeight < (prevHeight - spawnOffset))
        {
            //StartCoroutine(SpawnObstaclesInFrontOfPlayer("Down"));
            SpawnObstaclesInFrontOfPlayer("Down");
            prevHeight = curHeight;
        }
        if (curWidth < (prevWidth - spawnOffset))//Left
        {
            //StartCoroutine(SpawnObstaclesInFrontOfPlayer("Left"));
            SpawnObstaclesInFrontOfPlayer("Left");
            prevWidth = curWidth;
        }
        if (curWidth > (prevWidth + spawnOffset))
        {
            //StartCoroutine(SpawnObstaclesInFrontOfPlayer("Right"));
            SpawnObstaclesInFrontOfPlayer("Right");
            prevWidth = curWidth;
        }
    }

    public void RemoveCloud(CloudObj co)
    {
        spawnedCloudsList.Remove(co);
    }
    public void RemoveCollectible(CollectibleObj co)
    {
        spawnedCollectibleList.Remove(co);
    }
    public void RemoveObstacle(PlaneMotor pm)
    {
        spawnedObstacleList.Remove(pm);
    }

    public void DirectionalObsSpawner(string dirToSpawn)
    {
        Debug.Log("DirectionalObsSpawner");
        Debug.Log("dirToSpawn " + dirToSpawn);

        int xPos = 0;
        int yPos = 0;
        int ran;
        var thingToSpawn = new PlaneMotor();
        Vector3 pos = new Vector3(0,0,0);
        for (int i = 0; i < numberOfBadThingsToSpawn; i++)
        {

            if (dirToSpawn == "Up")
            {
                xPos = Random.Range(-xDist, xDist + 1);
                yPos = Random.Range(yDist / 2, yDist);
            }
            if (dirToSpawn == "Down")
            {
                xPos = Random.Range(-xDist, xDist + 1);
                yPos = Random.Range(-yDist, -yDist / 2);
            }

            if (dirToSpawn == "Left")
            {
                xPos = Random.Range(-xDist, -xDist / 2);
                yPos = Random.Range(yDist, yDist);
            }

            if (dirToSpawn == "Right")
            {
                xPos = Random.Range(xDist / 2, -xDist);
                yPos = Random.Range(yDist, yDist);
            }
            Debug.Log("for spawn ");


            pos = new Vector3(gameSceneManager.playerRocket.transform.position.x + xPos,
            gameSceneManager.playerRocket.transform.position.y + yPos, 0f);

            if (gameSceneManager.currentPhase == "Low")
            {
                ran = Random.Range(0, lowObstacleList.Count);
                thingToSpawn = Instantiate(lowObstacleList[ran], obstacleContainer.transform, true);

                thingToSpawn.Init(this);
            }
            if (gameSceneManager.currentPhase == "Medium")
            {
                ran = Random.Range(0, medObstacleList.Count);
                thingToSpawn = Instantiate(medObstacleList[ran], obstacleContainer.transform, true);

                thingToSpawn.Init(this);
            }
        }


        thingToSpawn.transform.position = pos;
        spawnedObstacleList.Add(thingToSpawn);
        firstSpawned = true;
    }

    public void SpawnCloudsInFrontOfPlayer()
    {
        //Debug.Log("SpawnCloudsInFrontOfPlayer");
        if (gameSceneManager.currentPhase == "Low" || gameSceneManager.currentPhase == "Medium")
        {
            for (int i = 0; i < numberOfCloudsToSpawn; i++)
            {
                var thingToSpawn = Instantiate(cloudObj, cloudsContainer.transform, true);
                thingToSpawn.Init(this);

                int rando = Random.Range(1, 10);

                int xPos;
                int yPos;

                if (rando <= 4)
                {
                    xPos = Random.Range(-xDist, xDist + 1);
                    yPos = Random.Range(yDist / 2, yDist + 1);
                }
                else
                {
                    xPos = Random.Range(-xDist, xDist + 1);
                    yPos = Random.Range(-yDist, -yDist / 2);
                }
                int zPos = Random.Range(15, 40);
                Vector3 pos = new Vector3(gameSceneManager.playerRocket.transform.position.x + xPos,
                                            gameSceneManager.playerRocket.transform.position.y + yPos,
                                                zPos);

                thingToSpawn.transform.position = pos;
                //Instantiate(thingToSpawn, collectibleContainer.transform, true);

                spawnedCloudsList.Add(thingToSpawn);
            }
        }
        firstSpawned = true;
    }

    public void SpawnCollectiblesInFrontOfPlayer()
    {
        //Debug.Log("SpawnCollectiblesInFrontOfPlayer");



        for (int i = 0; i < numberOfGoodThingsToSpawn; i++)
        {
            int rando = Random.Range(1, 10);

            int xPos;
            int yPos;

            if (rando <= 4)
            {
                xPos = Random.Range(-xDist, xDist + 1);
                yPos = Random.Range(yDist / 2, yDist + 1);
            }
            else
            {
                xPos = Random.Range(-xDist, xDist + 1);
                yPos = Random.Range(-yDist, -yDist / 2);
            }

            Vector3 pos = new Vector3(gameSceneManager.playerRocket.transform.position.x + xPos,
                                        gameSceneManager.playerRocket.transform.position.y + yPos,
                                            0f);

            if (gameSceneManager.currentPhase == "Low")
            {
                int ran = Random.Range(0, lowCollectibleList.Count);
                var thingToSpawn = Instantiate(lowCollectibleList[ran], collectibleContainer.transform, true);
                thingToSpawn.Init(this);
                if (i <= numberOfGoodThingsToSpawn/2)
                {
                    thingToSpawn.transform.position = pos;
                } else
                {
                    thingToSpawn.transform.position = -pos;
                }

                spawnedCollectibleList.Add(thingToSpawn);
            }
            if (gameSceneManager.currentPhase == "Medium")
            {
                int ran = Random.Range(0, medCollectibleList.Count);
                var thingToSpawn = Instantiate(medCollectibleList[ran], collectibleContainer.transform, true);
                thingToSpawn.Init(this);
                if (i <= numberOfGoodThingsToSpawn / 2)
                {
                    thingToSpawn.transform.position = pos;
                }
                else
                {
                    thingToSpawn.transform.position = -pos;
                }
                spawnedCollectibleList.Add(thingToSpawn);
            }




            
        }
        firstSpawned = true;
    }

    private void SpawnObstaclesInFrontOfPlayer(string dirToSpawn)
    {
        Debug.Log("dirToSpawn: " + dirToSpawn);
        //yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < numberOfBadThingsToSpawn; i++)
            {
                int xPos = 0;
                int yPos = 0;

                if (dirToSpawn == "Up")
                {
                    xPos = Random.Range(-xDist, xDist + 1);
                    yPos = Random.Range(yDist / 2, yDist);
                }
                if (dirToSpawn == "Down")
                {
                    xPos = Random.Range(-xDist, xDist + 1);
                    yPos = Random.Range(-yDist, -yDist / 2);
                }

                if (dirToSpawn == "Left")
                {
                    xPos = Random.Range(-xDist, -xDist / 2);
                    yPos = Random.Range(-yDist, yDist+1);
                }

                if (dirToSpawn == "Right")
                {
                    xPos = Random.Range(xDist / 2, xDist);
                    yPos = Random.Range(-yDist, yDist+1);
                }

                Vector3 pos = new Vector3(gameSceneManager.playerRocket.transform.position.x + xPos,
                                                    gameSceneManager.playerRocket.transform.position.y + yPos,
                                                        0f);
                if (gameSceneManager.currentPhase == "Low")
                {
                    int ran = Random.Range(0, lowObstacleList.Count);
                    var thingToSpawn = Instantiate(lowObstacleList[ran], obstacleContainer.transform, true);

                    thingToSpawn.Init(this);

                    thingToSpawn.transform.position = pos;

                    spawnedObstacleList.Add(thingToSpawn);
                }

                if (gameSceneManager.currentPhase == "Medium")
                {
                    int ran = Random.Range(0, medObstacleList.Count);
                    //var thingToSpawn = medObstacleList[ran];
                    var thingToSpawn = Instantiate(medObstacleList[ran], obstacleContainer.transform, true);
                    thingToSpawn.Init(this);

                    thingToSpawn.transform.position = pos;

                    spawnedObstacleList.Add(thingToSpawn);
                }
        }
        firstSpawned = true;
        //yield return new WaitForSeconds(0f);
    }
}

