using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public int health = 100;
    public int maxHealth;
    protected bool isDead = false;
    
    protected virtual void Awake()
    {
        maxHealth = health; // Set maxHealth di awal sebagai health awal
    }
    
    public bool IsDead()
    {
        return isDead;
    }
    
    private void Start()
    {
        Initialize();
    }

    // Fungsi untuk menerima damage
    public virtual void TakeDamage(int damage)
    {
        if (isDead)
            return;

        health -= damage;
        health = Mathf.Max(health, 0);
        // Debug.Log(gameObject.name + " health after damage: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    // Fungsi abstrak untuk mengatur kematian
    protected abstract void Die();

    // Fungsi untuk inisialisasi tambahan di turunan
    protected virtual void Initialize() { }
}
