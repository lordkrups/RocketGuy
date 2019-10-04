using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public StoreManager storeManager;
    public UIPanel MenuPanel;
    public UIPanel StorePanel;

    public List<PlaneMotor> thingsToSpawn;
    public CloudObj cloudObj;
    public bool spawnThings;
    public GameObject obstacleContainer;
    public int xDist, yDist;

    private void Awake()
    {
    }
    public void StartGame()
    {
        //SceneManager.LoadScene("GameScene");
        SceneManager.LoadScene("NGUIScene");
    }
    public void OpenStore()
    {
        MenuPanel.Off();
        StorePanel.On();
        storeManager.Init();
    }
    public void CloseStore()
    {
        MenuPanel.On();
        StorePanel.Off();
        RocketGameManager.Instance.SavePersistatStats();
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
            Vector3 pos = new Vector3(xPos, yDist, 0f);
            thingToSpawn.transform.position = pos;

            yield return new WaitForSeconds(1f);

            xPos = Random.Range(-xDist, xDist + 1);
            ran = Random.Range(10, 40);
            var cloud = Instantiate(cloudObj, obstacleContainer.transform, true);
            cloud.Init(null, true);
            pos = new Vector3(xPos, yDist, ran);
            cloud.transform.position = pos;
        }

        spawnThings = false;
    }
}
