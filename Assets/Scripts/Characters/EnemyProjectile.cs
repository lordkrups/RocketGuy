using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public WeaponStats weaponStats;
    public bool playerWeapon;

    public UISprite weaponSprite;
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
    public Vector3 _targetPos;
    public Vector2 _movement;
    public Rigidbody2D _rb2D;
    public float _angle;
    public Quaternion _targetRotation;

    public bool canMove;

    private Coroutine _currentCoroutine;
    public delegate void PatternAction(Vector3 pos, float range);
    public event PatternAction onPatternAction;

    public void Init(int wID)
    {
        weaponStats = new WeaponStats();
        weaponStats.Init(wID);

        weaponId = weaponStats.WeaponId;
        spriteName = weaponStats.Sprite;
        speed = weaponStats.Speed;
        pattern = weaponStats.Pattern;
        add_pattern = weaponStats.Add_pattern;
        wall_passage = weaponStats.Wall_passage;

        weaponSprite.spriteName = spriteName;
        Object.Destroy(gameObject, 10f);

    }
    public void Settings(int dmg = 0, float atkRange = 0, PatternAction pa = null)
    {
        damage = dmg;
        range = atkRange;
        onPatternAction = pa;
        _rb2D = GetComponent<Rigidbody2D>();
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
                passThroughMob = true;
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

        yield return new WaitUntil(() => targetReached == true);
        passThroughMob = false;
        this.Off();
        //transform.rotation = Quaternion.Euler(0, 0, 0);

        onPatternAction?.Invoke(transform.position, range/4);

        Object.Destroy(gameObject, dissapearTime);

        yield return null;

    }
    IEnumerator BoomerangShot()
    {

        yield return new WaitUntil(() => rangeReached == true);

        SetTarget(startPos);

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
            if (rangeReached )
            {
                if (Vector2.Distance(transform.position, startPos) < 0.1f)
                {
                    returnedHome = true;
                }
            }
            if (Vector2.Distance(transform.position, startPos) > 5f)
            {
                Object.Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Scenery"))
        {
            if (!wall_passage)
            {
                canMove = false;
                this.Off();
                Object.Destroy(gameObject, dissapearTime);
            }
        }
        if (collision.gameObject.CompareTag("Fence"))
        {

        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (!wall_passage)
            {
                canMove = false;
                this.Off();
                Object.Destroy(gameObject, dissapearTime);
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!passThroughMob)
            {
                if (sentDmg == false)
                {
                    if (!boomerangMode)
                    {
                        canMove = false;
                        sentDmg = true;
                        Object.Destroy(gameObject, dissapearTime);
                    }
                    collision.gameObject.GetComponent<PlayerBrain>().levelManager.audioManager.PlaySFXClip("missileHit");
                    collision.gameObject.GetComponent<PlayerBrain>().TakeDamage(damage);
                    collision.gameObject.GetComponent<PlayerBrain>().TakeDamageLocation(transform.position, 1);
                    
                }
            }
     
        }

    }
    //Could be used for a knockback effect on player or enemy
    //void OnCollisionEnter2D(Collision2D collision)
    //{

    //}
    
}
