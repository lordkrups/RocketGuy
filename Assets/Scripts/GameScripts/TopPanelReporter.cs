using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TopPanelReporter : MonoBehaviour
{
    public GameSceneManager gameSceneManager;

    public Text healthValue;
    public Text fuelValue;
    public Text boosterValue;
    public Text speedValue;
    public Text heightValue;
    public Text phaseValue;
    // Start is called before the first frame update
    void Start()
    {
        if (gameSceneManager == null)
        {
            gameSceneManager = this.GetComponentInParent<GameSceneManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthValue.text = gameSceneManager.playerRocket.health.ToString() + "HP";
        fuelValue.text = (((int)Mathf.Abs(gameSceneManager.playerRocket.fuel)).ToString() + "L");
        //healthValue.Value = rocketMotor.health.ToString() + "HP";
        speedValue.text = (Mathf.Abs(gameSceneManager.playerRocket.rb.velocity.y).ToString() + " KMH");
        heightValue.text = (((int)Mathf.Abs(gameSceneManager.playerRocket.height)).ToString() + " M");
        phaseValue.text = gameSceneManager.currentPhase;
    }
}
