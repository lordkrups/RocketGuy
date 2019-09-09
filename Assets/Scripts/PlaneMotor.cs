using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMotor : MonoBehaviour
{
    public Rigidbody rb;
    public float velocity = 1f;

    public bool flyLeft;
    public bool flyRight;

    public bool flyUp;
    public bool flyDown;


    private void OnCollisionEnter(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.None;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (flyLeft)
        {
            Quaternion deltaRotation = Quaternion.Euler(0, 270f, 0);
            rb.rotation = deltaRotation;
        }
        if (flyRight)
        {
            Quaternion deltaRotation = Quaternion.Euler(0, 90, 0);
            rb.rotation = deltaRotation;
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * (velocity * Time.fixedDeltaTime));
        if (rb.transform.position.y < 1f)
        {
            Destroy(gameObject);
        }
    }
    
}
