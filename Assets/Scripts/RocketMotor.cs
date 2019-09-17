using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMotor : MonoBehaviour
{
    public GameSceneManager gameSceneManager;
    public Rigidbody rb;

    public Vector3 force;//variable for lift
    public Vector3 relativeTorque;//variable for torque

    public float maxForce;//determines how much force to apply
    public float liftSpeed;//determines how much force to apply
    public float turnSpeed;//determines how much torque to apply

    public float height;
    public float maxHeightReached;

    public float fuel;
	public float fuelConsumptionRate;

    public float health = 10f;
    public float dmgMultiplier = 1f;
    public bool isDead;
    public bool canTakeDmg;

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
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                relativeTorque.z = 0;
            }
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
                }
            }
        }
	}

    IEnumerator PlayerDied()
    {
        yield return new WaitForSeconds(1f);
        gameSceneManager.ShowGameOver();
    }
}
