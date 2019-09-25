using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObj : MonoBehaviour
{
    public Rigidbody rb;
    public ThingsSpawnerNGUI thingsSpawnerNGUI;

    public bool fuel;
    public bool boosterFuel;
    public bool health;
    public bool money;

    public bool timeToDie;

    public int value;

    public void Init(ThingsSpawnerNGUI ts)
    {
        rb = GetComponent<Rigidbody>();
        thingsSpawnerNGUI = ts;
        timeToDie = false;
        Destroy(gameObject, 5f);
    }
    private void FixedUpdate()
    {
        //int distX = (int)Mathf.Abs(thingsSpawnerNGUI.gameSceneManager.playerRocket.rb.position.x - rb.transform.position.x);
        //int distY = (int)Mathf.Abs(thingsSpawnerNGUI.gameSceneManager.playerRocket.rb.position.y - rb.transform.position.y);

        //if (distY > thingsSpawnerNGUI.maxDespawnDistance || distX > thingsSpawnerNGUI.maxDespawnDistance)
        //{
            //SetToDie();
        //}

        if (rb.transform.localPosition.y < thingsSpawnerNGUI.despawnHeight)
        {
            SetToDie();
        }
        if (timeToDie == true)
        {
            DestroySoon();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.None;
        StartCoroutine(RemoveCollider());
    }
    public void SetToDie()
    {
        timeToDie = true;
    }
    public void DestroySoon()
    {
        thingsSpawnerNGUI.RemoveCollectible(this);
        Destroy(gameObject, 1f);
    }
    IEnumerator RemoveCollider()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<Collider>().enabled = false;
    }
}
