using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public StoreManager storeManager;
    public UIPanel MenuPanel;
    public UIPanel StorePanel;
    public UIPanel OptionsPanel;
    public UIPanel flightSchoolPanel;
    public UIPanel resetBox;

    public UILabel bgmMusicLabel;
    public UILabel sfxMusicLabel;

    public UISprite addMoneyButton;

    public List<PlaneMotor> thingsToSpawn;
    public CloudObj cloudObj;
    public bool spawnThings;
    public GameObject obstacleContainer;
    public int xDist, yDist;

    private void Awake()
    {
        MenuPanel.On();
        StorePanel.Off();
        flightSchoolPanel.Off();
        OptionsPanel.Off();
        resetBox.Off();
        addMoneyButton.Off();

#if UNITY_EDITOR
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
        {
            Debug.Log("You're on a editor not a device!");
            Debug.Log("Use the 'Add Money' button in the bottom right if you want =] ");
            addMoneyButton.On();
        }
#endif

    }
    public void StartGame()
    {
        RocketGameManager.Instance.numberOfPlays++;
        RocketGameManager.Instance.currentObjectiveCounter = RocketGameManager.Instance.persistantObjectiveCounter;
        SceneManager.LoadScene("NGUIScene");
    }
    public void StartTrainingLevel()
    {
        RocketGameManager.Instance.numberOfPlays++;
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
    public void OpenSchool()
    {
        MenuPanel.Off();
        flightSchoolPanel.On();
    }
    public void CloseSchool()
    {
        MenuPanel.On();
        flightSchoolPanel.Off();
    }
    public void OpenOptions()
    {

        MenuPanel.Off();
        OptionsPanel.On();
        UpdateSoundDisplay();
    }
    public void UpdateSoundDisplay()
    {
        if (RocketGameManager.Instance.CheckBGMStatus())
        {
            bgmMusicLabel.text = "On";
        }
        else
        {
            bgmMusicLabel.text = "Off";
        }
        if (RocketGameManager.Instance.CheckSFXStatus())
        {
            sfxMusicLabel.text = "On";
        }
        else
        {
            sfxMusicLabel.text = "Off";
        }
    }
    public void ToggleBGM()
    {
        if (RocketGameManager.Instance.CheckBGMStatus())
        {
            RocketGameManager.Instance.persistantBGM = 0;
        }
        else
        {
            RocketGameManager.Instance.persistantBGM = 1;
        }
        RocketGameManager.Instance.SavePersistatStats();
        UpdateSoundDisplay();
    }
    public void ToggleSFX()
    {
        if (RocketGameManager.Instance.CheckSFXStatus())
        {
            RocketGameManager.Instance.persistantSFX = 0;
        }
        else
        {
            RocketGameManager.Instance.persistantSFX = 1;
        }
        RocketGameManager.Instance.SavePersistatStats();
        UpdateSoundDisplay();
    }
    public void CloseOptions()
    {
        MenuPanel.On();
        OptionsPanel.Off();
    }
    public void OpenResetBox()
    {
        resetBox.On();
    }
    public void CloseResetBox()
    {
        resetBox.Off();
    }
    public void ResetStats()
    {
        RocketGameManager.Instance.ResetAllStats();
        //SceneManager.LoadScene("MenuScene");
    }
    public void AddMoney()
    {
        RocketGameManager.Instance.SaveEarnedCoins(1000);
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
            var thingToSpawn = Instantiate(thingsToSpawn[ran], obstacleContainer.transform);
            thingToSpawn.Init(null, true);
            Vector3 pos = new Vector3(xPos, yDist, 0f);
            thingToSpawn.transform.position = pos;

            yield return new WaitForSeconds(1f);

            xPos = Random.Range(-xDist, xDist + 1);
            ran = Random.Range(10, 40);
            var cloud = Instantiate(cloudObj, obstacleContainer.transform);
            cloud.Init(null, true);
            pos = new Vector3(xPos, yDist, ran);
            cloud.transform.position = pos;
        }

        spawnThings = false;
    }

    public void StartFlightSchool()
    {
        RocketGameManager.Instance.isFlightSchool = true;
        SceneManager.LoadScene("NGUIScene");
    }
}
