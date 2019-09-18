using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public string currentPhase;

    public RocketMotor playerRocket;

    public Image pauseMenuPanel;
    public Image gameOverPanel;
    public Text goHeightValue;
    public Text goMoneyValue;

    void Start()
    {
        Time.timeScale = 1;
        pauseMenuPanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        currentPhase = "Low";
    }

    void Update()
    {
        if (playerRocket.rb.position.y < 1000f)
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
