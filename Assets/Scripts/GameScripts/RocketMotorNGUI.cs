using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMotorNGUI : MonoBehaviour
{
    public GameSceneManagerNGUI gameSceneManager;
    public Rigidbody rb;
    public Collider smallCollider;
    public Collider bigCollider;

    public Light lightOne;
    public List<ParticleSystem> flamesList;

    public Vector3 force;//variable for lift
    public Vector3 relativeTorque;//variable for torque

    public float maxForce;//determines how much force to apply
    public float liftSpeed;//determines how much force to apply
    public float turnSpeed;//determines how much torque to apply

    public float height;
    public float maxHeightReached;
    public bool isRocketing;

    public int moneyEarned;

    public float maxFuel = 100f;
    public float fuel;
    public float fuelConsumptionRate;
    public float maxBoosterFuel = 100f;
    public float boosterFuel;
    public float boosterFuelConsumptionRate;

    public float maxHealth = 100f;
    public float health;
    public float dmgMultiplier = 1f;
    public bool isDead;
    public bool canTakeDmg;

    private void Start()
    {
        for (int i = 0; i < flamesList.Count; i++)
        {
            flamesList[i].gameObject.SetActive(true);
        }
       // lightOne.gameObject.SetActive(false);

        health = maxHealth;
        fuel = maxFuel;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!canTakeDmg && transform.position.y > 2.5f)
        {
            canTakeDmg = true;
        }
        if (!isDead)
        {
            if (Input.GetKey(KeyCode.Space) || gameSceneManager.flyPressed)
            {
                if (force.y < maxForce)
                {
                    force.y = force.y + liftSpeed;
                }
                if (fuel > 0)
                {
                    fuel = fuel - fuelConsumptionRate;
                    rb.AddRelativeForce(force);
                    isRocketing = true;
                }
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

        if (isRocketing)
        {
            flamesList[0].Play();
            flamesList[1].Play();
            flamesList[2].Play();
            //lightOne.gameObject.SetActive(true);
            flamesList[3].Play();

        }
        if (!isRocketing)
        {
            flamesList[0].Pause();
            flamesList[0].Clear();
            flamesList[1].Pause();
            flamesList[1].Clear();
            flamesList[2].Pause();
            flamesList[2].Clear();
            flamesList[3].Pause();
            flamesList[3].Clear();

            //tailLight.gameObject.SetActive(false);
        }
        height = rb.position.y;
        if (maxHeightReached <= height)
        {
            maxHeightReached = height;
        }

        if (isDead)
        {
            StartCoroutine(PlayerDied());
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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enviroment")
        {
            if (canTakeDmg && !isDead)
            {
                health = health - (10f * dmgMultiplier);

                if (health <= 0f)
                {
                    isDead = true;
                    health = 0f;
                }
            }
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            if (canTakeDmg && !isDead)
            {
                health = health - (1f * dmgMultiplier);

                Physics.IgnoreCollision(smallCollider, collision.gameObject.GetComponent<Collider>());
                Physics.IgnoreCollision(bigCollider, collision.gameObject.GetComponent<Collider>());

                if (health <= 0f)
                {
                    isDead = true;
                    health = 0f;
                }
            }
        }
    }

    IEnumerator PlayerDied()
    {
        yield return new WaitForSeconds(1f);
        gameSceneManager.ShowGameOver();
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;

    }
}