using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public WeaponStats weaponStats;
    public PlayerBrain parentShooter;

    public UISprite sprite;
    public float dissapearTime = 0.1f;
    public float range = 0.1f;
    public int damage;
    public bool sentDmg;

    public bool targetReached;
    public bool rangeReached;
    public bool returnedHome;
    public bool passThroughMob;
    public bool boomerangMode;

    public int weaponId;
    public string spriteName;           
    public float speed;            
    public string pattern;           
    public string add_pattern;          
    public bool wall_passage;

    public Vector3 loPos;
    public Vector3 woPos;
    public Vector3 startPos;
    public Vector2 _targetPos;
    public Vector2 _movement;
    public Rigidbody2D _rb2D;
    public float _angle;
    public Quaternion _targetRotation;

    public bool canMove;

    private Coroutine _currentCoroutine;
    public delegate void PatternAction(Vector3 pos, float range);
    public event PatternAction onPatternAction;



    public void Init(int weapon)
    {
        weaponStats = new WeaponStats();
        weaponStats.Init(weapon);
        //weaponId = weaponStats.WeaponId;
        spriteName = weaponStats.Sprite;
        speed = weaponStats.Speed;
        pattern = weaponStats.Pattern;
        add_pattern = weaponStats.Add_pattern;
        wall_passage = weaponStats.Wall_passage;

        sprite.spriteName = spriteName;

    }
    public void Setting(int dmg = 0, float atkRange = 0, PlayerBrain parShooter = null, PatternAction pa = null)
    {
        parentShooter = parShooter;
        _rb2D = GetComponent<Rigidbody2D>();
        damage = dmg;
        range = atkRange;
        onPatternAction = pa;
    }
    public void SetTarget(Vector2 targ)
    {
        _targetPos.x = targ.x;
        _targetPos.y = targ.y;

        if (!boomerangMode)
        {
            startPos = transform.position;
        }
        canMove = true;
        targetReached = false;

        _movement.Set(_targetPos.x, _targetPos.y);
        Vector2 rbv2 = new Vector2(_rb2D.transform.position.x, _rb2D.transform.position.y);
        Vector2 moveDirection = (_movement - rbv2).normalized;

        _movement = moveDirection * speed;

        _angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, _angle);

        PatternActions();

    }
    public void PatternActions()
    {
        //Debug.Log("CrossShot: ");

        if (pattern == "laser")
        {
            _currentCoroutine = StartCoroutine(FreeShot());
        }
        if (pattern == "cannon")
        {
            if (add_pattern == "none")
            {
                _currentCoroutine = StartCoroutine(RangeShot());
            }
            if (add_pattern == "crossed")
            {
                _currentCoroutine = StartCoroutine(CrossShot());
            }
        }
        if (pattern == "boomerang")
        {
            boomerangMode = true;
            _currentCoroutine = StartCoroutine(BoomerangShot());
        }
    }
    IEnumerator FreeShot()
    {
        yield return null;
    }
    IEnumerator RangeShot()
    {
        yield return new WaitUntil(() => rangeReached == true);
        Object.Destroy(gameObject);

        yield return null;
    }
    IEnumerator CrossShot()
    {
        passThroughMob = true;

        yield return new WaitUntil(() => targetReached == true);
        //passThroughMob = false;
        this.Off();
        //transform.rotation = Quaternion.Euler(0, 0, 0);

        onPatternAction?.Invoke(transform.position, range / 4);

        Object.Destroy(gameObject, dissapearTime);

        yield return null;

    }
    IEnumerator BoomerangShot()
    {

        yield return new WaitUntil(() => rangeReached == true);

        //SetTarget(parentShooter.transform.localPosition);

        yield return new WaitUntil(() => returnedHome == true);

        this.Off();
        Object.Destroy(gameObject, dissapearTime);

        yield return null;

    }
    public float GetAngle()
    {
        return _angle;
    }

    void FixedUpdate()
    {
        loPos = transform.localPosition;
        woPos = transform.position;
        if (canMove)
        {
            _rb2D.MovePosition(_rb2D.position + _movement * Time.deltaTime);

            if (Vector2.Distance(transform.position, _targetPos) < 0.1f)
            {
                targetReached = true;
            }
            if (Vector2.Distance(transform.position, startPos) >= range)
            {
                rangeReached = true;
            }
            if (boomerangMode && rangeReached)
            {
                SetTarget(parentShooter.transform.position);

                if (Vector2.Distance(transform.position, parentShooter.transform.position) < 0.1f)
                {
                    returnedHome = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            sentDmg = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Scenery"))
        {
            if (!boomerangMode)
            {
                canMove = false;
                this.Off();
                Object.Destroy(gameObject, dissapearTime);
            }
            if (boomerangMode)
            {
                //Debug.Log("boomerangMode rangeReached: " + rangeReached);
                rangeReached = true;
            }
        }
        if (collision.gameObject.CompareTag("Fence"))
        {
            if (!boomerangMode)
            {
                canMove = false;
                this.Off();
                Object.Destroy(gameObject, dissapearTime);
            }
            if (boomerangMode)
            {
                //Debug.Log("boomerangMode rangeReached: " + rangeReached);
                rangeReached = true;
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (boomerangMode)
            {
                canMove = false;
                this.Off();
                Object.Destroy(gameObject, dissapearTime);
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyMultiBrain>().levelManager.audioManager.PlaySFXClip("missileHit");

            if (!passThroughMob)
            {
                if (sentDmg == false)
                {
                    collision.gameObject.GetComponent<EnemyMultiBrain>().TakeDamage(damage);
                    collision.gameObject.GetComponent<EnemyMultiBrain>().TakeDamageLocation(transform.position);
                    sentDmg = true;

                    if (!boomerangMode)
                    {
                        canMove = false;
                        this.transform.SetParent(collision.transform);
                        Object.Destroy(gameObject, dissapearTime);
                    }
                }
            }
        }
    }
    //Could be used for a knockback effect on player or enemy
    //void OnCollisionEnter2D(Collision2D collision)
    //{

    //}
    
}
