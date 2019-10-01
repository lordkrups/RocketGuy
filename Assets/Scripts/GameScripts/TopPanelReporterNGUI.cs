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
    public UILabel timeValue;
    public UILabel boosterValue;
    public UISlider boosterFuelSlider;
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

        boosterValue.text = ((int)Mathf.Abs(gameSceneManager.playerRocket.boosterFuel)).ToString() + "/" + gameSceneManager.playerRocket.maxBoosterFuel.ToString();
        boosterFuelSlider.value = gameSceneManager.playerRocket.boosterFuel / gameSceneManager.playerRocket.maxBoosterFuel;

        timeValue.text = ((int)Mathf.Abs(gameSceneManager.playerRocket.flightTime)).ToString() + " s";
        speedValue.text = ((int)Mathf.Abs(gameSceneManager.playerRocket.rb.velocity.y)).ToString() + " m/s";
        heightValue.text = ((int)Mathf.Abs(gameSceneManager.playerRocket.height)).ToString() + " M";
        phaseValue.text = gameSceneManager.currentPhase;
    }
}
