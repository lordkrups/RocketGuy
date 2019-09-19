using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public EnemyStats stats;
    public UISprite trapSprite;

    public int trapID;
    public int dmg;

    public void Awake()
    {
        StartCoroutine(LoadStats());

    }
    IEnumerator LoadStats()
    {
        stats = new EnemyStats();
        stats.Init(trapID);
        yield return new WaitUntil(() => stats.isLoaded == true);

        dmg = stats.Atk;

        trapSprite.spriteName = stats.Sprite;

        yield return new WaitForSeconds(0.2f);//Break so that everything can be loaded first

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBrain>().TakeDamage(dmg);
            collision.gameObject.GetComponent<PlayerBrain>().TakeDamageLocation(transform.position, 1);
        }
    }
}
