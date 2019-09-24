using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudObj : MonoBehaviour
{
    public ThingsSpawnerNGUI thingsSpawnerNGUI;

    public List<GameObject> sphereList;

    public bool timeToDie;

    // Start is called before the first frame update
    public void Init(ThingsSpawnerNGUI ts)
    {
        thingsSpawnerNGUI = ts;

        for (int i = 0; i < sphereList.Count; i++)
        {
            float ran = Random.Range(0.5f, 1.6f);
            Vector3 s = new Vector3(ran, ran, ran);
            sphereList[i].transform.localScale = s;
        }
    }
    private void Update()
    {
        int distX = (int)Mathf.Abs(thingsSpawnerNGUI.gameSceneManager.playerRocket.rb.position.x - transform.position.x);
        int distY = (int)Mathf.Abs(thingsSpawnerNGUI.gameSceneManager.playerRocket.rb.position.y - transform.position.y);

        if (distY > thingsSpawnerNGUI.maxDespawnDistance || distX > thingsSpawnerNGUI.maxDespawnDistance)
        {
            SetToDie();
        }

        if (transform.localPosition.y < 0f)
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
        thingsSpawnerNGUI.RemoveCloud(this);
        Destroy(gameObject, 1f);
    }
}
