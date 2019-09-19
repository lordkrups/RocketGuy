using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStorePanel : MonoBehaviour
{
    public GearMenuManager gearMenuManager;
    public WeaponStoreObj weaponStoreObjs;
    public UIGrid grid;
    public List<WeaponStoreObj> weaponStoreObjsList;

    private void Awake()
    {
        this.Off();
    }
    public void Init()
    {
        weaponStoreObjsList = new List<WeaponStoreObj>();
        Weapon[] keyArray = EverythingLoader.Instance.WeaponInfos.Values.ToArray();

        for (int i = 0; i < keyArray.Length; i++)
        {
            if (keyArray[i].show_store == 1)
            {
                var wSO = new WeaponStoreObj();
                wSO = weaponStoreObjs.MakeInstance(grid.gameObject);

                bool unlocked = false;
                if (EverythingLoader.Instance.gameManager.unlockedWeaponsList[i] == 1)
                {
                    unlocked = true;
                }
                weaponStoreObjsList.Add(wSO);
                weaponStoreObjsList[weaponStoreObjsList.Count - 1].Init(keyArray[i].id, i, unlocked, gearMenuManager);
            }
            //grid.maxPerLine = weaponStoreObjsList.Count / 2;
            //grid.Reposition();

        }
        grid.maxPerLine = weaponStoreObjsList.Count / 2;
        //grid.Reposition();

    }

    public void UpdatePurchase(int wID)
    {
        for (int i = 0; i < weaponStoreObjsList.Count; i++)
        {
            if (weaponStoreObjsList[i].weaponID == wID)
            {
                weaponStoreObjsList[i].Unlock();
            } 
        }
    }
}