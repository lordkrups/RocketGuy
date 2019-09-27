using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public List<PlaneMotor> thingsToSpawn;
    public CloudObj cloudObj;
    public bool spawnThings;
    public GameObject obstacleContainer;
    public int xDist;

    public void StartGame()
    {
        //SceneManager.LoadScene("GameScene");
        SceneManager.LoadScene("NGUIScene");
    }

    private void Update()
    {
        if (!spawnThings)
        {
            StartCoroutine(SpawnThings());
            spawnThings = true;
        }
    }

    IEnumerator SpawnThings()
    {
        for (int i = 0; i < 10; i++)
        {
            int xPos;

            xPos = Random.Range(-xDist, xDist + 1);

            int ran = Random.Range(0, thingsToSpawn.Count);
            var thingToSpawn = Instantiate(thingsToSpawn[ran], obstacleContainer.transform, true);
            thingToSpawn.Init(null, true);
            Vector3 pos = new Vector3(xPos, 20f, 0f);
            thingToSpawn.transform.position = pos;

            yield return new WaitForSeconds(1f);
        }

        spawnThings = false;
    }
}
