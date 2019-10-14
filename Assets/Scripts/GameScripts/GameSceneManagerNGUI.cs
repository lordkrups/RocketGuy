using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManagerNGUI : MonoBehaviour
{
    public string currentPhase;

    public AudioPlayer audioPlayer;
    public RocketMotorNGUI playerRocket;
    public Transform floor;

    public bool isGamePaused;
    public UISprite blindSprite;
    public UIPanel pauseMenuPanel;
    public UIPanel gameOverPanel;
    public UILabel goHeightValue;
    public UILabel goMoneyValue;

    public float lowHeight;
    public float medHeight;
    public float highHeight;
    public float spaceHeight;

    public bool flyPressed;
    public bool rotLeft;
    public bool rotRight;
    public bool completedObjective;

    void Start()
    {
        blindSprite.On();
        Time.timeScale = 1;
        pauseMenuPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        currentPhase = "Ground";


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
        if (!completedObjective && RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.persistantObjectiveCounter].objtype == "flight")
        {
            if (playerRocket.maxHeightReached >= RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.persistantObjectiveCounter].objvalue)
            {
                Debug.Log("flight Objective Complete");
                completedObjective = true;
                RocketGameManager.Instance.ObjectiveComplete();
            }
        }
        if (!completedObjective && RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.persistantObjectiveCounter].objtype == "time")
        {
            if (playerRocket.flightTime >= RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.persistantObjectiveCounter].objvalue)
            {
                Debug.Log("time Objective Complete");
                completedObjective = true;
                RocketGameManager.Instance.ObjectiveComplete();
            }
        }
        if (RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.persistantObjectiveCounter].objtype == "coin")
        {
            if (!completedObjective && playerRocket.numberOfCollectedCoins >= RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.persistantObjectiveCounter].objvalue)
            {
                Debug.Log("Coins Objective Complete");
                completedObjective = true;
                RocketGameManager.Instance.ObjectiveComplete();
            }
        }
        if (!completedObjective && RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.persistantObjectiveCounter].objtype == "speed")
        {
            if (playerRocket.maxSpeedReached >= RocketGameManager.Instance.ObjectiveInfos[RocketGameManager.Instance.persistantObjectiveCounter].objvalue)
            {
                Debug.Log("Speed Objective Complete");
                completedObjective = true;
                RocketGameManager.Instance.ObjectiveComplete();
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
        gameOverPanel.gameObject.SetActive(true);
        //goHeightValue.text = ((int)Mathf.Abs(playerRocket.height)).ToString() + "Meters";
        goMoneyValue.text = playerRocket.moneyEarned.ToString() + "G";
        goHeightValue.text = (((int)playerRocket.maxHeightReached).ToString() + " M");

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
    }
    public void LeftButtonReleased()
    {
        rotLeft = false;
    }
    public void RightButtonPressed()
    {
        rotRight = true;
    }
    public void RightButtonReleased()
    {
        rotRight = false;
    }
}

