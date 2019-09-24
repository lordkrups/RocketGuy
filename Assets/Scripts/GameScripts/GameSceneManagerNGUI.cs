using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManagerNGUI : MonoBehaviour
{
    public string currentPhase;

    public RocketMotorNGUI playerRocket;

    public bool isGamePaused;
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
        if (playerRocket.rb.position.y > 2.5f && playerRocket.rb.position.y < 300f)
        {
            currentPhase = "Low";
        }
        if (playerRocket.rb.position.y > 300f && playerRocket.rb.position.y < 500f)
        {
            currentPhase = "Medium";
        }
        if (playerRocket.rb.position.y > 500f && playerRocket.rb.position.y < 3000f)
        {
            currentPhase = "High";
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
        SceneManager.LoadScene("MenuScene");
    }
}

