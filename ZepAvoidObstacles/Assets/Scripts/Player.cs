using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private readonly float ACCEL = 1.1f;
    private readonly float ADJUST = 0.002f;
    private readonly int DEFAULT_MAG = 1;
    private readonly float DEFAULT_SPD = 6f;

    Animator animator;
    Rigidbody2D _rigidbody;
    GameObject ammo;

    public float horizontalDirection = 1f;
    public float speed = 6f;
    public bool isDead = false;
    float deathCooldown = 0f;

    bool isFlap = false;
    bool isDescent = false;
    float vertical = 0;

    public bool godMode = false;

    MiniGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = MiniGameManager._instance;

        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (animator == null)
        {
            Debug.LogError("Not Founded Animator");
        }

        if (_rigidbody == null)
        {
            Debug.LogError("Not Founded Animator");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                DeletePlayer(1f);
                gameManager.GameOver();
                Time.timeScale = 0f;
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            isFlap = true;
            vertical = Input.GetAxisRaw("Vertical");
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = horizontalDirection;

        if (isFlap)
        {
            velocity.y = vertical;
        }

        velocity = velocity.normalized * speed;
        _rigidbody.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return;
        if (isDead) return;

        isDead = true;
        deathCooldown = 1f;

        if (collision.gameObject.CompareTag("Ammo"))
        {
            animator.SetBool("expl", true);
        }

        if (collision.gameObject.CompareTag("obstacle") || collision.gameObject.CompareTag("Untagged"))
        {
            animator.SetInteger("isDie", 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (godMode) return;
        if (isDead) return;

        if (collision.gameObject.CompareTag("Bomb"))
        {
            animator.SetBool("expl", true);
        }
        else
        {
            return;
        }

        isDead = true;
        deathCooldown = 1f;
    }

    public void PlayerLevelUp(float level)
    {
        if (level != 1)
            speed = speed * (DEFAULT_MAG + ADJUST * Mathf.Pow(level, ACCEL));
    }

    public void ClearAndSpeedReset()
    {
        speed = DEFAULT_SPD;
    }

    private IEnumerator DeletePlayer(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
        Destroy(animator);
    }
}
