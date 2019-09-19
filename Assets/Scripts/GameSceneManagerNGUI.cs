using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManagerNGUI : MonoBehaviour
{
    public string currentPhase;

    public RocketMotorNGUI playerRocket;

    public UIPanel pauseMenuPanel;
    public UIPanel gameOverPanel;
    public UILabel goHeightValue;
    public UILabel goMoneyValue;

    void Start()
    {
        Time.timeScale = 1;
        pauseMenuPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        currentPhase = "Ground";
    }

    void Update()
    {
        if (playerRocket.rb.position.y > 2.5f && playerRocket.rb.position.y < 1000f)
        {
            currentPhase = "Low";
        }
        if (playerRocket.rb.position.y > 1000f && playerRocket.rb.position.y < 2000f)
        {
            currentPhase = "Medium";
        }
        if (playerRocket.rb.position.y > 2000f && playerRocket.rb.position.y < 3000f)
        {
            currentPhase = "High";
        }
    }
    public void OpenPauseMenu()
    {
        Time.timeScale = 0;
        pauseMenuPanel.gameObject.SetActive(true);
    }
    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        pauseMenuPanel.gameObject.SetActive(false);
    }
    public void ShowGameOver()
    {
        gameOverPanel.gameObject.SetActive(true);
        //goHeightValue.text = ((int)Mathf.Abs(playerRocket.height)).ToString() + "Meters";
        //goMoneyValue.text = playerRocket.maxHeightReached.ToString() + "Meters";
        goHeightValue.text = (((int)playerRocket.maxHeightReached).ToString() + " M");

    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}

