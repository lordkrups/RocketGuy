using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMotorNGUI : MonoBehaviour
{
    public GameSceneManagerNGUI gameSceneManager;

    public GameObject launchPad;

    public Rigidbody rb;
    public Collider smallCollider;
    public Collider bigCollider;

    public Light lightOne;
    public List<ParticleSystem> flamesList;

    public Vector3 force;//variable for lift
    public Vector3 relativeTorque;//variable for torque


    public float height;
    public float maxHeightReached;
    public bool isRocketing;
    public bool isBoosting;
    public bool isFalling;
    public float boostTimer;
    public float fallTimer;

    public int moneyEarned;

    public float maxHealth;
    public float liftSpeed;//determines how much force to apply
    public float maxForce;//determines how much force to apply
    public float turnSpeed;//determines how much torque to apply
    public float dmgMultiplier;
    public float maxFuel;
    public float maxBoosterFuel;
    public float dragAirResis;
    public float fuelConsumptionRate;
    public float boosterFuelConsumptionRate;

    public float health;
    public float fuel;
    public float boosterFuel;

    public float flightTime;
    public bool gameStarted;
    public bool isDead;
    public bool canTakeDmg;

    private void Start()
    {
        maxHealth = RocketGameManager.Instance.StoreStatsInfos[0].itemStat[RocketGameManager.Instance.persistantMaxHealth];
        liftSpeed = RocketGameManager.Instance.StoreStatsInfos[1].itemStat[RocketGameManager.Instance.persistantEngineUpgrade];
        maxForce = RocketGameManager.Instance.StoreStatsInfos[2].itemStat[RocketGameManager.Instance.persistantBoosterEngineUpgrade];
        turnSpeed = RocketGameManager.Instance.StoreStatsInfos[3].itemStat[RocketGameManager.Instance.persistantTurnSpeed];
        dmgMultiplier = RocketGameManager.Instance.StoreStatsInfos[4].itemStat[RocketGameManager.Instance.persistantDamageMultiplier];
        maxFuel = RocketGameManager.Instance.StoreStatsInfos[5].itemStat[RocketGameManager.Instance.persistantMaxFuel];
        maxBoosterFuel = RocketGameManager.Instance.StoreStatsInfos[6].itemStat[RocketGameManager.Instance.persistantMaxBoosterFuel];
        dragAirResis = RocketGameManager.Instance.StoreStatsInfos[7].itemStat[RocketGameManager.Instance.persistantAirReistance];
        fuelConsumptionRate = RocketGameManager.Instance.StoreStatsInfos[9].itemStat[RocketGameManager.Instance.persistantFuelConsumptionRate];
        boosterFuelConsumptionRate = RocketGameManager.Instance.StoreStatsInfos[10].itemStat[RocketGameManager.Instance.persistantBoosterFuelConsumptionRate];


        for (int i = 0; i < flamesList.Count; i++)
        {
            flamesList[i].gameObject.SetActive(true);
        }
        // lightOne.gameObject.SetActive(false);
        health = maxHealth;
        fuel = maxFuel;
        boosterFuel = maxBoosterFuel;
        rb.drag = dragAirResis;

    }
    private void FireEngine()
    {
        if (force.y < maxForce)
        {
            force.y = force.y + liftSpeed;
        }

        if (boosterFuel > 0)
        {
            if (isBoosting)
            {
                force.y = force.y + (liftSpeed / 2);
                boosterFuel = boosterFuel - boosterFuelConsumptionRate;
                rb.AddRelativeForce(force);
                flamesList[3].Play();
            }
        }
        else
        {
            isBoosting = false;
        }

        if (fuel > 0)
        {
            fuel = fuel - fuelConsumptionRate;
            rb.AddRelativeForce(force);
            isRocketing = true;
            fallTimer = 0;
        } else
        {
            isRocketing = false;
        }


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameStarted)
        {
            flightTime += Time.deltaTime;
            if (launchPad != null)
            {
                Destroy(launchPad);
            }
        }

        if (!canTakeDmg && transform.position.y > 4f)
        {
            canTakeDmg = true;
            gameStarted = true;
        }
        if (gameSceneManager.floor.transform.position.x <= -2f || gameSceneManager.floor.transform.position.x >= 2f)
        {
            canTakeDmg = true;
            gameStarted = true;
        }

        if (!isDead)
        {
            if (Input.GetKey(KeyCode.Space) || gameSceneManager.flyPressed)
            {
                FireEngine();
            }
            if (fuel < 1 || !gameSceneManager.flyPressed)
            {
                force.y = 0f;
                isRocketing = false;
            }
            if (Input.GetKey(KeyCode.A) || gameSceneManager.rotLeft)
            {
                relativeTorque.z = relativeTorque.z + turnSpeed;
                Quaternion deltaRotation = Quaternion.Euler(relativeTorque * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }
            if (Input.GetKey(KeyCode.D) || gameSceneManager.rotRight)
            {
                relativeTorque.z = relativeTorque.z + turnSpeed;
                Quaternion deltaRotation = Quaternion.Euler(-relativeTorque * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                force.y = 0f;
                isRocketing = false;
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                relativeTorque.z = 0;
            }
        }
        if (isFalling)
        {
            rb.drag = rb.drag / 4;
        }
        else
        {
            rb.drag = dragAirResis;
        }

        if (isRocketing)
        {
            flamesList[0].Play();
            flamesList[1].Play();
            flamesList[2].Play();
            //lightOne.gameObject.SetActive(true);
            boostTimer += Time.deltaTime;
            isFalling = false;
        }
        if (fallTimer >= 3f)
        {
            isFalling = true;
        }
        if (boostTimer >= 3f)
        {
            isBoosting = true;
        }
        if (!isBoosting)
        {
            flamesList[3].Pause();
            flamesList[3].Clear();
        }
        if (!isRocketing)
        {
            fallTimer += Time.deltaTime;

            flamesList[0].Pause();
            flamesList[0].Clear();
            flamesList[1].Pause();
            flamesList[1].Clear();
            flamesList[2].Pause();
            flamesList[2].Clear();

            isBoosting = false;
            boostTimer = 0f;

            //tailLight.gameObject.SetActive(false);
        }

        height = rb.position.y;

        if (maxHeightReached <= height)
        {
            maxHeightReached = height;
        }

        if (isDead)
        {
            if (gameStarted)
            {
                StartCoroutine(PlayerDied());
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Collectible")
        {
            Physics.IgnoreCollision(smallCollider, collision.gameObject.GetComponent<Collider>());
            Physics.IgnoreCollision(bigCollider, collision.gameObject.GetComponent<Collider>());

            var colObj = collision.gameObject.GetComponent<CollectibleObj>();

            if (colObj.fuel)
            {
                fuel += colObj.value;
            }
            if (colObj.boosterFuel)
            {
                boosterFuel += colObj.value;
            }
            if (colObj.health)
            {
                health += colObj.value;
            }
            if (colObj.money)
            {
                moneyEarned += colObj.value;
            }

            Destroy(colObj);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            if (canTakeDmg && !isDead)
            {
                health = health - (1f * dmgMultiplier);

                if (health <= 0f)
                {
                    isDead = true;
                }
            }
        }
    }
    private void TakeDamage()
    {
        health = health - (1f * dmgMultiplier);

        if (health <= 0f)
        {
            isDead = true;
            canTakeDmg = false;
            health = 0f;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enviroment")
        {
            if (canTakeDmg && !isDead)
            {
                TakeDamage();
            }
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            if (canTakeDmg && !isDead)
            {

                Physics.IgnoreCollision(smallCollider, collision.gameObject.GetComponent<Collider>());
                Physics.IgnoreCollision(bigCollider, collision.gameObject.GetComponent<Collider>());

                TakeDamage();
            }
        }
    }

    IEnumerator PlayerDied()
    {
        gameStarted = false;
        yield return new WaitForSeconds(1f);
        gameSceneManager.ShowGameOver();
        rb.drag = rb.drag / 4;
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }
}