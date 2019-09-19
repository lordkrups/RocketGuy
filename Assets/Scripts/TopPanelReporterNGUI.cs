using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopPanelReporterNGUI : MonoBehaviour
{
    public GameSceneManagerNGUI gameSceneManager;

    public UILabel healthValue;
    public UILabel fuelValue;
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
        healthValue.text = gameSceneManager.playerRocket.health.ToString() + "HP";
        fuelValue.text = ((int)Mathf.Abs(gameSceneManager.playerRocket.fuel)).ToString() + "L";
        //healthValue.Value = rocketMotor.health.ToString() + "HP";
        speedValue.text = ((int)Mathf.Abs(gameSceneManager.playerRocket.rb.velocity.y)).ToString() + " m/s";
        heightValue.text = ((int)Mathf.Abs(gameSceneManager.playerRocket.height)).ToString() + " M";
        phaseValue.text = gameSceneManager.currentPhase;
    }
}
