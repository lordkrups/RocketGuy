using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMotor : MonoBehaviour
{
    public Rigidbody rb;

    public Vector3 force;//variable for lift
    public Vector3 relativeTorque;//variable for torque

    public float maxForce;//determines how much force to apply
    public float liftSpeed;//determines how much force to apply
    public float turnSpeed;//determines how much torque to apply

	public float height;

	public float fuel;
	public float fuelConsumptionRate;

	public float health = 10f;
	public bool canTakeDmg;

	// Start is called before the first frame update
	void Start()
    {
        //relativeTorque = new Vector3(0, 0, 10);

    }

	// Update is called once per frame
	void FixedUpdate()
	{
		if (!canTakeDmg && transform.position.y > 10f)
		{
			canTakeDmg = true;
		}

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

        height = rb.position.y;
    }

	private void OnCollisionEnter(Collision collision)
	{
		if (canTakeDmg)
		{
			health = health - 1f;
		}
	}

    private void OnCollisionStay(Collision collision)
	{
		if (canTakeDmg)
		{
			health = health - 1f;
		}
	}
}
