using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMultiBrain : MonoBehaviour
{
    public int enemyType;

    public LevelManager levelManager;
    public EnemyStats enemyStats;
    public UISlider healthSlider;

    public EnemyMotor motor;
    public EnemyShooter shooter;
    public EnemyChaser enemyChaser;
    public DamagePopup damagePopup;
    public UISprite enemySprite;
    public SpriteAnims enemySpriteAnim;
    public UIWidget enemyObj;
    public EnemyProjectile projectilePrefab;
    public Transform gunEnd;
    public CapsuleCollider2D enemyCollider;

    public Vector3 spawnPos;

    public DropContainer dropContainer;

    public bool stuffDropped;
    public bool DoANewThing;
    public bool isShooting;

    private Coroutine _currentCoroutine;

    public bool drawGizmo;
    public bool debug;
    public bool isAlive;
    public float dissapearTime;


    public string animalName;
    public int currentHp;
    public int moveType;
    public float moveAmount;
    public float moveSpeed;
    public float moveDelay;
    public int atk;
    public int noOfShots = 1;
    public int attackType;
    public int weapon;
    public float atkRange;
    public float atkSpeed;
    public float targetDelay;
    public int exp;
    public int reward;
    public int hpReward;
    public string sprite;
    public int size;
    public string moveAbility;
    public string shootAbility;

    void OnDrawGizmosSelected()
    {
        if (drawGizmo)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, atkRange);
        }
    }

    public void Init(LevelManager lm )
    {

        //enemyStats = GetComponent<EnemyStats>();
        //motor = GetComponent<EnemyMotor>();
        //enemyStats.Init(levelManager);

        levelManager = lm;

        shooter = new EnemyShooter();
        enemyStats = new EnemyStats();
        enemyStats.Init(enemyType);

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);

            _currentCoroutine = null;
        }

        _currentCoroutine = StartCoroutine(LoadStats());
        StartCoroutine(LoadStats());
        
    }
    IEnumerator LoadStats()
    {
        yield return new WaitUntil(() => enemyStats.isLoaded == true);

        animalName = enemyStats.AnimalName;
        currentHp = enemyStats.MaxHealth;
        moveType = enemyStats.MoveType;
        animalName = enemyStats.AnimalName;
        moveAmount = enemyStats.MoveAmount;
        moveSpeed = enemyStats.MoveSpeed;
        moveDelay = enemyStats.MoveDelay;
        atk = enemyStats.Atk;
        attackType = enemyStats.AttackType;
        weapon = enemyStats.Weapon;
        noOfShots = enemyStats.Shots;
        atkRange = enemyStats.AtackRange;
        atkSpeed = enemyStats.AtkSpeed;
        targetDelay = enemyStats.TargetDelay;
        exp = enemyStats.Exp;
        reward = enemyStats.Reward;
        hpReward = enemyStats.HpReward;
        sprite = enemyStats.Sprite;
        size = enemyStats.Size;
        moveAbility = enemyStats.MoveAbility;
        shootAbility = enemyStats.ShootAbility;

        enemySprite.spriteName = sprite;
        spawnPos = transform.position;
        healthSlider.value = currentHp / (float)enemyStats.MaxHealth;
        isAlive = true;
        yield return new WaitForSeconds(0.2f);//Break so that everything can be loaded first

        enemyChaser.Init();
        shooter.Init(weapon, this, motor, projectilePrefab, gunEnd);
        motor.Init(this, enemyCollider);
        yield return new WaitForSeconds(1.8f);
        DoANewThing = true;
    }

    private void KeepStraight()
    {
        healthSlider.transform.rotation = Quaternion.Euler(0, 0, 0);
        healthSlider.transform.localPosition = new Vector3(transform.position.x, transform.position.y + 57f, 0);
        enemySprite.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (!isShooting)
        {
            if (transform.position.x > motor._targetPos.x)
            {
                enemySprite.flip = UIBasicSprite.Flip.Horizontally;
            }
            else if (transform.position.x < motor._targetPos.x)
            {
                enemySprite.flip = UIBasicSprite.Flip.Nothing;
            }
        }
        if (isShooting)
        {
            if (transform.position.x > shooter.target.x)
            {
                enemySprite.flip = UIBasicSprite.Flip.Nothing;
            }
            else if (transform.position.x < shooter.target.x)
            {
                enemySprite.flip = UIBasicSprite.Flip.Horizontally;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {//Alive
            if (debug)
            {
                Debug.Log("pos: " + transform.position);

            }
            KeepStraight();

            if (DoANewThing)
            {
                ChooseNewAction();
            }
        }
    }

    private void FireGun()
    {
        levelManager.audioManager.PlaySFXClip("enemyShoot");
        shooter.FireGun(atk, atkRange);
    }

    public void ChooseNewAction()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);

            _currentCoroutine = null;
        }
        if (moveType == 0 && attackType == 0)
        {
            //For stationary enenmy?!
        }
        if (moveType == 0 && attackType == 1)//Stationary shoot
        {
            _currentCoroutine = StartCoroutine(StandAttack());
        }
        if (moveType == 1 && attackType == 0)//Random move and crash attack
        {
            _currentCoroutine = StartCoroutine(GridRandomCrash());
        }
        if (moveType == 1 && attackType == 1)//Random move and shoot
        {
            _currentCoroutine = StartCoroutine(RandomAttack());
        }
        if (moveType == 2 && attackType == 0)//Chase and crash
        {
            _currentCoroutine = StartCoroutine(GridRandomCrash());
            //_currentCoroutine = StartCoroutine(ChaseCrash());
        }
        if (moveType == 2 && attackType == 1)//Chase and shoot
        {
            //_currentCoroutine = StartCoroutine(ChaseCrash());
        }
        if (moveType == 3 && attackType == 0)//Vertical Move and Crash
        {
            _currentCoroutine = StartCoroutine(VerticalPatrolCrash());
        }
        if (moveType == 4 && attackType == 0)//Horizontal Move and Crash
        {
            _currentCoroutine = StartCoroutine(HorizontalPatrolCrash());
        }
    }

    IEnumerator StandAttack()// Stand still and shoot at the player
    {
        DoANewThing = false;
        motor.canMove = false;

        float ran = Random.Range(moveDelay / 10, moveDelay);

        yield return new WaitForSeconds(ran);

        for (int i = 0; i < noOfShots; i++)
        {
            shooter.TargetPlayer();
            if (Vector2.Distance(transform.position, levelManager.playerBrain.transform.position) <= atkRange)
            {
                enemySpriteAnim.FlashRed(4);
                yield return new WaitForSeconds(targetDelay);
                FireGun();
                yield return new WaitForSeconds(atkSpeed);
            }
        }

        yield return new WaitUntil(() => shooter.shooting == false);

        float x = moveDelay - ran;
        yield return new WaitForSeconds(x);

        DoANewThing = true;
        yield return null;
    }
    IEnumerator GridRandomCrash()// Randomly move and crash in to the player
    {
        DoANewThing = false;

        //RANDOM LENGTH
        //float ranx = transform.position.x + Random.Range(-moveAmount / 2, moveAmount / 2);
        //ranx = Mathf.Clamp(ranx, -1.9f, levelManager.camStopRight.transform.position.x);

        //float rany = transform.position.y + Random.Range(-moveAmount / 2, moveAmount / 2);
        //rany = Mathf.Clamp(rany, -0.9f, 0.9f);
        //Debug.Log("ranx: " + ranx);
        //Debug.Log("rany: " + rany);

        //Playerchase
        float ranx = levelManager.playerBrain.transform.position.x + Random.Range(-moveAmount / 2, moveAmount / 2);
        ranx = Mathf.Clamp(ranx, -1.9f, levelManager.camStopRight.transform.position.x);

        float rany = levelManager.playerBrain.transform.position.y + Random.Range(-moveAmount / 2, moveAmount / 2);
        rany = Mathf.Clamp(rany, -0.9f, 0.9f);
        Debug.Log("ranx: " + ranx);
        Debug.Log("rany: " + rany);

        Vector3 randomMovePos = new Vector3(ranx, rany, 0);

        enemyChaser.SetTarget(randomMovePos);

        yield return new WaitForSeconds(moveDelay);
        Debug.Log("1: ");

        enemyChaser.FindPath();
        Debug.Log("2: ");

        yield return new WaitForSeconds(1f);

        if (enemyChaser.stuck)
        {
            Debug.Log("stuck escape: ");

            int moveAm = Random.Range(1, 5);//1,2,3,4 are the random moves
            MiniMove(moveAm);

            yield return new WaitUntil(() => motor.hasTarget == false);

            DoANewThing = true;

            yield return null;
        }
        Debug.Log("5: ");

        //yield return new WaitUntil(() => enemyChaser.stuck == true);

        yield return new WaitUntil(() => enemyChaser.reachedTarget == true || enemyChaser.hasTarget == false);
        Debug.Log("6: ");

        int x = Random.Range(1, 3);

        if (Vector2.Distance(transform.position, levelManager.playerBrain.transform.position) <= atkRange)
        {
            Debug.Log("7: ");

            enemySpriteAnim.FlashRed(4);
            yield return new WaitForSeconds(targetDelay);

            GetAMove(0, true);//0 is go to the player, true to perform crash attack

            yield return new WaitUntil(() => motor.hasTarget == false);
            yield return new WaitForSeconds(atkSpeed);
        }
        Debug.Log("8: ");

        DoANewThing = true;

        yield return null;
    }
    IEnumerator RandomCrash()// Randomly move and crash in to the player
    {

        DoANewThing = false;

        //RANDOM LENGTH
        float ranx = transform.position.x + Random.Range(-moveAmount / 2, moveAmount / 2);
        ranx = Mathf.Clamp(ranx, -1.9f, levelManager.camStopRight.transform.position.x);

        float rany = transform.position.y + Random.Range(-moveAmount / 2, moveAmount / 2);
        rany = Mathf.Clamp(rany, -0.9f, 0.9f);
        Debug.Log("ranx: " + ranx);
        Debug.Log("rany: " + rany);

        Vector3 randomMovePos = new Vector3(ranx, rany, 0);

        enemyChaser.SetTarget(randomMovePos);

        yield return new WaitForSeconds(moveDelay);
        Debug.Log("1: ");

        enemyChaser.FindPath();
        Debug.Log("2: ");

        yield return new WaitForSeconds(1f);

        if (enemyChaser.stuck)
        {
            Debug.Log("stuck escape: ");

            int moveAm = Random.Range(1, 5);//1,2,3,4 are the random moves
            MiniMove(moveAm);

            yield return new WaitUntil(() => motor.hasTarget == false);

            DoANewThing = true;

            yield return null;
        }
        Debug.Log("5: ");

        //yield return new WaitUntil(() => enemyChaser.stuck == true);

        yield return new WaitUntil(() => enemyChaser.reachedTarget == true || enemyChaser.hasTarget == false);
        Debug.Log("6: ");

        int x = Random.Range(1, 3);

        if (Vector2.Distance(transform.position, levelManager.playerBrain.transform.position) <= atkRange)
        {
            Debug.Log("7: ");

            enemySpriteAnim.FlashRed(4);
            yield return new WaitForSeconds(targetDelay);

            GetAMove(0, true);//0 is go to the player, true to perform crash attack

            yield return new WaitUntil(() => motor.hasTarget == false);
            yield return new WaitForSeconds(atkSpeed);
        }
        Debug.Log("8: ");

        DoANewThing = true;

        yield return null;
        /* DoANewThing = false;
         float ran = Random.Range(moveDelay / 10, moveDelay);

         yield return new WaitForSeconds(ran);

         if (moveAbility == "sneak")
         {
             //Debug.Log("StartSneak: " );

             motor.StartSneak();
             while (enemyObj.alpha > 0)
             {
                 enemyObj.alpha -= 0.25f;
                 yield return new WaitForSeconds(0.1f);
             }

         }
         int moveAm = Random.Range(1, 5);//1,2,3,4 are the random moves
         GetAMove(moveAm);

         yield return new WaitUntil(() => motor.hasTarget == false);

         if (moveAbility == "sneak")
         {
             motor.EndSneak();
             while (enemyObj.alpha < 1)
             {
                 enemyObj.alpha += 0.25f;
                 yield return new WaitForSeconds(0.1f);
             }

         }

         float x = moveDelay - ran;
         yield return new WaitForSeconds(x);

         if (Vector2.Distance(transform.position, levelManager.playerBrain.transform.position) <= atkRange)
         {
             //Debug.Log("PLAYER IN RANGE");
             enemySpriteAnim.FlashRed(4);
             yield return new WaitForSeconds(targetDelay);

             GetAMove(0, true);//0 is go to the player, true to perform crash attack

             yield return new WaitUntil(() => motor.hasTarget == false);
             yield return new WaitForSeconds(atkSpeed);

         }

         DoANewThing = true;
         yield return null; */
    }
    IEnumerator RandomAttack()// Randomly move and crash in to the player
    {
        DoANewThing = false;

        float ran = Random.Range(moveDelay / 10, moveDelay);

        yield return new WaitForSeconds(ran);

        if (moveAbility == "fly")
        {
            //Debug.Log("LocalScale: " + transform.localScale);
            motor.StartSneak();
            while (transform.localScale.x < 1.5f)
            {
                transform.localScale += new Vector3(0.25f, 0.25f, 0);
                enemyObj.alpha -= 0.25f;
                yield return new WaitForSeconds(0.1f);
            }
        }

        int moveAm = Random.Range(1, 5);//1,2,3,4 are the random moves
        GetAMove(moveAm);

        yield return new WaitUntil(() => motor.hasTarget == false);

        if (moveAbility == "fly")
        {
            motor.EndSneak();
            while (transform.localScale.x > 1f)
            {
                transform.localScale -= new Vector3(0.25f, 0.25f, 0);
                enemyObj.alpha += 0.25f;
                yield return new WaitForSeconds(0.1f);
            }
        }
        float x = moveDelay - ran;
        yield return new WaitForSeconds(x);

        for (int i = 0; i < noOfShots; i++)
        {
            shooter.TargetPlayer();
            yield return new WaitForSeconds(targetDelay);
            if (Vector2.Distance(transform.position, levelManager.playerBrain.transform.position) <= atkRange)
            {
                enemySpriteAnim.FlashRed(4);
                yield return new WaitForSeconds(targetDelay);
                FireGun();
                yield return new WaitForSeconds(atkSpeed);
            }
        }

        yield return new WaitUntil(() => shooter.shooting == false);

        DoANewThing = true;
        yield return null;
    }
    IEnumerator ChaseCrash()// 
    {
        DoANewThing = false;
        enemyChaser.stuck = false;

        float t = Random.Range(-moveDelay, moveDelay);
        //yield return new WaitForSeconds(moveDelay + t);
        Debug.Log("1: ");

        if (enemyChaser.player == null)
        {
            Debug.Log("2: ");
            enemyChaser.SetTarget(levelManager.playerBrain.transform.position);
        }

        yield return new WaitForSeconds(moveDelay);
        Debug.Log("3: ");

        enemyChaser.FindPath();
        Debug.Log("4: ");




        yield return new WaitForSeconds(1f);

        if (enemyChaser.stuck)
        {
            Debug.Log("stuck escape: ");

            int moveAm = Random.Range(1, 5);//1,2,3,4 are the random moves
            MiniMove(moveAm);

            yield return new WaitUntil(() => motor.hasTarget == false);

            DoANewThing = true;

            yield return null;
        } 
        Debug.Log("5: ");

        //yield return new WaitUntil(() => enemyChaser.stuck == true);

        yield return new WaitUntil(() => enemyChaser.reachedTarget == true || enemyChaser.hasTarget == false);
        Debug.Log("6: ");

        int x = Random.Range(1,3);

        if (Vector2.Distance(transform.position, levelManager.playerBrain.transform.position) <= atkRange)
        {
            Debug.Log("7: ");

            enemySpriteAnim.FlashRed(4);
            yield return new WaitForSeconds(targetDelay);

            GetAMove(0, true);//0 is go to the player, true to perform crash attack

            yield return new WaitUntil(() => motor.hasTarget == false);
            yield return new WaitForSeconds(atkSpeed);
        }
        Debug.Log("8: ");

        DoANewThing = true;

        yield return null;
    }
    public void Unstick(int action)
    {
        Vector2 ran = new Vector2();

        if (action == 1)
        {
            float moveAm = Random.Range(-moveAmount, moveAmount);
            //VERTICAL RANDOM LENGTH
            ran.x = transform.position.x + moveAm / 4;
            if (motor._tempTarget.y > motor._targetPos.y)
            {
                ran.y = transform.position.y - 0.1f;
            }
            if (motor._tempTarget.y < motor._targetPos.y)
            {
                ran.y = transform.position.y + 0.1f;
            }

            //ran.y = transform.position.y + moveAm/4;

            motor.SetTarget(ran);
        }

        if (action == 2)
        {
            float moveAm = Random.Range(-moveAmount, moveAmount);
            //HORIZONTAL RANDOM LENGTH
            //ran.x = transform.position.x + moveAm/4;
            if (motor._tempTarget.x > motor._targetPos.x)
            {
                ran.x = transform.position.x - 0.1f;
            }
            if (motor._tempTarget.x < motor._targetPos.y)
            {
                ran.x = transform.position.x + 0.1f;
            }

            ran.y = transform.position.y + moveAm / 4;

            motor.SetTarget(ran);
        }
    }

    IEnumerator VerticalPatrolCrash()// Randomly move and crash in to the player
    {
        DoANewThing = false;

        yield return new WaitForSeconds(moveDelay);

        //motor.SetTarget(ran);
        GetAMove(5);

        yield return new WaitUntil(() => motor.hasTarget == false);

        if (Vector2.Distance(transform.position, levelManager.playerBrain.transform.position) <= atkRange)
        {
            enemySpriteAnim.FlashRed(4);
            yield return new WaitForSeconds(targetDelay);

            GetAMove(0, true);//0 is go to the player, true to perform crash attack

            yield return new WaitUntil(() => motor.hasTarget == false);
            yield return new WaitForSeconds(atkSpeed);
        }

        DoANewThing = true;

        yield return null;
    }
    IEnumerator HorizontalPatrolCrash()// Randomly move and crash in to the player
    {
        DoANewThing = false;

        yield return new WaitForSeconds(moveDelay);

        //motor.SetTarget(ran);
        GetAMove(6);

        yield return new WaitUntil(() => motor.hasTarget == false);

        if (Vector2.Distance(transform.position, levelManager.playerBrain.transform.position) <= atkRange)
        {
            enemySpriteAnim.FlashRed(4);
            yield return new WaitForSeconds(targetDelay);

            GetAMove(0, true);//0 is go to the player, true to perform crash attack

            yield return new WaitUntil(() => motor.hasTarget == false);
            yield return new WaitForSeconds(atkSpeed);
        }

        DoANewThing = true;

        yield return null;
    }
    public bool IsSneekingCheck()
    {
        return motor.sneaking;
    }
    public void AxisMove(int action)
    {
        Vector2 ran = new Vector2();

        if (action == 1)
        {
            ran.y = transform.position.y - moveAmount;
            ran.x = transform.position.x;

            motor.SetTarget(ran);
        }
        if (action == 2)
        {
            ran.y = transform.position.y + moveAmount;
            ran.x = transform.position.x;

            motor.SetTarget(ran);
        }

        if (action == 3)
        {
            ran.x = transform.position.x - moveAmount;
            ran.y = transform.position.y;

            motor.SetTarget(ran);
        }
        if (action == 4)
        {
            ran.x = transform.position.x + moveAmount;
            ran.y = transform.position.y;

            motor.SetTarget(ran);
        }
    }
    public void MiniMove(int action)
    {
        Vector2 ran = new Vector2();

        if (action == 1)
        {
            float moveAmX = Random.Range(0.1f, 0.3f);
            float moveAmY = Random.Range(0.1f, 0.3f);
            //RANDOM LENGTH
            ran.x = transform.position.x - moveAmX;
            ran.y = transform.position.y - moveAmY;
            motor.SetTarget(ran);
        }

        if (action == 2)
        {
            float moveAmX = Random.Range(0.1f, 0.3f);
            float moveAmY = Random.Range(0.1f, 0.3f);
            //RANDOM LENGTH
            ran.x = transform.position.x + moveAmX;
            ran.y = transform.position.y + moveAmY;

            motor.SetTarget(ran);
        }

        if (action == 3)
        {
            float moveAmX = Random.Range(0.1f, 0.3f);
            float moveAmY = Random.Range(0.1f, 0.3f);
            //RANDOM LENGTH
            ran.x = transform.position.x + moveAmX;
            ran.y = transform.position.y - moveAmY;

            motor.SetTarget(ran);
        }

        if (action == 4)
        {
            float moveAmX = Random.Range(0.1f, 0.3f);
            float moveAmY = Random.Range(0.1f, 0.3f);
            //RANDOM LENGTH
            ran.x = transform.position.x - moveAmX;
            ran.y = transform.position.y + moveAmY;

            motor.SetTarget(ran);
        }
    }
    public void GetAMove(int action, bool crashAtk = false)
    {
        Vector2 ran = new Vector2();

        if (action == 0 && crashAtk == true)
        {
            //Debug.Log("CRASH ATTACK");

            ran.x = levelManager.playerBrain.transform.position.x;
            ran.y = levelManager.playerBrain.transform.position.y;
            motor.SetTarget(ran, true);

        }

        if (action == 1)
        {
            float moveAmX = Random.Range(-moveAmount, moveAmount);
            float moveAmY = Random.Range(-moveAmount, moveAmount);
            //RANDOM LENGTH
            ran.x = transform.position.x - moveAmX;
            ran.y = transform.position.y - moveAmY;
            motor.SetTarget(ran);
        }

        if (action == 2)
        {
            float moveAmX = Random.Range(-moveAmount, moveAmount);
            float moveAmY = Random.Range(-moveAmount, moveAmount);
            //RANDOM LENGTH
            ran.x = transform.position.x + moveAmX;
            ran.y = transform.position.y + moveAmY;

            motor.SetTarget(ran);
        }

        if (action == 3)
        {
            float moveAmX = Random.Range(-moveAmount, moveAmount);
            float moveAmY = Random.Range(-moveAmount, moveAmount);
            //RANDOM LENGTH
            ran.x = transform.position.x + moveAmX;
            ran.y = transform.position.y - moveAmY;

            motor.SetTarget(ran);
        }

        if (action == 4)
        {
            float moveAmX = Random.Range(-moveAmount, moveAmount);
            float moveAmY = Random.Range(-moveAmount, moveAmount);
            //RANDOM LENGTH
            ran.x = transform.position.x - moveAmX;
            ran.y = transform.position.y + moveAmY;

            motor.SetTarget(ran);
        }

        if (action == 5)//VERTICAL CHASE
        {
            float moveAm = Random.Range(-moveAmount, moveAmount);
            //VERTICAL RANDOM LENGTH
            ran.x = transform.position.x;
            ran.y = levelManager.playerBrain.transform.position.y;
            motor.SetTarget(ran);
        }
        if (action == 6)//HORIZONTAL CHASE
        {
            float moveAm = Random.Range(-moveAmount, moveAmount);
            //HORIZONTAL RANDOM LENGTH
            ran.x = levelManager.playerBrain.transform.position.x;
            ran.y = transform.position.y;
            motor.SetTarget(ran);
        }

    }

    public void TakeDamageLocation(Vector3 hitPos)
    {
        enemySpriteAnim.FlashRed(1, hitPos.x, hitPos.y);

    }
    public void TakeDamage(int dmg)
    {
        Debug.Log("dmg: " +  dmg);

        currentHp = currentHp - dmg;

        if (dmg > 0)
        {
            //enemySprite.GetComponent<SpriteAnims>().ShowDamgeAmount(dmg);
            enemySprite.GetComponent<SpriteAnims>().FlashRed(0,0,0);
            damagePopup.DamagePop(dmg);
        }

        healthSlider.value = currentHp / (float)enemyStats.MaxHealth;

        if (currentHp <= 0)
        {
            isAlive = false;
            DropStuff();
            ShowDeath();

            levelManager.RemoveEnemy(this);
            //DoDeathAction(this.transform);
        }
    }
    private void ShowDeath()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        enemySprite.spriteName = enemySprite.spriteName + "_dead";

        Object.Destroy(gameObject, dissapearTime);

    }
    private void DropStuff()
    {
        if (!stuffDropped)
        {
            if (reward > 0)
            {
                var drop = dropContainer.MakeInstance(levelManager.GamePanel.gameObject);
                levelManager.dropContainerList.Add(drop);
                drop.transform.position = transform.position;
                drop.Init(0,(int)(reward/5f), 5);
                for (int i = 0; i < drop.dropPieces.Count; i++)
                {
                    levelManager.dropPiecesList.Add(drop.GetDropPieces(i));

                }
            }
            if (exp > 0)
            {
                var drop = dropContainer.MakeInstance(levelManager.GamePanel.gameObject);
                levelManager.dropContainerList.Add(drop);
                drop.transform.position = transform.position;
                drop.Init(1, (int)(exp /5f), 5);
                for (int i = 0; i < drop.dropPieces.Count; i++)
                {
                    levelManager.dropPiecesList.Add(drop.GetDropPieces(i));
                }
            }
            if (hpReward > 0)
            {
                Debug.Log("HP ADDED: " + hpReward);
                var drop = dropContainer.MakeInstance(levelManager.GamePanel.gameObject);
                levelManager.dropContainerList.Add(drop);
                drop.transform.position = transform.position;
                drop.Init(2, hpReward , 1);
                for (int i = 0; i < drop.dropPieces.Count; i++)
                {
                    levelManager.dropPiecesList.Add(drop.GetDropPieces(i));
                }
            }

            stuffDropped = true;
        }
    }
}
