using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPiece : MonoBehaviour
{
    public UISprite dropSprite;
    public UILabel dropLabel;

    public GameObject playerGO;

    public int value;
    public bool isGold;
    public bool isXp;
    public bool isHealth;

    public float _speed;
    public Rigidbody2D rb2D;                //The Rigidbody2D component attached to this object.
    public Vector2 _targetPos;
    public Vector2 _movement;
    public bool hasTarget = false;
    public bool canMove = false;

    public delegate void FinishAction(DropPiece dP);

    public event FinishAction finishAction;

    public void Init(int type, int v)
    {
        value = v;
        if (type == 0)
        {
            isGold = true;
            dropSprite.spriteName = "gold";
            dropLabel.Off();
            //Debug.Log("gold value : " + value);

        }
        if (type == 1)
        {
            isXp = true;
            dropSprite.Off();
            dropLabel.On();
            //Debug.Log("XP value : " + value);

        }
        if (type == 2)
        {
            isHealth = true;
            dropSprite.spriteName = "heart";
            dropLabel.Off();
            //Debug.Log("HP value : " + value);  

        }

    }
    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MoveToTarget();
            if (Vector2.Distance(transform.position, _targetPos) <= 0.2f)
            {
                this.Off();
                finishAction(this);
                Object.Destroy(this.gameObject, 0.1f);
            }

        }
    }

    public void AllowMove()
    {
        canMove = true;
    }


    public void SetTarget(Transform pGO, FinishAction finAct)
    {
        //playerGO.transform = pGO;

        _targetPos.x = pGO.transform.position.x;
        _targetPos.y = pGO.transform.position.y;

       // _targetPos.x = targ.x;
        //_targetPos.y = targ.y;

        finishAction = finAct;

        hasTarget = true;

        Object.Destroy(this.gameObject, 1f);
    }
    public void MoveTo()
    {
        _movement.Set(_targetPos.x, _targetPos.y);

        Vector2 rbv2 = new Vector2(rb2D.transform.position.x, rb2D.transform.position.y);
        _movement = (_movement - rbv2).normalized * _speed;
        transform.position = Vector2.Lerp(transform.position, _movement, 1f);
    }
    public void MoveToTarget()
    {
        //_targetPos.x = playerGO.transform.position.x;
        //_targetPos.y = playerGO.transform.position.y;

        _movement.Set(_targetPos.x, _targetPos.y);

        Vector2 rbv2 = new Vector2(rb2D.transform.position.x, rb2D.transform.position.y);
        _movement = (_movement - rbv2).normalized * _speed;
        rb2D.MovePosition(rb2D.position + _movement * Time.deltaTime);
    }

    public void Dissapear()
    {


    }
}
