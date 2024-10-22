using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;  // Kesehatan pemain
    public bool isDead = false;  // Status kematian

    // Fungsi untuk menerima damage
    public void TakeDamage(int damage)
    {
        if (isDead)
            return; // Tidak menerima damage jika sudah mati

        health -= damage;
        health = Mathf.Max(health, 0);
        Debug.Log("Kesehatan pemain setelah menerima damage: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    // Fungsi untuk mengatur kematian pemain
    void Die()
    {
        isDead = true;  // Menandai pemain sebagai mati
        Debug.Log("Pemain mati!");
        // Tambahkan logika seperti menonaktifkan kontrol, menampilkan UI game over, dll.
    }
}
