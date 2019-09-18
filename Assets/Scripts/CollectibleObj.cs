using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObj : MonoBehaviour
{
    public Rigidbody rb;

    public bool fuel;
    public bool boosterFuel;
    public bool health;
    public bool money;

    public int value;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
        private void OnCollisionEnter(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.None;
    }
}
