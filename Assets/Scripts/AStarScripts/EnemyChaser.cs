using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    public EnemyMultiBrain brain;
    public Grid grid;

    public Vector3 player;
    public float speed;
    public Vector3[] path;
    public int targetIndex;
    public bool chaseMode;
    public bool reachedTarget;
    public bool hasTarget;
    public bool setUPComplete;
    public bool stuck;
    public float stuckTime;

    public void Init()
    {
        grid = brain.levelManager.curLevelPF.aStarManager.grid;
        speed = brain.moveSpeed;
        //target = brain.levelManager.playerBrain.transform;
    }
   
     void Update()
    {
        if (!hasTarget || reachedTarget)
        {
            //hasTarget = false;
            StopCoroutine("FollowPath");
        }
    }


    public void SetTarget(Vector3 p)
    {
        player = p;
    }
    public void FindPath()
    {
        PathRequestManager.RequestPath(transform.position, player, OnPathFound);
    }
    public void EndPath()
    {
        StopCoroutine("FollowPath");
    }
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            reachedTarget = false;
            hasTarget = pathSuccessful;
            stuck = false;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
        if (!pathSuccessful)
        {
            stuck = true;

            //PathRequestManager.RequestPath(transform.position, player.transform.position, OnPathFound);
        }
    }

    IEnumerator FollowPath()
    {
        if (path.Length > 0)
        {
            Vector3 currentWaypoint = path[0];
            while (brain.isAlive)
            {
                if (transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        reachedTarget = true;
                        hasTarget = false;
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }

                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, brain.enemyStats.MoveSpeed * Time.deltaTime);

                yield return null;
            }
        }
        else
        {
            reachedTarget = true;
            hasTarget = false;
            yield break;
        }
    }
    private void OncollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Scenery"))
        {
            Debug.Log("OncollisionEnter2D");
            //reachedTarget = true;
            //hasTarget = false;

        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Scenery"))
        {
            Debug.Log("OnCollisionStay2D");
            //reachedTarget = true;
            //hasTarget = false;

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Scenery"))
        {
            Debug.Log("OnCollisionExit2D");
        }
    }
    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one * (0.2f));

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
