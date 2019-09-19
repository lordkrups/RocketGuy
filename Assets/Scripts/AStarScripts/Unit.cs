using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {


    public EnemyMultiBrain brain;
    public Grid grid;

    public Transform target;
    public float speed = 5;
    public Vector3[] path;
    public int targetIndex;
    public bool reachedTarget;
    public bool hasTarget;

    void Awake()
    {
        //grid = brain.levelParameters.aStarManager.grid;
        //target = brain.levelManager.playerBrain.transform;
    }
    void Update()
        {


        if (grid.setup && !hasTarget)
        {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        }
        if (reachedTarget)
        {
            StopCoroutine("FollowPath");
            //hasTarget = false;
        }
    }

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
			path = newPath;
			targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");

            hasTarget = pathSuccessful;
        }
        else
        {
            StopCoroutine("FollowPath");

        }
    }

	IEnumerator FollowPath() {
		Vector3 currentWaypoint = path[0];
		while (true) {
			if (transform.position == currentWaypoint) {
				targetIndex ++;
				if (targetIndex >= path.Length) {
                    reachedTarget = true;

                    yield break;
				}
				currentWaypoint = path[targetIndex];

            }

            Debug.Log(("path.Length: ") + path.Length);
            Debug.Log(("targetIndex: ") + targetIndex);
            transform.position = Vector3.MoveTowards(transform.position,currentWaypoint,speed * Time.deltaTime);


            yield return null;

		}

	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one * (0.12f - .1f));

                if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}
