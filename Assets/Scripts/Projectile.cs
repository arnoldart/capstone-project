using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Kecepatan proyektil
    public int damage = 10; // Damage yang diberikan proyektil
    public float lifetime = 5f; // Durasi hidup proyektil sebelum dihancurkan otomatis
    public Vector2 direction = Vector2.right; // Arah default proyektil

    void Start()
    {
        Destroy(gameObject, lifetime); // Hancurkan proyektil setelah durasi tertentu
    }

    void Update()
    {
        // Proyektil bergerak dalam dunia 2D
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Cek apakah proyektil mengenai musuh
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage); // Berikan damage
            Destroy(gameObject); // Hancurkan proyektil setelah mengenai musuh
        }

        // Proyektil juga bisa dihancurkan jika menabrak dinding atau objek lainnya (opsional)
    }
}
