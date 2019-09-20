using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopPanelReporterNGUI : MonoBehaviour
{
    public GameSceneManagerNGUI gameSceneManager;

    public UILabel healthValue;
    public UISlider healthSlider;
    public UILabel fuelValue;
    public UISlider fuelSlider;
    public UILabel boosterValue;
    public UILabel speedValue;
    public UILabel heightValue;
    public UILabel phaseValue;
    // Start is called before the first frame update
    void Start()
    {
        if (gameSceneManager == null)
        {
            gameSceneManager = this.GetComponentInParent<GameSceneManagerNGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthValue.text = gameSceneManager.playerRocket.health.ToString() + "/" + gameSceneManager.playerRocket.maxHealth.ToString();
        healthSlider.value = gameSceneManager.playerRocket.health / gameSceneManager.playerRocket.maxHealth;
        fuelValue.text = ((int)Mathf.Abs(gameSceneManager.playerRocket.fuel)).ToString() + "/" + gameSceneManager.playerRocket.maxFuel.ToString();
        fuelSlider.value = gameSceneManager.playerRocket.fuel / gameSceneManager.playerRocket.maxFuel;

        speedValue.text = ((int)Mathf.Abs(gameSceneManager.playerRocket.rb.velocity.y)).ToString() + " m/s";
        heightValue.text = ((int)Mathf.Abs(gameSceneManager.playerRocket.height)).ToString() + " M";
        phaseValue.text = gameSceneManager.currentPhase;
    }
}
