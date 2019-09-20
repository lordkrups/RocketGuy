using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObj : MonoBehaviour
{
    public Rigidbody rb;
    public float despawnHeight = 3f;

    public bool fuel;
    public bool boosterFuel;
    public bool health;
    public bool money;

    public bool timeToDie;

    public int value;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Destroy(this.gameObject, 10f);
    }
    private void Update()
    {
        if (rb.transform.localPosition.y < despawnHeight)
        {
            Destroy(gameObject);
        }
        if (timeToDie == true)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.None;
    }
    public void DestroySoon()
    {
        Destroy(gameObject, 1f);
    }
}
