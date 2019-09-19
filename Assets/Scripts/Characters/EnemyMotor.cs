using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotor : MonoBehaviour
{
    public EnemyMultiBrain brain;
    public Rigidbody2D rb2D;                //The Rigidbody2D component attached to this object.
    public CapsuleCollider2D enemyCollider;                //The Rigidbody2D component attached to this object.
    public Vector2 _targetPos;
    public Vector2 _tempTarget;
    public Vector2 _movement;
    public bool hasTarget;
    public bool canMove;
    public bool reAdjust;
    public bool sneaking;
    public bool stuck;
    public float stuckTime;
    public bool meleeAttack;
    public bool shooting;
    public bool axisMoved;
    public float _angle;
    //public Quaternion _targetRotation;
    public CapsuleCollider2D capCollider;

    // Start is called before the first frame update
    public void Init(EnemyMultiBrain emb, CapsuleCollider2D c)
    {
        brain = emb;
        capCollider = c;

        if (brain.levelManager.enemiesList.Count > 1)
        {
            for (int i = 0; i < brain.levelManager.enemiesList.Count; i++)
            {
                Physics2D.IgnoreCollision(capCollider, brain.levelManager.enemiesList[i].GetComponent<CapsuleCollider2D>());
            }
        }


        //StartCoroutine(StartMove(2f));
    }

    private void CheckBounds()
    {
        Vector2 resetPos = new Vector2(0, 0);
        if (_targetPos.x < -1.75f)
        {
            _targetPos.x = -1.6f;
        }
        if (_targetPos.x >  brain.levelManager.camStopRight.position.x - -0.1f)
        {
            _targetPos.x = brain.levelManager.camStopRight.position.x - 0.2f;
        }
        if (_targetPos.y < -0.7f)
        {
            _targetPos.y = -0.68f;
        }
        if (_targetPos.y > 0.7f)
        {
            _targetPos.y = 0.68f;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (brain.isAlive)
        {
            if (canMove)
            {
                if (stuckTime > 1f)
                {
                    stuckTime = 0f;
                    hasTarget = false;
                }
                if (hasTarget)
                {
                    CheckBounds();
                    MoveToTarget();
                    //if (Vector2.Distance(_tempTarget, transform.position) >= (brain.moveAmount* 2))
                    //{
                        //hasTarget = false;
                        //meleeAttack = false;
                    //}
                    if (Vector2.Distance(transform.position, _targetPos) <= 0.1f)
                    {
                        //Debug.Log("TARGET REACHED");
                        hasTarget = false;
                        meleeAttack = false;
                    }
                }
            }
        }
    }

    public void SetTarget(Vector3 targ, bool melee = false)
    {
        meleeAttack = melee;

        stuck = false;
        stuckTime = 0;

        canMove = true;
        hasTarget = true;

        _tempTarget = _targetPos;

        _targetPos= targ;

        MoveToTarget();
    }

    public void MoveToTarget()
    {
        //CalculateDirection();
        //Rotate();

        float moveSpeed = brain.moveSpeed;

        if (meleeAttack)
        {
            moveSpeed = brain.moveSpeed * 2f;
        }

        _movement.Set(_targetPos.x, _targetPos.y);

        Vector2 rbv2 = new Vector2(rb2D.transform.position.x, rb2D.transform.position.y);
        _movement = (_movement - rbv2).normalized * moveSpeed;
        rb2D.MovePosition(rb2D.position + _movement * Time.deltaTime);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            stuckTime += Time.deltaTime;

        }
        if (collision.gameObject.CompareTag("Scenery"))
        {
            //Debug.Log(" ENEMY scenery");

            //stuck = true;
            //hasTarget = false;
            stuckTime += Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            brain.levelManager.audioManager.PlaySFXClip("missileHit");
        }
        if (collision.gameObject.CompareTag("Scenery"))
        {
                stuck = true;
                hasTarget = false;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (!sneaking)
            {
                collision.gameObject.GetComponent<PlayerBrain>().TakeDamage(brain.atk);
                collision.gameObject.GetComponent<PlayerBrain>().TakeDamageLocation((transform.position + collision.transform.position) / 2, 2);
                brain.levelManager.audioManager.PlaySFXClip("crash");

            }
            if (meleeAttack)
            {
                //col.gameObject.GetComponent<PlayerBrain>().TakeDamage(brain.atk);
                //col.gameObject.GetComponent<PlayerBrain>().TakeDamageLocation((transform.position + col.transform.position) / 2, 2);
            }
        }
    }
    public void StartSneak()
    {
        sneaking = true;
        enemyCollider.enabled = false;
    }
    public void EndSneak()
    {
        sneaking = false;
        enemyCollider.enabled = true;

    }
    public void SetLook(float angle)
    {
        //_targetRotation = Quaternion.Euler(0, 0, angle);

        //rb2D.SetRotation(_targetRotation);
    }
}
