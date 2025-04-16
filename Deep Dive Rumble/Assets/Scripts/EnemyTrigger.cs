using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public string playerTag = "Player"; // Tag to identify the player
    public Transform[] patrolPoints = new Transform[10]; // Patrol waypoints
    public float speed; // Movement speed
    public float distanceBetween; // Distance to start chasing the player

    private GameObject player; // Reference to the player GameObject
    private float distance; // Distance between enemy and player
    private int targetPoint = 0; // Current patrol point index
    private bool isChasing = false; // Whether the enemy is chasing the player

    void Start()
    {
        // Find the player GameObject by tag
        player = GameObject.FindGameObjectWithTag(playerTag);

        // Check if the player was found
        if (player == null)
        {
            Debug.LogError("Player GameObject with tag '" + playerTag + "' not found.");
        }

        // patrolPoints[0] = GameObject.FindGameObjectWithTag("Waypoint11").transform;
        // patrolPoints[1] = GameObject.FindGameObjectWithTag("Waypoint12").transform;
        // patrolPoints[2] = GameObject.FindGameObjectWithTag("Waypoint13")?.transform;
        // patrolPoints[3] = GameObject.FindGameObjectWithTag("Waypoint14")?.transform;
        // patrolPoints[4] = GameObject.FindGameObjectWithTag("Waypoint15")?.transform;
        // patrolPoints[5] = GameObject.FindGameObjectWithTag("Waypoint16")?.transform;
        // patrolPoints[6] = GameObject.FindGameObjectWithTag("Waypoint17")?.transform;
        // patrolPoints[7] = GameObject.FindGameObjectWithTag("Waypoint18")?.transform;
        // patrolPoints[8] = GameObject.FindGameObjectWithTag("Waypoint19")?.transform;
        // patrolPoints[9] = GameObject.FindGameObjectWithTag("Waypoint20")?.transform;
    }

    void Update()
    {
        // Ensure the player exists
         if (player == null) return;

        // Calculate the distance to the player
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < distanceBetween)
        {
            // Start chasing the player
            isChasing = true;
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        // Move towards the player
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    // void Patrol()
    // {
    //     // Move towards the next patrol point
    //     transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);

    //     // Optional: Face the direction of movement
    //     Vector2 direction = patrolPoints[targetPoint].position - transform.position;
    //     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //     transform.rotation = Quaternion.Euler(Vector3.forward * angle);

    //     // Check if the enemy reached the current patrol point
    //     if (Vector3.Distance(transform.position, patrolPoints[targetPoint].position) < 0.1f)
    //     {
    //         IncreaseTargetPoint();
    //     }
    // }

    // void IncreaseTargetPoint()
    // {
    //     targetPoint++;
    //     if (targetPoint > patrolPoints.Length)
    //     {
    //         targetPoint = 0;
    //     }
    // }
}