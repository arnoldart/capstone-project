using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    [SerializeField] private float attackRange;      // Jarak serangan musuh
    [SerializeField] private float chaseRange;       // Jarak pengejaran musuh
    [SerializeField] private int attackDamage;       // Kerusakan serangan
    [SerializeField] private float attackRate;       // Kecepatan serangan
    [SerializeField] private int maxAttackCount;     // Jumlah maksimum serangan
    [SerializeField] private float attackCooldown;   // Cooldown serangan

    private float nextAttackTime;
    private int currentAttackCount;
    private bool isCooldown;

    private Transform player;
    private Animator animator;

    private Vector3 initialPosition;                 // Posisi awal musuh
    private EnemyMovement enemyMovement;

    private void Start()
    {
        // Simpan posisi awal musuh
        initialPosition = transform.position;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found!");
        }

        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        if (enemyMovement == null)
        {
            Debug.LogError("EnemyMovement component not found!");
        }

        if (player != null && enemyMovement != null)
        {
            enemyMovement.Initialize(player); // Set target di EnemyMovement
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // Jika dalam jarak serangan, lakukan serangan
            HandleCombat();
        }
        else if (distanceToPlayer <= chaseRange)
        {
            // Jika dalam jarak pengejaran, kejar pemain
            enemyMovement?.MoveTowards(player.position);
        }
        else
        {
            // Jika terlalu jauh, kembali ke posisi awal
            enemyMovement?.MoveTowards(initialPosition);
        }
    }

    private void HandleCombat()
    {
        if (isCooldown || Time.time < nextAttackTime) return;

        AttackPlayer();

        nextAttackTime = Time.time + 1f / attackRate;
        currentAttackCount++;

        if (currentAttackCount >= maxAttackCount)
        {
            StartCoroutine(AttackCooldown());
        }
    }

    private void AttackPlayer()
    {
        if (player == null) return;

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            animator.SetTrigger("Attack");
            playerHealth.TakeDamage(attackDamage);
        }
        else
        {
            Debug.LogError("PlayerHealth component not found on Player!");
        }
    }

    private IEnumerator AttackCooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        currentAttackCount = 0;
        isCooldown = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (transform == null) return;

        // Visualisasi attackRange
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Visualisasi chaseRange
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}