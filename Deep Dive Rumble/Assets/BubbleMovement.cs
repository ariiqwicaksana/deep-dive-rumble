using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public Vector2 direction; // Arah gerak bubble
    public float speed = 5f; // Kecepatan bubble

    void Update()
    {
        // Menggerakkan bubble sesuai arah
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // Hapus bubble saat keluar dari layar
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Cek jika bubble menabrak objek dengan tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // Hancurkan enemy
            Destroy(gameObject);       // Hancurkan bubble
        }
    }
}