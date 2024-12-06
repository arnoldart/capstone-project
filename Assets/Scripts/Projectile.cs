using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Kecepatan proyektil
    public int damage = 10; // Damage proyektil
    public float lifetime = 5f; // Durasi hidup proyektil
    public Vector2 direction = Vector2.right; // Arah default proyektil

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = direction.normalized * speed; // Proyektil bergerak sesuai arah
        }

        Destroy(gameObject, lifetime); // Hancurkan setelah waktu tertentu
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage); // Berikan damage ke musuh
            Destroy(gameObject); // Hancurkan proyektil
        }

        // Opsional: Tambahkan logika untuk dinding atau objek lain.
    }
}
