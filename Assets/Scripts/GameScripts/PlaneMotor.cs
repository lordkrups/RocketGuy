using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMotor : MonoBehaviour
{
    public Rigidbody rb;

    public float despawnHeight = 3f;

    public float velocity = 1f;

    public bool flyReversed;
    public bool dirChange;

    public bool flyLeft;
    public bool flyRight;

    public bool flyUp;
    public bool flyDown;

    public bool timeToDie;


    private void OnCollisionEnter(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.None;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Destroy(this.gameObject, 10f);
        int randomInt = Random.Range(0, 51);

        if (randomInt < 24)
        {
            flyLeft = false;
            flyRight = true;
        }
        else
        {
            flyLeft = true;
            flyRight = false;
        }
               

        if (flyLeft && !flyReversed)
        {
            Quaternion deltaRotation = Quaternion.Euler(0, 270f, 0);
            //transform.rotation = deltaRotation;
            rb.rotation = deltaRotation;
        }
        if (flyRight && !flyReversed)
        {
            Quaternion deltaRotation = Quaternion.Euler(0, 90, 0);
            //transform.rotation = deltaRotation;
            rb.rotation = deltaRotation;
        }
        if (flyLeft && flyReversed)
        {
            Quaternion deltaRotation = Quaternion.Euler(0, 90f, 0);
            //transform.rotation = deltaRotation;
            rb.rotation = deltaRotation;
        }
        if (flyRight && flyReversed)
        {
            Quaternion deltaRotation = Quaternion.Euler(0, 270, 0);
            //transform.rotation = deltaRotation;
            rb.rotation = deltaRotation;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dirChange)
        {
            flyLeft = !flyLeft;
            flyRight = !flyRight;
            dirChange = false;
            Start();
        }

        if (flyReversed)
        {
            rb.MovePosition(transform.position - transform.forward * (velocity * Time.fixedDeltaTime));
        } else
        {
            rb.MovePosition(transform.position + transform.forward * (velocity * Time.fixedDeltaTime));
        }
        if (rb.transform.localPosition.y < despawnHeight)
        {
            Destroy(gameObject);
        }
        if (timeToDie == true)
        {
            Destroy(gameObject);
        }
    }

    public void DestroySoon()
    {
        Destroy(gameObject, 1f);
    }
    
}
