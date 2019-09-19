using System.Collections; using System.Collections.Generic; using UnityEngine;  public class TopDownPlayerMotor : MonoBehaviour {

    public bool showGizmoAndLocation;

    public LevelManager levelManager;     public CapsuleCollider2D playerCollider;     public PlayerBrain playerBrain;

    public float varSpeed = 0;          public Vector2 _movement;     public Vector2 _input;     public float _angle;
    public Rigidbody2D rb;     private Quaternion _targetRotation; 
    public bool hasTarget;      void FixedUpdate()     {         if (hasTarget && playerBrain.isAlive && playerBrain.canMove)
        {             MovePlayer();         }
    }      public void SetLook(float angle)     {
        _targetRotation = Quaternion.Euler(0, 0, angle);          rb.SetRotation(_targetRotation);     } 
    public void SetMove(float h, float v)     {         if (h != 0 || v != 0)         {             varSpeed += 0.1f;         if (varSpeed > playerBrain.playerStats.MoveSpeed)         {             varSpeed = playerBrain.playerStats.MoveSpeed;         }             _input.x = h;             _input.y = v;         }         if (h == 0 && v == 0)         {             varSpeed = 0;
            _input.x = 0;             _input.y = 0;         }     }      public void MovePlayer()     {
        _movement.Set(_input.x, _input.y);         _movement = _movement.normalized * varSpeed;          rb.MovePosition(rb.position + _movement * Time.deltaTime);     }
     public Vector2 GetMoveDirection()
    {
        return _movement;
    }

    void OnCollisionEnter2D(Collision2D col)     {

        if (col.gameObject.CompareTag("LevelGoal"))         {             Debug.Log("PLAYER LevelGoal :D");             if (!playerBrain.levelManager.levelLoading)
            {
                playerBrain.levelManager.GoToNextLevel();             }         }
    }      private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("OnCollisionStay2D");

            StartCoroutine(RunAway(collision.gameObject.GetComponent<CapsuleCollider2D>()));
        }
    }      IEnumerator RunAway(CapsuleCollider2D enemy)
    {
        Physics2D.IgnoreCollision(enemy, playerCollider);

        yield return new WaitForSeconds(1f);
        if (enemy != null)
        {
            Physics2D.IgnoreCollision(enemy, playerCollider, false);
        }

    }
}  