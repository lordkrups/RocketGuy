using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesStore : MonoBehaviour
{
    public int persistantMaxHealth;
    public float liftSpeed;
    public int maxForce;
    public float turnSpeed;
    public float damageMultiplier;
    public int maxFuel;
    public int maxBoosterFuel;
    public float airReistance;
    public float fuelConsumptionRate;
    public float boosterFuelConsumptionRate;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void SavePlayerPurchases()
    {
        PlayerPrefs.SetInt("MaxHealth", persistantMaxHealth);
    }
}
