
using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
    //public Sprite dmgSprite;                    //Alternate sprite to display after Wall has been attacked by player.
    public int hp = 3;                            //hit points for the wall.


    public UISprite sprite;        //Store a component reference to the attached SpriteRenderer.
    public BoxCollider2D col;        //Store a component reference to the attached SpriteRenderer.


    void Awake()
    {
        //Get a component reference to the SpriteRenderer.
        //sprite = GetComponent<UISprite>();
    }


    //DamageWall is called when the player attacks a wall.
    public void DamageWall(int loss)
    {


        //Set spriteRenderer to the damaged wall sprite.
        //spriteRenderer.sprite = dmgSprite;

        //Subtract loss from hit point total.
        hp -= loss;

        //If hit points are less than or equal to zero:
        if (hp <= 0)
        {
            //Disable the gameObject.
            gameObject.SetActive(false);
            sprite.spriteName = "event_map_pan";
            this.Off();
            //col.gameObject.SetActive(false);
        }

    }
}