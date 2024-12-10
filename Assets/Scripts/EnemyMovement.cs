using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed;               // Kecepatan gerak musuh
    private Rigidbody2D rb;               // Referensi ke Rigidbody2D
    private Animator animator;            // Referensi ke Animator

    [Header("Detection Settings")]
    public float detectionRange;         // Jarak deteksi terhadap pemain
    private Transform target;            // Target musuh (pemain)

    [Header("Wandering Settings")]
    public float changeDirectionTime = 2f; // Interval perubahan arah
    private Vector2 randomDirection;     // Arah acak untuk berkelana
    private float directionTimer;        // Timer untuk perubahan arah

    private Vector2 movement;            // Arah pergerakan saat ini

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        InitializeTarget();
        ResetDirectionTimer();
        SetRandomDirection();
    }

    private void Update()
    {
        if (target == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (IsPlayerInDetectionRange(distanceToPlayer))
        {
            MoveTowardsPlayer();
        }
        else
        {
            Wander();
        }

        UpdateAnimatorParameters();
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        MoveEnemy();
    }

    private void InitializeTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    private bool IsPlayerInDetectionRange(float distance)
    {
        return distance <= detectionRange;
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        movement = direction;
    }

    private void Wander()
    {
        directionTimer -= Time.deltaTime;

        if (directionTimer <= 0)
        {
            SetRandomDirection();
            ResetDirectionTimer();
        }

        movement = randomDirection;
    }

    private void SetRandomDirection()
    {
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void ResetDirectionTimer()
    {
        directionTimer = changeDirectionTime;
    }

    private void MoveEnemy()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void UpdateAnimatorParameters()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
    }
}
