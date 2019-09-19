using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatStorePanel : MonoBehaviour
{
    public List<HatStoreObj> hatStoreObjsList;

    public void Init()
    {

        for (int i = 0; i < hatStoreObjsList.Count; i++)
        {
//            Debug.Log("hatStoreObjsList.Count: " + hatStoreObjsList.Count);

            bool unlocked = false;
            if (EverythingLoader.Instance.gameManager.unlockedHatsList[i] == 1)
            {
                unlocked = true;
            }

            hatStoreObjsList[i].Init(i, unlocked);
        }
    }

}
