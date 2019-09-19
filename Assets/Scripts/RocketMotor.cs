using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMotor : MonoBehaviour
{
    public GameSceneManagerNGUI gameSceneManager;
    public Rigidbody rb;
    public Collider cl;

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

    public float fuel;
	public float fuelConsumptionRate;
    public float boosterFuel;
    public float boosterFuelConsumptionRate;

    public float health = 10f;
    public float dmgMultiplier = 1f;
    public bool isDead;
    public bool canTakeDmg;

    private void Start()
    {
        for (int i = 0; i < flamesList.Count; i++)
        {
            flamesList[i].gameObject.SetActive(true);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
	{
		if (!canTakeDmg && transform.position.y > 10f)
		{
			canTakeDmg = true;
		}
        if (!isDead)
        {
            if (Input.GetKey(KeyCode.Space))
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
                if (fuel < 1)
                {
                    isRocketing = false;
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                relativeTorque.z = relativeTorque.z + turnSpeed;
                Quaternion deltaRotation = Quaternion.Euler(relativeTorque * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }
            if (Input.GetKey(KeyCode.D))
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
        }
        if (!isRocketing)
        {
            flamesList[0].Pause();
            flamesList[0].Clear();
            flamesList[1].Pause();
            flamesList[1].Clear();
            flamesList[2].Pause();
            flamesList[2].Clear();
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
            Physics.IgnoreCollision(cl, collision.gameObject.GetComponent<Collider>());
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
        if (collision.gameObject.tag == "Obstacle")
        {
            if (canTakeDmg && !isDead)
            {
                health = health - (1f * dmgMultiplier);
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
