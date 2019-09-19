using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
    public LevelManager levelManager;
    public PlayerStats playerStats;
    public TopDownPlayerMotor playerMotor;
    public PlayerShooter shooter;
    public PlayerProjectile playerProjectile;
    public DamagePopup damagePopup;
    public Transform gunEnd;
    public UIWidget bulletBox;
    public PowerUpObj powerUpObj;

    public List<PowerUpObj> activePowerUps;
    public int activatePowersCount;
    public bool puAdded;
    public int currentHat;

    public UISprite playerSrite;
    public UISprite invincibleSprite;
    public UILabel healthLabel;
    public UISlider healthSlider;

    public bool isAlive;
    public bool invincibleNow;
    public float invicibleCountdown;

    public float timeSinceLastShot = 1f;
    
    public int coinsEarnedThisLevel;
    public int expEarnedThisLevel;

    public bool canMove;
    public bool isShooting;
    public bool canShoot;

    public int extraDmgRef;
    public int extraHPRef;
    public int extraXPRef;
    public float extraAtkSpeed;

    public int currentWeapon;

    private Coroutine _currentCoroutine;

    public void Init(LevelManager lm, UIWidget bb)
    {
        levelManager = lm;
        bulletBox = bb;

        playerStats = EverythingLoader.Instance.GetPlayerStats();

        playerSrite.spriteName = playerStats.PlayerSprite;
        playerStats.AttackSpeed = 2f - playerStats.AttackSpeed;
        shooter = new PlayerShooter();
        shooter.Init(this, playerMotor, playerProjectile, gunEnd, bulletBox);
        currentHat = EverythingLoader.Instance.gameManager.currentHat;
        ApplyHat();
        ApplySkills();
        Reset();
        playerStats.CurrentHealth = playerStats.MaxHealth;
        currentWeapon = playerStats.Weapon;
        CalculateHealth();


    }
    private void CalculateHealth()
    {

        healthLabel.text = playerStats.CurrentHealth.ToString() + " / " + playerStats.MaxHealth;
        healthSlider.value = playerStats.CurrentHealth / (float)playerStats.MaxHealth;
    }

    public void Reset()
    {
        isAlive = true;
        canShoot = false;
        canMove = false;
        //playerStats.CurrentHealth = playerStats.MaxHealth;
        //activePowerUps = new List<PowerUpObj>();

        _currentCoroutine = StartCoroutine(StartShoot(2f));
        StartCoroutine(StartShoot(2f));
    }

    public void AddPowerUp(int pu)
    {
        //Debug.Log("selected powerup: " + pu);

        var puo = new PowerUpObj();
        puo = powerUpObj.MakeInstance(transform.gameObject);
        puo.Init(EverythingLoader.Instance.PowerUpInfos[pu]);
        Debug.Log("selected powerup: " + puo.powerTitle);

        activePowerUps.Add(puo);

        activatePowersCount = activePowerUps.Count - 1;
        puAdded = false;

        UpdateStatsWithPowerUP();
    }

    IEnumerator StartShoot(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
        canMove = true;
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            healthSlider.transform.rotation = Quaternion.Euler(0, 0, 0);
            healthSlider.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 66f, 0);

            playerSrite.transform.rotation = Quaternion.Euler(0, 0, 0);


            if (playerMotor._input.x == 0 && playerMotor._input.y == 0)
            {//No input
                //Player not moving so shoot
                if (Time.fixedTime > (timeSinceLastShot + playerStats.AttackSpeed) && canShoot)
                {
                    playerMotor.hasTarget = false;
                    //Debug.Log("SHOOT");
                    Shoot();

                    if (shooter.tard2D.x < transform.position.x)
                    {
                        playerSrite.flip = UIBasicSprite.Flip.Horizontally;
                    }
                    if (shooter.tard2D.x > transform.position.x)
                    {
                        playerSrite.flip = UIBasicSprite.Flip.Nothing;
                    }
                }

            }
            else
            {//Yes input
                if (canMove)
                {
                    playerMotor.hasTarget = true;

                    if (playerMotor._input.x < -0.5f)
                    {
                        playerSrite.flip = UIBasicSprite.Flip.Horizontally;
                    }
                    if (playerMotor._input.x > -0.5f)
                    {
                        playerSrite.flip = UIBasicSprite.Flip.Nothing;
                    }
                }
            }

            invicibleCountdown -= Time.fixedDeltaTime;

            if (playerStats.Invincible && invicibleCountdown <= 0.0f)
            {
                StartCoroutine(Invincible());
            }

            if (playerStats.Invincible && Time.fixedTime > invicibleCountdown)
            {
                //StartCoroutine(Invincible());
            }

        }
        else
        {
            return;
        }
    }
    IEnumerator Invincible()
    {
        invincibleNow = true;
        invincibleSprite.On();
        invicibleCountdown = Time.deltaTime + 30f + playerStats.Invincibletime;

        yield return new WaitForSeconds(playerStats.Invincibletime);

        invincibleSprite.Off();
        invincibleNow = false;

        yield return 0;
    }
   /* IEnumerator MultiCo()
    {
        yield return StartCoroutine(GenerateMaze(0, 0, 0));
        yield return StartCoroutine(GenerateMaze(24, 24, 1));
    }*/
    //From Virtual Controller via levelManager
    //Maybe move to direct control
    public void SetMove(float h, float v)
    {
        if (canMove)
        {
            playerMotor.SetMove(h, v);
        }
        if (!canMove)
        {
            playerMotor.SetMove(0f, 0f);
            playerMotor._movement = new Vector2(0f, 0f);
        }
    }
    private int CalculateCritDamage()
    {
        int dmg = (int)(playerStats.AttackPower / 100f * playerStats.CriticalHitDamagePercentage);
        //Debug.Log("CRIT DMG: " + dmg);
        return dmg;
    }

    private void Shoot()
    {
        if (levelManager.EnemiesPresentCheck())
        {
            isShooting = true;
            timeSinceLastShot = Time.time;
            //shooter.Init(playerStats.weapon);
            //Debug.Log("Shoot: ");

            int dmgBoost = 0;
            int wpnDmg = EverythingLoader.Instance.WeaponInfos[playerStats.Weapon].dmgMod;

            if (playerStats.Rage)
            {
                float healthPercentage = (playerStats.CurrentHealth / (float)playerStats.MaxHealth) * 100;
                //Debug.Log("rage mode hp perc: " + healthPercentage);

                if (healthPercentage < 50)
                {
                    int missHP = 50 - (int)healthPercentage;
                    dmgBoost = (int)(playerStats.AttackPower * missHP / 400f);
                    //Debug.Log("rage mode atk boost: " + dmgBoost);
                    //extraDmgRef = dmgBoost;
                }

            }

            int randomPoint = Random.Range(1, 101);

            if (randomPoint <= playerStats.CriticalHitRate)
            {
                dmgBoost += CalculateCritDamage();
            }

            //Debug.Log("Total Damage: " + (dmgBoost + playerStats.AttackPower));
            levelManager.audioManager.PlaySFXClip("playerShoot");

            //Add all boosts and shoot
            shooter.FireGun(playerStats.Weapon, playerStats.AttackRange, dmgBoost + playerStats.AttackPower + wpnDmg);
        }
    }

    public void GainHealth(int hp)
    {
        //Debug.Log("heal hp: " + hp);

        if (playerStats.HealPercentage !=0)
        {
            float hpp = (playerStats.MaxHealth / 100f) * playerStats.HealPercentage;
            hp += (int)hpp;

            Debug.Log("hpp 1 : " + hpp);
            Debug.Log("heal hp with boost: " + hp);
        }

        if (playerStats.CurrentHealth < playerStats.MaxHealth)
        {
            playerStats.CurrentHealth += hp;
        }
        if (playerStats.CurrentHealth > playerStats.MaxHealth)
        {
            playerStats.CurrentHealth = playerStats.MaxHealth;
        }

        CalculateHealth();
    }

    public void TakeDamageLocation(Vector3 hitPos, int hitType = 0)
    {
        if (isAlive && !invincibleNow)
        {
            if (hitType == 1)
            {
                playerSrite.GetComponent<SpriteAnims>().FlashRed(hitType, hitPos.x, hitPos.y);
            }
            if (hitType == 1)
            {
                playerSrite.GetComponent<SpriteAnims>().FlashRed(hitType, hitPos.x, hitPos.y);
            }
            if (hitType == 2)
            {
                playerSrite.GetComponent<SpriteAnims>().FlashRed(hitType, hitPos.x, hitPos.y);
            }
        }
    }
    public void TakeDamage(int dmg)
    {
        if (isAlive && !invincibleNow)
        {
            int damageToApply = (int)(dmg / 100f * EverythingLoader.Instance.gameManager.unlockedSkillsList[2]);
            Debug.Log("damageToApply: " + damageToApply);

            damagePopup.DamagePop(dmg);
            playerStats.CurrentHealth -= dmg - damageToApply;

            playerSrite.GetComponent<SpriteAnims>().FlashRed();

            CalculateHealth();

            if (playerStats.CurrentHealth <= 0)
            {
                playerStats.CurrentHealth = 0;
                CalculateHealth();

                playerSrite.GetComponent<SpriteAnims>().FlashRed(3, transform.position.x, transform.position.y);

                isAlive = false;
                canMove = false;
                canShoot = false;
                Debug.Log("PLAYER DEAD =( ");

            }
        }
    }
    public void ApplySkills()
    {
        playerStats.MaxHealth += EverythingLoader.Instance.SkillInfos[0].hpInc * EverythingLoader.Instance.gameManager.unlockedSkillsList[0];
        //Debug.Log("skill hp inc: " + EverythingLoader.Instance.SkillInfos[0].hpInc * EverythingLoader.Instance.gameManager.unlockedSkillsList[0]);

        playerStats.AttackPower += (int) EverythingLoader.Instance.SkillInfos[1].atkInc * EverythingLoader.Instance.gameManager.unlockedSkillsList[1];
        extraDmgRef += (int)EverythingLoader.Instance.SkillInfos[1].atkInc * EverythingLoader.Instance.gameManager.unlockedSkillsList[1];
        //Debug.Log("skill attack inc: " + (int)EverythingLoader.Instance.SkillInfos[1].atkInc * EverythingLoader.Instance.gameManager.unlockedSkillsList[1]);

        playerStats.ReduceCollisionsDamageAmount += EverythingLoader.Instance.SkillInfos[2].bulwark * EverythingLoader.Instance.gameManager.unlockedSkillsList[2];
        playerStats.ReduceRangedDamageAmount += EverythingLoader.Instance.SkillInfos[2].bulwark * EverythingLoader.Instance.gameManager.unlockedSkillsList[2];
        //Debug.Log("skill bulwark col inc: " + EverythingLoader.Instance.SkillInfos[2].bulwark * EverythingLoader.Instance.gameManager.unlockedSkillsList[2]);
        //Debug.Log("skill bulwark ran inc: " + EverythingLoader.Instance.SkillInfos[2].bulwark * EverythingLoader.Instance.gameManager.unlockedSkillsList[2]);
        //Debug.Log("red col: " + playerStats.ReduceCollisionsDamageAmount);
        //Debug.Log("red ran: " + playerStats.ReduceRangedDamageAmount);

        playerStats.HealPercentage += EverythingLoader.Instance.gameManager.unlockedSkillsList[3];
        //Debug.Log("skill heal on level up inc: " + EverythingLoader.Instance.gameManager.unlockedSkillsList[3]);

        playerStats.HealOnLevelUp += EverythingLoader.Instance.SkillInfos[4].inspire * EverythingLoader.Instance.gameManager.unlockedSkillsList[4];
        //Debug.Log("skill HealPercentage inc: " + EverythingLoader.Instance.SkillInfos[4].inspire * EverythingLoader.Instance.gameManager.unlockedSkillsList[4]);

        playerStats.AttackSpeed -= EverythingLoader.Instance.gameManager.unlockedSkillsList[5]/100f;
        extraAtkSpeed -= EverythingLoader.Instance.gameManager.unlockedSkillsList[5] / 100f;
        //Debug.Log("skill att speed inc: " + EverythingLoader.Instance.gameManager.unlockedSkillsList[5] / 100f);
        //Debug.Log("skill att speed inc: " + EverythingLoader.Instance.gameManager.unlockedSkillsList[5]);

        playerStats.Enhance += EverythingLoader.Instance.SkillInfos[7].enhance * EverythingLoader.Instance.gameManager.unlockedSkillsList[7];
        //Debug.Log("skill Enhance inc: " + EverythingLoader.Instance.gameManager.unlockedSkillsList[7]);

        if (EverythingLoader.Instance.gameManager.unlockedSkillsList[8] == 1)
        {
            levelManager.numberOfLevelUps = 1;
        }

    }
    public void ApplyHat()
    {
        playerStats.AttackPower += (int)(playerStats.AttackPower / 100f * EverythingLoader.Instance.HatInfos[currentHat].atkInc) + (int)(EverythingLoader.Instance.HatInfos[currentHat].atkInc / 10f) * playerStats.Enhance;
        playerStats.AttackSpeed -= playerStats.AttackSpeed / 100f * EverythingLoader.Instance.HatInfos[currentHat].atkSpdInc + (int)(EverythingLoader.Instance.HatInfos[currentHat].atkSpdInc / 10f * playerStats.Enhance);
        playerStats.CriticalHitRate += EverythingLoader.Instance.HatInfos[currentHat].critInc + (int)(EverythingLoader.Instance.HatInfos[currentHat].critInc / 10f * playerStats.Enhance);
        playerStats.MaxHealth +=  EverythingLoader.Instance.HatInfos[currentHat].hpInc + (int)(EverythingLoader.Instance.HatInfos[currentHat].hpInc / 10f * playerStats.Enhance);
        playerStats.HealPercentage += EverythingLoader.Instance.HatInfos[currentHat].healPerc + (int)(EverythingLoader.Instance.HatInfos[currentHat].healPerc / 10f * playerStats.Enhance);
    }
        public void UpdateStatsWithPowerUP()
    {
        if (!puAdded)
        {
            if (activePowerUps[activatePowersCount].atkIncrease > 0)
            {
                extraDmgRef += (int)(playerStats.AttackPower / 100f * activePowerUps[activatePowersCount].atkIncrease);
                playerStats.AttackPower += (int)(playerStats.AttackPower / 100f * activePowerUps[activatePowersCount].atkIncrease);
            }

            if (activePowerUps[activatePowersCount].atkSpdIncrease > 0)
            {
                extraAtkSpeed += (playerStats.AttackSpeed / 100f * activePowerUps[activatePowersCount].atkSpdIncrease);
                playerStats.AttackSpeed -= (playerStats.AttackSpeed / 100f * activePowerUps[activatePowersCount].atkSpdIncrease);
            }

            if (activePowerUps[activatePowersCount].critIncrease > 0)
                playerStats.CriticalHitRate += activePowerUps[activatePowersCount].critIncrease;

            if (activePowerUps[activatePowersCount].rage)
                playerStats.Rage = activePowerUps[activatePowersCount].rage;

            if (activePowerUps[activatePowersCount].hpBoost > 0)
            {
                extraHPRef += (int)(playerStats.MaxHealth / 100f * activePowerUps[activatePowersCount].hpBoost);
                playerStats.MaxHealth += (int)(playerStats.MaxHealth / 100f * activePowerUps[activatePowersCount].hpBoost);
                CalculateHealth();
            }

            if (activePowerUps[activatePowersCount].healPerc > 0)
                playerStats.HealPercentage += activePowerUps[activatePowersCount].healPerc;

            if (activePowerUps[activatePowersCount].bloodThirst)
                playerStats.BloodThirst = activePowerUps[activatePowersCount].bloodThirst;

            if (activePowerUps[activatePowersCount].invincibility)
            {
                playerStats.Invincible = activePowerUps[activatePowersCount].invincibility;
                playerStats.Invincibletime = activePowerUps[activatePowersCount].invincibleTime;
            }

            if (activePowerUps[activatePowersCount].smart)
                playerStats.Smart = activePowerUps[activatePowersCount].smart;

        }
        activatePowersCount++;
        puAdded = true;
    }
}
