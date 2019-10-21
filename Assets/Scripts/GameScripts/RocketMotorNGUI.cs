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
    public ParticleSystem mainFlame, leftFlame, rightFlame;
    public GameObject[] Prefabs;
    private GameObject deathExplosion;

    public Vector3 force;//variable for lift
    public Vector3 relativeTorque;//variable for torque


    public float height;
    public float maxHeightReached;
    public float maxSpeedReached;
    public bool isRocketing;
    public bool isBoosting;
    public bool isFalling;
    public float boostTimer;
    public float fallTimer;

    public int moneyEarned;
    public int coinsCollected;
    public int diamondsCollected;

    public float maxHealth;
    public float liftSpeed;//determines how much force to apply
    public float maxForce;//determines how much force to apply
    public float boosterLift;//determines how much force to apply
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
    public bool deathCalled;
    public bool isDead;
    public bool canTakeDmg;

    private void Start()
    {
        maxHealth = RocketGameManager.Instance.StoreStatsInfos[0].itemStat[RocketGameManager.Instance.persistantMaxHealth];
        maxForce = RocketGameManager.Instance.StoreStatsInfos[1].itemStat[RocketGameManager.Instance.persistantEngineUpgrade];
        boosterLift = RocketGameManager.Instance.StoreStatsInfos[2].itemStat[RocketGameManager.Instance.persistantBoosterEngineUpgrade];
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
            flamesList[i].Pause();
            flamesList[i].Clear();
        }
        mainFlame.gameObject.SetActive(true);
        leftFlame.gameObject.SetActive(true);
        rightFlame.gameObject.SetActive(true);
        mainFlame.Pause();
        mainFlame.Clear();
        leftFlame.Pause();
        leftFlame.Clear();
        rightFlame.Pause();
        rightFlame.Clear();
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

        //Debug.Log("isBoosting: " + isBoosting);
        //Debug.Log("force.y: " + force.y);
        //Debug.Log("maxForce + boosterLift: " + (maxForce + boosterLift));
        if (isBoosting)
        {
            boosterFuel = boosterFuel - boosterFuelConsumptionRate/2;
            flamesList[3].Play();
            mainFlame.Play();
            if (force.y <= (maxForce + boosterLift))
            {
                force.y = force.y + (liftSpeed / 2);
                rb.AddRelativeForce(force);
            }
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
    private void FireSideEngine()
    {
        if (gameSceneManager.rocketLeft)
        {
            if (force.x < maxForce)
            {
                force.x = force.x + 0.1f;
            }
        }
        if (gameSceneManager.rocketRight)
        {
            if (force.x < maxForce)
            {
                force.x = force.x - 0.1f;
            }
        }

        if (fuel > 0)
        {
            fuel = fuel - fuelConsumptionRate;
            rb.AddRelativeForce(force);
            isRocketing = true;
            fallTimer = 0;
        }
        else
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
            if (gameSceneManager.flyPressed)
            {
                FireEngine();
            }
            if (fuel < 1 || !gameSceneManager.flyPressed)
            {
                force.y = 0f;
                isRocketing = false;
            }

            if (gameSceneManager.rotLeft)
            {
                relativeTorque.z = relativeTorque.z + turnSpeed;
                Quaternion deltaRotation = Quaternion.Euler(relativeTorque * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }
            if (gameSceneManager.rotRight)
            {
                relativeTorque.z = relativeTorque.z + turnSpeed;
                Quaternion deltaRotation = Quaternion.Euler(-relativeTorque * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }
            if (!gameSceneManager.rotLeft && !gameSceneManager.rotRight)
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
        if (gameSceneManager.rocketLeft)
        {
            leftFlame.Play();
        }
        else
        {
            leftFlame.Pause();
            leftFlame.Clear();
        }

        if (gameSceneManager.rocketRight)
        {
            rightFlame.Play();
        }
        else
        {
            rightFlame.Pause();
            rightFlame.Clear();
        }
        if (isRocketing)
        {
            //gameSceneManager.audioPlayer.PlaySFXClip("rocketBlast");
            if (!gameSceneManager.rocketLeft && !gameSceneManager.rocketRight)
            {
                /*
                flamesList[0].Play();
                flamesList[1].Play();
                flamesList[2].Play();
                */
                mainFlame.Play();
                boostTimer += Time.deltaTime;
            }
            isFalling = false;
        }
        if (fallTimer >= 3f)
        {
            isFalling = true;
        }
        if (boostTimer >= 3f)
        {
            if (boosterFuel > 0)
            {
                isBoosting = true;
            } else
            {
                isBoosting = false;
            }
        }
        if (!isBoosting)
        {
            flamesList[3].Pause();
            flamesList[3].Clear();
        }
        if (!isRocketing)
        {
            fallTimer += Time.deltaTime;
            //gameSceneManager.audioPlayer.PauseRocketSound();
            /*
            flamesList[0].Pause();
            flamesList[0].Clear();
            flamesList[1].Pause();
            flamesList[1].Clear();
            flamesList[2].Pause();
            flamesList[2].Clear();
            */
            mainFlame.Pause();
            mainFlame.Clear();
            isBoosting = false;
            boostTimer = 0f;

            //tailLight.gameObject.SetActive(false);
        }

        height = rb.position.y;

        if (maxHeightReached <= height)
        {
            maxHeightReached = height;
        }
        if (maxSpeedReached <= rb.velocity.y)
        {
            maxSpeedReached = rb.velocity.y;
        }
        if (isDead && !deathCalled)
        {
            StartCoroutine(PlayerDied());
            deathCalled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Collectible")
        {
            gameSceneManager.audioPlayer.PlaySFXClip("getItem");

            Physics.IgnoreCollision(smallCollider, collision.gameObject.GetComponent<Collider>());
            Physics.IgnoreCollision(bigCollider, collision.gameObject.GetComponent<Collider>());

            var colObj = collision.gameObject.GetComponent<CollectibleObj>();

            if (colObj.fuel)
            {
                fuel += colObj.value;
                if (fuel > maxFuel)
                {
                    fuel = maxFuel;
                }
            }
            if (colObj.boosterFuel)
            {
                boosterFuel += colObj.value;
                if (boosterFuel > maxBoosterFuel)
                {
                    boosterFuel = maxBoosterFuel;
                }
            }
            if (colObj.health)
            {
                health += colObj.value;
                if (health > maxHealth)
                {
                    health = maxHealth;
                }
            }
            if (colObj.money)
            {
                coinsCollected++;
                moneyEarned += colObj.value * (int)RocketGameManager.Instance.nummyMultiplierStatValue[RocketGameManager.Instance.persistantNummyMultiplier];
            }
            if (colObj.diamond)
            {
                diamondsCollected++;
                moneyEarned += colObj.value * (int)RocketGameManager.Instance.diamonValueStatValue[RocketGameManager.Instance.persistantDiamonValue];
            }
            Destroy(colObj);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            gameSceneManager.audioPlayer.PlaySFXClip("crash");

            if (canTakeDmg && !isDead)
            {
                health = health - (1f * dmgMultiplier);

                if (health <= 0f)
                {
                    isDead = true;
                }
            }
            collision.gameObject.GetComponent<PlaneMotor>().timeToDie = true;
        }
        if (collision.gameObject.tag == "Enviroment")
        {
            gameSceneManager.audioPlayer.PlaySFXClip("crash");
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
        Debug.Log("PlayerDied");
        gameStarted = false;
        Vector3 trans = new Vector3(0, 0, -1f);
        trans = transform.position + trans;
        deathExplosion = Instantiate(Prefabs[0], trans, new Quaternion(), transform) as GameObject;
        //yield return new WaitForSeconds(0.4f);
        //trans = new Vector3(0, 0, -1f);
        //trans = transform.position + trans;
        //deathExplosion = Instantiate(Prefabs[1], trans, new Quaternion(), transform) as GameObject;

        yield return new WaitForSeconds(1.4f);
        rb.drag = rb.drag / 4;
        gameSceneManager.ShowGameOver();
        Time.timeScale = 0;
        //Time.timeScale = 0;
    }
}