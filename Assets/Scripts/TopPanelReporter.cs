using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TopPanelReporter : MonoBehaviour
{
    public RocketMotor rocketMotor;

    public Text healthText;
    public Text fuelText;
    public Text boosterText;
    public Text speedText;
    public Text heightText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = rocketMotor.health.ToString() + "HP";
        fuelText.text = (((int)Mathf.Abs(rocketMotor.fuel)).ToString() + "L");
        //healthText.text = rocketMotor.health.ToString() + "HP";
        speedText.text = (Mathf.Abs(rocketMotor.rb.velocity.y).ToString() + " KMH");
        heightText.text = (((int)Mathf.Abs(rocketMotor.height)).ToString() + " M");
    }
}
