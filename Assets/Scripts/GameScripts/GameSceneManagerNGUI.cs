using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManagerNGUI : MonoBehaviour
{
    public VirtualController virtualController;
    public Camera uiCamera;

    public string currentPhase;

    public AudioPlayer audioPlayer;
    public RocketMotorNGUI playerRocket;
    public Transform floor;

    public bool isGamePaused;
    public UISprite blindSprite;
    public UIPanel pauseMenuPanel;
    public UIPanel gameOverPanel;
    public UILabel goHeightValue;
    public UILabel goFlightTimeValue;
    public UILabel goMaxSpeedValue;
    public UILabel goCoinsCollectedValue;
    public UILabel goDiamondsCollectedValue;
    public UILabel goObjectiveRewardValue;
    public UILabel goMoneyValue;

    public float lowHeight;
    public float medHeight;
    public float highHeight;
    public float spaceHeight;

    public bool flyPressed;
    public bool rotLeft;
    public bool rotRight;
    public bool rocketLeft;
    public bool rocketRight;
    public bool completedObjective;

    void Start()
    {
        virtualController.Init(uiCamera, Move, Stop);

        blindSprite.On();
        Time.timeScale = 1;
        pauseMenuPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        currentPhase = "Ground";

        if (RocketGameManager.Instance.numberOfPlays == RocketGameManager.Instance.playsSinceLastAd)
        {
            RocketGameManager.Instance.Display_Interstitial();
            RocketGameManager.Instance.playsSinceLastAd += 3;
        }
        else
        {
            if (!RocketGameManager.Instance.interstitialAd.IsLoaded())
            {
                RocketGameManager.Instance.RequestInterstitial();
            }
        }

        StartCoroutine(CloseBlind());
    }

    IEnumerator CloseBlind()
    {
        while (blindSprite.alpha > 0f)
        {
            blindSprite.alpha -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log(RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.persistantObjectiveCounter].objdescription);

    }
    void Update()
    {
        if (playerRocket.rb.position.y > 2.5f && playerRocket.rb.position.y < lowHeight)
        {
            currentPhase = "Low";
        }
        if (playerRocket.rb.position.y > lowHeight && playerRocket.rb.position.y < medHeight)
        {
            currentPhase = "Medium";
        }
        if (playerRocket.rb.position.y > medHeight && playerRocket.rb.position.y < spaceHeight)
        {
            currentPhase = "High";
        }
        if (playerRocket.rb.position.y > spaceHeight)
        {
            currentPhase = "Space";
        }
        if (!completedObjective)
        {
            CheckCurrentObjective();
        }
    }
    public void CheckCurrentObjective()
    {
        if (!completedObjective && RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.currentObjectiveCounter].objtype == "flight")
        {
            if (playerRocket.maxHeightReached >= RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.currentObjectiveCounter].objvalue)
            {
                Debug.Log("flight Objective Complete");
                completedObjective = true;
                if (RocketGameManager.Instance.currentObjectiveCounter == RocketGameManager.Instance.persistantObjectiveCounter)
                {
                    RocketGameManager.Instance.SaveEarnedCoins(RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.currentObjectiveCounter].objreward);
                    //RocketGameManager.Instance.ObjectiveComplete();
                }
            }
        }
        if (!completedObjective && RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.currentObjectiveCounter].objtype == "time")
        {
            if (playerRocket.flightTime >= RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.currentObjectiveCounter].objvalue)
            {
                Debug.Log("time Objective Complete");
                completedObjective = true;
                if (RocketGameManager.Instance.currentObjectiveCounter == RocketGameManager.Instance.persistantObjectiveCounter)
                {
                    RocketGameManager.Instance.SaveEarnedCoins(RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.currentObjectiveCounter].objreward);
                    RocketGameManager.Instance.ObjectiveComplete();
                }
            }
        }
        if (RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.currentObjectiveCounter].objtype == "coin")
        {
            if (!completedObjective && playerRocket.coinsCollected >= RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.currentObjectiveCounter].objvalue)
            {
                Debug.Log("Coins Objective Complete");
                completedObjective = true;
                if (RocketGameManager.Instance.currentObjectiveCounter == RocketGameManager.Instance.persistantObjectiveCounter)
                {
                    RocketGameManager.Instance.SaveEarnedCoins(RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.currentObjectiveCounter].objreward);
                    //RocketGameManager.Instance.ObjectiveComplete();
                }
            }
        }
        if (RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.currentObjectiveCounter].objtype == "diamond")
        {
            if (!completedObjective && playerRocket.diamondsCollected >= RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.currentObjectiveCounter].objvalue)
            {
                Debug.Log("Diamond Objective Complete");
                completedObjective = true;
                if (RocketGameManager.Instance.currentObjectiveCounter == RocketGameManager.Instance.persistantObjectiveCounter)
                {
                    RocketGameManager.Instance.SaveEarnedCoins(RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.currentObjectiveCounter].objreward);
                    //RocketGameManager.Instance.ObjectiveComplete();
                }
            }
        }
        if (!completedObjective && RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.currentObjectiveCounter].objtype == "speed")
        {
            if (playerRocket.maxSpeedReached >= RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.currentObjectiveCounter].objvalue)
            {
                Debug.Log("Speed Objective Complete");
                completedObjective = true;
                if (RocketGameManager.Instance.currentObjectiveCounter == RocketGameManager.Instance.persistantObjectiveCounter)
                {
                    RocketGameManager.Instance.SaveEarnedCoins(RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.currentObjectiveCounter].objreward);
                }
            }
        }
    }
    public void TogglePause()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0;
            pauseMenuPanel.gameObject.SetActive(true);
        }
        if (!isGamePaused)
        {
            Time.timeScale = 1;
            pauseMenuPanel.gameObject.SetActive(false);
        }
    }
    public void ShowGameOver()
    {
        int objMoney = 0;
        gameOverPanel.gameObject.SetActive(true);
        //goHeightValue.text = ((int)Mathf.Abs(playerRocket.height)).ToString() + "Meters";
        goHeightValue.text = (((int)playerRocket.maxHeightReached).ToString() + " M");
        goFlightTimeValue.text = (((int)playerRocket.flightTime).ToString() + " S");
        goMaxSpeedValue.text = (((int)playerRocket.maxSpeedReached).ToString() + " M/S");
        goCoinsCollectedValue.text = playerRocket.coinsCollected.ToString();
        goDiamondsCollectedValue.text = playerRocket.diamondsCollected.ToString();
        if (completedObjective)
        {
            if (RocketGameManager.Instance.currentObjectiveCounter == RocketGameManager.Instance.persistantObjectiveCounter)
            {
                RocketGameManager.Instance.ObjectiveComplete();
                goObjectiveRewardValue.text = RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.persistantObjectiveCounter - 1].objreward.ToString() + " G";
                objMoney = RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.persistantObjectiveCounter - 1].objreward;
            }
            else
            {
                goObjectiveRewardValue.text = "Already rewarded";
            }
        }
        else
        {
            goObjectiveRewardValue.text = "0 GP";
        }

        int totalMoney = playerRocket.moneyEarned + objMoney;
        goMoneyValue.text = totalMoney.ToString() + "G";

        RocketGameManager.Instance.SavePersistatStats();

    }
    public void ReturnToMenu()
    {
        RocketGameManager.Instance.SaveEarnedCoins(playerRocket.moneyEarned);

        SceneManager.LoadScene("MenuScene");
        Time.timeScale = 1;
    }
    public void ResetLevel()
    {
        SceneManager.LoadScene("NGUIScene");
    }

    public void FlyButtonPressed()
    {
        flyPressed = true;
    }
    public void FlyButtonReleased()
    {
        flyPressed = false;
    }
    public void LeftButtonPressed()
    {
        rotLeft = true;
        rocketLeft = true;

    }
    public void LeftButtonReleased()
    {
        rocketLeft = false;
        rotLeft = false;
    }
    public void RightButtonPressed()
    {
        rocketRight = true;
        rotRight = true;
    }
    public void RightButtonReleased()
    {
        rocketRight = false;
        rotRight = false;
    }

    public void PressScreenToControl()
    {
        //Debug.Log("LM PressToControl");
        if (virtualController.IsPlaying)
        {
            return;
            //virtualController.Stop();
        }
        virtualController.StartControlling();
    }
    private void Move(Vector2 vec)
    {
        //Debug.Log("Move");
        Debug.Log("vec.y " + vec.y);
        Debug.Log("vec.x " + vec.x);
        if (vec.y > 0)
        {
            flyPressed = true;
        }
        if (vec.y < 0)
        {
            flyPressed = false;
        }

        if (vec.x > 0.3f)
        {
            rocketRight = true;
            rotRight = true;
        }
        else
        {
            rocketRight = false;
            rotRight = false;
        }
        if (vec.x < -0.3f)
        {
            rocketLeft = true;
            rotLeft = true;
        }
        else
        {
            rocketLeft = false;
            rotLeft = false;
        }
    }
    private void Stop()
    {
        //Debug.Log("Stop");
        flyPressed = false;
        rocketLeft = false;
        rotLeft = false;
        rocketRight = false;
        rotRight = false;
    }
}

