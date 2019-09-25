using System.Collections;
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

        if (curHeight > (prevHeight + spawnOffset) || curHeight < (prevHeight - spawnOffset))
        {
            /*if (spawnedCloudsList.Count < numberOfGoodThingsToSpawn * 2)
            {
                SpawnCloudsInFrontOfPlayer();
            }
            if (spawnedCollectibleList.Count < numberOfGoodThingsToSpawn * 2)
            {
                SpawnCollectiblesInFrontOfPlayer();
            }
            if (spawnedObstacleList.Count < numberOfBadThingsToSpawn * 2)
            {
                SpawnObstaclesInFrontOfPlayer();
            } */
            SpawnCloudsInFrontOfPlayer();

            SpawnCollectiblesInFrontOfPlayer();

            SpawnObstaclesInFrontOfPlayer();
            prevHeight = curHeight;

        }

        if (curWidth > (prevWidth + spawnOffset) || curWidth < (prevWidth - spawnOffset))
        {
            SpawnCloudsInFrontOfPlayer();

            SpawnCollectiblesInFrontOfPlayer();

            SpawnObstaclesInFrontOfPlayer();
            /* if (spawnedCloudsList.Count < numberOfCloudsToSpawn * 2)
             {
                 SpawnCloudsInFrontOfPlayer();
             }
             if (spawnedCollectibleList.Count < numberOfGoodThingsToSpawn * 2)
             {
                 SpawnCollectiblesInFrontOfPlayer();
             }
             if (spawnedObstacleList.Count < numberOfBadThingsToSpawn * 2)
             {
                 SpawnObstaclesInFrontOfPlayer();
             }*/
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

    public void SpawnObstaclesInFrontOfPlayer()
    {
         //Debug.Log("SpawnObstaclesInFrontOfPlayer");

            for (int i = 0; i < numberOfBadThingsToSpawn; i++)
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
                    int ran = Random.Range(0, lowObstacleList.Count);
                    var thingToSpawn = Instantiate(lowObstacleList[ran], obstacleContainer.transform, true);

                    thingToSpawn.Init(this);

                if (i <= numberOfBadThingsToSpawn / 2)
                {
                    thingToSpawn.transform.position = pos;
                }
                else
                {
                    thingToSpawn.transform.position = -pos;
                }
                spawnedObstacleList.Add(thingToSpawn);
                }
                if (gameSceneManager.currentPhase == "Medium")
                {
                    int ran = Random.Range(0, medObstacleList.Count);
                    //var thingToSpawn = medObstacleList[ran];
                    var thingToSpawn = Instantiate(medObstacleList[ran], obstacleContainer.transform, true);
                    thingToSpawn.Init(this);

                if (i <= numberOfBadThingsToSpawn / 2)
                {
                    thingToSpawn.transform.position = pos;
                }
                else
                {
                    thingToSpawn.transform.position = -pos;
                }
                spawnedObstacleList.Add(thingToSpawn);
                }

            }
        firstSpawned = true;
    }
}

