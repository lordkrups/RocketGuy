using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    //public UISprite dmgLabelSprite;
    public UILabel dmgLabel;

    // Start is called before the first frame update
    public void DamagePop(int damage)
    {
        StartCoroutine(ShowDamage(damage));
    }

    IEnumerator ShowDamage(int damage)
    {
        Debug.Log("ShowDamage: " + damage);
        if (damage > 0)
        {
            dmgLabel.text = "-" + damage.ToString();
            //dmgLabelSprite.On();
        }

        yield return new WaitForSeconds(0.3f);

        Debug.Log("ShowDamage 2");

        dmgLabel.text = " ";
        //dmgLabelSprite.Off();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
