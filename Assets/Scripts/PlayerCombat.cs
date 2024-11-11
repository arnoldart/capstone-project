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
    public void PerformAttack(string attackType)
    {
        if (playerHealth.IsDead())
            return; // Tidak bisa menyerang jika mati

        switch (attackType)
        {
            case "SingleSlash":
                animator.SetTrigger("SingleSlash");
                ApplyDamage();
                break;
            // case "LightWeapon":
            //     animator.SetTrigger("LightWeapon");
            //     ApplyDamage();
            //     break;
            // case "MediumWeapon":
            //     animator.SetTrigger("MediumWeapon");
            //     ApplyDamage();
            //     break;
            // case "HeavyWeapon":
            //     animator.SetTrigger("HeavyWeapon");
            //     ApplyDamage();
            //     break;
            // case "SkyfallBlast":
            //     animator.SetTrigger("SkyfallBlast");
            //     ApplyDamage();
            //     break;
            // case "CircleBlast":
            //     animator.SetTrigger("CircleBlast");
            //     ApplyDamage();
            //     break;
            default:
                break;
        }
    }

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

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
