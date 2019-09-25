using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMotor : MonoBehaviour
{
    public Rigidbody rb;
    public ThingsSpawnerNGUI thingsSpawnerNGUI;

    public float velocity = 1f;

    public bool flyReversed;
    public bool dirChange;

    public bool flyLeft;
    public bool flyRight;

    public bool flyUp;
    public bool flyDown;

    public bool timeToDie;


    // Start is called before the first frame update
    public void Init(ThingsSpawnerNGUI ts)
    {
        rb = GetComponent<Rigidbody>();
        thingsSpawnerNGUI = ts;
        timeToDie = false;
        int randomInt = Random.Range(1, 52);
        velocity = randomInt / 5;

        //float ran = Random.Range(0.5, 10f);
        //Vector3 s = new Vector3(ran, ran, ran);
        //transform.localScale = s;

        if (randomInt < 25)
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

        int distX = (int)Mathf.Abs(thingsSpawnerNGUI.gameSceneManager.playerRocket.rb.position.x - rb.transform.position.x);
        int distY = (int)Mathf.Abs(thingsSpawnerNGUI.gameSceneManager.playerRocket.rb.position.y - rb.transform.position.y);

        if (distY > thingsSpawnerNGUI.maxDespawnDistance || distX > thingsSpawnerNGUI.maxDespawnDistance)
        {
            SetToDie();
        }
        if (flyReversed)
        {
            rb.MovePosition(transform.position - transform.forward * (velocity * Time.fixedDeltaTime));
        } else
        {
            rb.MovePosition(transform.position + transform.forward * (velocity * Time.fixedDeltaTime));
        }
        if (rb.transform.localPosition.y < thingsSpawnerNGUI.despawnHeight)
        {
            SetToDie();
        }
        if (timeToDie == true)
        {
            DestroySoon();
        }
    }
    public void SetToDie()
    {
        timeToDie = true;
    }
    public void DestroySoon()
    {
        thingsSpawnerNGUI.RemoveObstacle(this);
        Destroy(gameObject, 1f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.None;
        StartCoroutine(RemoveCollider());
    }
    IEnumerator RemoveCollider()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<Collider>().enabled = false;
    }
}
