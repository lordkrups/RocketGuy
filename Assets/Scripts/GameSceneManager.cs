using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public RocketMotor playerRocket;

    public Image GameOverPanel;
    public Text goHeightValue;
    public Text goMoneyValue;

    private void Start()
    {
        GameOverPanel.gameObject.SetActive(false);
    }

    public void ShowGameOver()
    {
        GameOverPanel.gameObject.SetActive(true);
        //goHeightValue.text = ((int)Mathf.Abs(playerRocket.height)).ToString() + "Meters";
        //goMoneyValue.text = playerRocket.maxHeightReached.ToString() + "Meters";
        goHeightValue.text = (((int)playerRocket.maxHeightReached).ToString() + " M");

    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
