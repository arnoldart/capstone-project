using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator; // Untuk animasi serangan
    public Transform attackPoint; // Posisi serangan melee
    public float attackRange = 1f; // Jarak serangan melee
    public LayerMask enemyLayers; // Layer musuh yang bisa diserang
    public int attackDamage = 10; // Damage serangan melee

    public GameObject projectilePrefab; // Prefab proyektil
    public Transform projectileSpawnPoint; // Posisi munculnya proyektil
    public float projectileCooldown = 1f; // Waktu cooldown proyektil

    private float nextProjectileTime = 0f; // Timer untuk cooldown proyektil
    private PlayerHealth playerHealth; // Skrip kesehatan pemain

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Klik kiri untuk serangan melee
        {
            PerformAttack();
        }

        if (Input.GetMouseButtonDown(1) && Time.time >= nextProjectileTime) // Klik kanan untuk proyektil
        {
            ShootProjectile();
            nextProjectileTime = Time.time + projectileCooldown; // Set cooldown proyektil
        }
    }

    // Melakukan serangan melee
    public void PerformAttack()
    {
        if (playerHealth.IsDead())
            return;

        if (animator != null)
        {
            animator.SetTrigger("singleSlash"); // Trigger animasi melee
        }

        ApplyDamage();
    }

    // Memberikan damage kepada musuh dalam radius serangan melee
    void ApplyDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    // Menembakkan proyektil
    void ShootProjectile()
    {
        if (playerHealth.IsDead())
            return;

        // Membuat proyektil
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        // Mengatur arah proyektil berdasarkan arah pemain
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        }

        if (animator != null)
        {
            animator.SetTrigger("Shoot"); // Trigger animasi tembakan
        }
    }

    // Menampilkan radius serangan melee di editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
