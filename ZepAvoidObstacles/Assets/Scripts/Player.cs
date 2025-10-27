using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private readonly float ACCEL = 1.1f;
    private readonly float ADJUST = 0.003f;
    private readonly int DEFAULT_MAG = 1;
    private readonly float DEFAULT_SPD = 3f;

    Animator animator;
    Rigidbody2D _rigidbody;

    public float flapForce = 6f;
    public float forworadSpeed = 3f;
    public bool isDead = false;
    float deathCooldown = 0f;

    bool isFlap = false;

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
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gameManager.RestartGame();
                }
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = forworadSpeed;

        if (isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        _rigidbody.velocity = velocity;

        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return;

        if (isDead) return;

        isDead = true;
        deathCooldown = 1f;

        animator.SetInteger("isDie", 1);

        gameManager.GameOver();
    }

    public void PlayerLevelUp(float level)
    {
        if(level != 1)
            forworadSpeed = forworadSpeed * (DEFAULT_MAG + ADJUST * Mathf.Pow(level, ACCEL));
    }

    public void ClearAndSpeedReset()
    {
        forworadSpeed = DEFAULT_SPD;
    }
}
