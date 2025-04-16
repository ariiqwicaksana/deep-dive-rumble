using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameManager manager;
    public bool isOnShop = false;
    public GameObject shop;

    [Header("Abillity Settings")]
    public bool isAddOxygen;
    public bool isBubbleAttack;
    [SerializeField] GameObject bubbleAttacks;
    public bool isInvincible;
    [SerializeField] List<CapsuleCollider2D> playerCollider;
    [SerializeField] float invicibleTime;


    [Space]
    public float moveSpeed = 5f; 
    private Rigidbody2D rb;
    private Vector2 movement;
    
    public GameManager gm;
    private bool isInOxygenZone = false;
    public float healthDrainRate = 10f;
    public float healthRegenRate = 5f;

    public ParticleSystem trailParticle; // Reference to the Particle System

    private bool isMoving; // Check if the player is moving

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shop.SetActive(false);
    }

    void Update()
    {
        movement = Vector2.zero;
        isMoving = false;

        if (Input.GetKey(KeyCode.W)) 
        {
            movement.y = 1;
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.S)) 
        {
            movement.y = -1;
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.A)) 
        {
            movement.x = -1;
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            movement.x = 1;
            isMoving = true;
        }

        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        HandleHealth();
        HandleParticles(); // Manage particle system

        //ability handling
        if(isAddOxygen)
        {
            AddOxygen();
        }

        if (isInvincible)
        {
            Invincible();
        }

        if (isBubbleAttack)
{
    if (Input.GetKeyDown(KeyCode.E))
    {
        Debug.Log("menekan shoot");
        GameObject bubble = Instantiate(bubbleAttacks, transform.position, Quaternion.identity);
        
        // Pastikan arah gerak tidak nol
        Vector2 shootDirection = movement != Vector2.zero ? movement : Vector2.up; 
        
        bubble.GetComponent<BubbleMovement>().direction = shootDirection;
        isBubbleAttack = false;
    }
}

        if(isOnShop)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                openShop();
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            gm.coinCount++;
        }
        if (other.gameObject.CompareTag("Piece"))
        {
            Destroy(other.gameObject);
            gm.pieceCount++;
        }
        if (other.gameObject.CompareTag("Scroll"))
        {
            Destroy(other.gameObject);
            gm.scrollCount++;
        }
        if (other.gameObject.CompareTag("Oxygen"))
        {
            isInOxygenZone = true; 
        }

        if(other.gameObject.CompareTag("Enemy"))
        {
            manager.GameOver();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Shop"))
        {
            isOnShop = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Oxygen"))
        {
            isInOxygenZone = false;
        }
        if(other.gameObject.CompareTag("Shop"))
        {
            isOnShop = false;
        }
    }

    void HandleHealth()
    {
        if (isInOxygenZone)
        {
            gm.healthAmount += healthRegenRate * Time.deltaTime; 
        }
        else
        {
            gm.healthAmount -= healthDrainRate * Time.deltaTime; 
        }

        gm.healthAmount = Mathf.Clamp(gm.healthAmount, 0, 100); 
        gm.healthBar.fillAmount = gm.healthAmount / 100f; 
    }

    void HandleParticles()
    {
        if (isMoving)
        {
            if (!trailParticle.isPlaying)
            {
                trailParticle.Play(); // Start the particle system if moving
            }
        }
        else
        {
            if (trailParticle.isPlaying)
            {
                trailParticle.Stop(); // Stop the particle system if not moving
            }
        }
    }
    void AddOxygen()
    {
        gm.healthAmount += 10;
        if (gm.healthAmount > 100)
        {
            gm.healthAmount = 100;
        }
        isAddOxygen = false;
    }

    void Invincible()
{
    int playerLayer = gameObject.layer; // Layer player
    int enemyLayer = LayerMask.NameToLayer("Enemy"); // Pastikan "Enemy" adalah nama layer musuh kamu

    if (enemyLayer != -1) // Pastikan layer enemy ditemukan
    {
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true); // Abaikan interaksi player dan enemy
    }

    StartCoroutine(ReenableCollision(playerLayer, enemyLayer));
}

IEnumerator ReenableCollision(int playerLayer, int enemyLayer)
{
    yield return new WaitForSeconds(invicibleTime);
    Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false); // Aktifkan kembali interaksi player dan enemy
}

    void openShop()
    {
        shop.SetActive(true);
        Time.timeScale = 0;
        isOnShop = false;
    }

}