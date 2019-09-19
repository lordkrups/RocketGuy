using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter
{
    public PlayerBrain playerBrain;
    public TopDownPlayerMotor motor;
    public UIWidget bulletBox;

    public Transform gunEnd;
    public Transform targ3D;
    public Vector2 tard2D;
    public PlayerProjectile playerProjectile;
    public int weaponToUse;
    public int damage;

    public void Init(PlayerBrain pb, TopDownPlayerMotor pm, PlayerProjectile pp, Transform we, UIWidget bb)
    {
        playerBrain = pb;
        motor = pm;
        playerProjectile = pp;
        gunEnd = we;
        bulletBox = bb;
    }

    public void FireGun(int wId, float range, int dmg)
    {
        //Debug.Log("FireGun: " );
        weaponToUse = wId;


        //Debug.Log("EnemiesPresentCheck: " + playerBrain.levelManager.EnemiesPresentCheck());

        damage = dmg;
        targ3D = FiringSolution();
        tard2D = new Vector2(targ3D.position.x, targ3D.position.y);

        if (!targ3D.GetComponent<EnemyMultiBrain>().IsSneekingCheck())
        {
            if (Vector2.Distance(playerBrain.transform.position, tard2D) <= range)
            {

                if (EverythingLoader.Instance.WeaponInfos[weaponToUse].pattern == "cone")
                {
                    Debug.Log("FireCone: ");

                    FireCone(dmg, range);
                    return;
                }

                var projectile = playerProjectile.MakeInstance(bulletBox.gameObject);
                projectile.Init(weaponToUse);
                Physics2D.IgnoreCollision(projectile.GetComponent<CapsuleCollider2D>(), motor.playerCollider);




                if (EverythingLoader.Instance.WeaponInfos[weaponToUse].add_pattern == "crossed")
                {
                    projectile.Setting(damage, range, playerBrain, FireCross);
                }
                else
                {
                    projectile.Setting(damage, range, playerBrain);
                }

                projectile.transform.position = gunEnd.transform.position;
                projectile.SetTarget(tard2D);
                //motor.SetLook(projectile.GetAngle());
            }
        }
        
        //tard2D = new Vector2(0f, 0f);

        playerBrain.isShooting = false;

    }

    private Transform FiringSolution()
    {
        Transform targetDir;

        targetDir = GetClosestEnemy(playerBrain.levelManager.GetEnemyList());
        //Vector2 targ = new Vector2(targetDir.position.x, targetDir.position.y);

        return targetDir;
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


    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = gunEnd.transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
    public void FireCone(int dmg, float range)
    {
        damage = dmg;

        var projectile = playerProjectile.MakeInstance(bulletBox.gameObject);
        projectile.Init(weaponToUse);

        projectile.Init(weaponToUse);
        projectile.Setting(damage, range, playerBrain);

        projectile.transform.position = gunEnd.transform.position;

        targ3D = FiringSolution();

        tard2D = new Vector2(targ3D.position.x, targ3D.position.y);

        projectile.SetTarget(tard2D);

        projectile = playerProjectile.MakeInstance(bulletBox.gameObject);
        projectile.Init(weaponToUse);

        projectile.Init(weaponToUse);
        projectile.Setting(damage, range, playerBrain);

        projectile.transform.position = gunEnd.transform.position;

        targ3D = FiringSolution();

        tard2D = new Vector2(targ3D.position.x + 0.5f, targ3D.position.y + 0.5f);

        projectile.SetTarget(tard2D);

        projectile = playerProjectile.MakeInstance(bulletBox.gameObject);
        projectile.Init(weaponToUse);

        projectile.Init(weaponToUse);
        projectile.Setting(damage, range, playerBrain);

        projectile.transform.position = gunEnd.transform.position;

        targ3D = FiringSolution();

        tard2D = new Vector2(targ3D.position.x - 0.5f, targ3D.position.y - 0.5f);

        projectile.SetTarget(tard2D);
    }
    public void FireCross(Vector3 pos, float range)
    {
        for (int i = 1; i < 5; i++)
        {
            var project = new PlayerProjectile();
            project = playerProjectile.MakeInstance(bulletBox.gameObject);
            //project.weaponStats.Init(4);
            project.Init(20);
            project.Setting(damage/4, range);
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
    }
}
