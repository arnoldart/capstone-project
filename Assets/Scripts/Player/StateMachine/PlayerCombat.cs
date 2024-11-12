using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    public int attackDamage;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        // Mungkin ditangani oleh PlayerStateMachine
    }

    // Method untuk melakukan serangan berdasarkan tipe
    public void PerformAttack()
    {
        if (playerHealth.IsDead())
            return; // Tidak bisa menyerang jika mati

        // Set trigger animasi berdasarkan tipe serangan

        // Terapkan damage jika tidak dalam kondisi mati
        if (!playerHealth.IsDead())
        {
            ApplyDamage();
        }
    }

    // Method untuk mendeteksi dan memberi damage kepada musuh yang terkena
    void ApplyDamage()
    {
        // Deteksi musuh dalam jarak serangan menggunakan OverlapCircle
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage musuh yang terkena
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    // Menampilkan jarak serangan dalam editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
