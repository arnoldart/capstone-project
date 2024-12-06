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
    private Vector2 lastDirection = Vector2.right; // Default ke kanan

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
            Vector2 projectileDirection = GetProjectileDirection(); // Dapatkan arah proyektil
            ShootProjectile(projectileDirection);
            nextProjectileTime = Time.time + projectileCooldown; // Set cooldown proyektil
        }
    }

    // Mendapatkan arah proyektil berdasarkan input keyboard
    Vector2 GetProjectileDirection()
    {
        if (Input.GetKey(KeyCode.W)) lastDirection = Vector2.up;    // Atas
        if (Input.GetKey(KeyCode.S)) lastDirection = Vector2.down;  // Bawah
        if (Input.GetKey(KeyCode.A)) lastDirection = Vector2.left;  // Kiri
        if (Input.GetKey(KeyCode.D)) lastDirection = Vector2.right; // Kanan

        return lastDirection; // Mengembalikan arah terakhir yang ditekan
    }

    // Melakukan serangan melee
    public void PerformAttack()
    {
        if (playerHealth != null && playerHealth.IsDead())
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
                enemyHealth.TakeDamage(attackDamage); // Memberikan damage pada musuh
            }
        }
    }

    // Menembakkan proyektil ke arah tertentu
    void ShootProjectile(Vector2 direction)
    {
        if (playerHealth != null && playerHealth.IsDead())
            return;

        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        // Set direction of the projectile
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.direction = direction; // Set arah proyektil
        }

        // Set the animation direction based on the projectile's direction
        if (animator != null)
        {
            animator.SetFloat("directionX", direction.x);
            animator.SetFloat("directionY", direction.y);
            animator.SetTrigger("Shoot"); // Trigger animasi tembakan
        }
    }

    // Menampilkan radius serangan melee di editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); // Menampilkan area serangan
    }
}