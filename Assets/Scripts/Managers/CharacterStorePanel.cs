using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStorePanel : MonoBehaviour
{
    public List<CharacterStoreObj> characterStoreObjsList;

    private void Awake()
    {
        this.Off();
    }

    public void Init()
    {

        for (int i = 0; i < characterStoreObjsList.Count; i++)
        {
            //Debug.Log("characterStoreObjsList.Count.Count: " + characterStoreObjsList.Count);

            bool unlocked = false;
            if (EverythingLoader.Instance.gameManager.unlockedGodsList[i] == 1)
            {
                unlocked = true;
            }

            characterStoreObjsList[i].Init(i, unlocked);
        }
    }
}
