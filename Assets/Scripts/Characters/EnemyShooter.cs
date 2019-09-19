using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter
{
    public EnemyMultiBrain brain;
    public EnemyMotor motor;
    public DropContainer dropContainer;

    public UIWidget bulletBox;

    public Transform gunEnd;

    public EnemyProjectile projectilePrefab;
    public Vector2 target;
    public bool shooting;
    public int weaponID;
    public int damage;

    public void Init(int wId, EnemyMultiBrain emb, EnemyMotor em, EnemyProjectile ep, Transform we)
    {
        weaponID = wId;

        brain = emb;
        motor = em;
        projectilePrefab = ep;
        gunEnd = we;
        //projectilePrefab.weaponStats.Init(weaponID);
    }

    public void FireGun( int dmg, float range)
    {
        if (brain.shootAbility == "cone")
        {
            FireCone(dmg, range);
            return;
        }

        shooting = true;
        damage = dmg;
        EnemyProjectile projectile = new EnemyProjectile();
        projectile = projectilePrefab.MakeInstance(brain.levelManager.bulletBox.gameObject);
        projectile.Init(weaponID);

        //for (int i = 0; i < brain.levelManager.enemiesList.Count; i++)//So that enemies and their projectiles do not hit each other
        //{
        //Physics2D.IgnoreCollision(projectile.GetComponent<CapsuleCollider2D>(), brain.levelManager.enemiesList[i].GetComponent<CapsuleCollider2D>());
        //}
        //if (projectilePrefab.weaponStats.Pattern == "none")
        if (EverythingLoader.Instance.WeaponInfos[weaponID].add_pattern == "crossed")
        {
            projectile.Settings(dmg, range, FireCross);
        }
        else
        {
            projectile.Settings(dmg, range);

        }


        projectile.transform.position = gunEnd.transform.position;

        if (brain.shootAbility == "burst")
        {
            float ranX = Random.Range(-0.3f, 0.3f);
            float ranY = Random.Range(-0.3f, 0.3f);

            target.x += ranX;
            target.y += ranY;
        }
        projectile.SetTarget(target);

        shooting = false;
    }
    public void TargetPlayer()
    {
        target = FiringSolution();
    }

    private Vector2 FiringSolution()
    {
        Transform targetDir;

        targetDir = brain.levelManager.playerBrain.transform;
        Vector2 targ = new Vector2(targetDir.position.x, targetDir.position.y);

        return targ;
    }

    private Quaternion CalculateDirection(Vector2 targ)
    {
        float _angle;
        _angle = Mathf.Atan2(targ.y, targ.x);
        _angle = Mathf.Rad2Deg * _angle;

        Quaternion _targetRotation;
        _targetRotation = Quaternion.Euler(0, 0, _angle);
        //transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, 100 * Time.deltaTime);
        return _targetRotation;
    }

    public void FireCone(int dmg, float range)
    {
        shooting = true;

        damage = dmg;

        EnemyProjectile projectile = new EnemyProjectile();
        projectile = projectilePrefab.MakeInstance(brain.levelManager.bulletBox.gameObject);

        projectile.Init(weaponID);
        projectile.Settings(dmg, range);

        projectile.transform.position = gunEnd.transform.position;

        Vector2 targ = new Vector2(brain.levelManager.playerBrain.transform.position.x, brain.levelManager.playerBrain.transform.position.y - 0.5f);

        projectile.SetTarget(targ);


        projectile = projectilePrefab.MakeInstance(brain.levelManager.bulletBox.gameObject);

        projectile.Init(weaponID);
        projectile.Settings(dmg, range);

        projectile.transform.position = gunEnd.transform.position;

        targ = new Vector2(brain.levelManager.playerBrain.transform.position.x, brain.levelManager.playerBrain.transform.position.y);

        projectile.SetTarget(targ);


        projectile = projectilePrefab.MakeInstance(brain.levelManager.bulletBox.gameObject);

        projectile.Init(weaponID);
        projectile.Settings(dmg, range);

        projectile.transform.position = gunEnd.transform.position;

        targ = new Vector2(brain.levelManager.playerBrain.transform.position.x, brain.levelManager.playerBrain.transform.position.y + 0.5f);

        projectile.SetTarget(targ);

        shooting = false;
    }

    public void FireCross(Vector3 pos, float range)
    {


        for (int i = 1; i < 5; i++)
        {
            EnemyProjectile project = new EnemyProjectile();
            project = projectilePrefab.MakeInstance(brain.levelManager.bulletBox.gameObject);
            project.Init(20);

            project.Settings(damage, range);
            project.transform.position = pos;
             if (i == 1)
            {
                Vector2 targ = new Vector2(pos.x, pos.y + 1.0f);
                project.SetTarget(targ);
            }
            if (i == 2)
            {
                Vector2 targ = new Vector2(pos.x, pos.y + -1.0f);
                project.SetTarget(targ);
            }
            if (i == 3)
            {
                Vector2 targ = new Vector2(pos.x + 1.0f, pos.y);
                project.SetTarget(targ);
            }
            if (i == 4)
            {
                Vector2 targ = new Vector2(pos.x + -1.0f, pos.y);
                project.SetTarget(targ);
            }
        }



        shooting = false;
    }
}
