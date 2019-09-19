using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantObj : MonoBehaviour
{
    public LevelParameters levelParameters;

    private void Start()
    {
        levelParameters = gameObject.GetComponentInParent<LevelParameters>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("MERCHANT THING");
            levelParameters.levelManager.ChooseWheelSkill();
            Destroy(gameObject, 1f);
        }
    }
}
