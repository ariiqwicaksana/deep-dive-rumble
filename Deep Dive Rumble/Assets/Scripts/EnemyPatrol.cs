using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    private Transform[] patrolPoints = new Transform[10]; // Maksimal 10 waypoint
    private int targetPoints;

    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            patrolPoints[i] = GameObject.FindGameObjectWithTag("Waypoint"+(i+1))?.transform;
        }
        targetPoints = 0; // Mulai dari waypoint pertama
    }

    void Update()
    {
        // Jika waypoint target tidak ada, keluar dari fungsi
        if (patrolPoints[targetPoints] == null) return;

        // Bergerak menuju waypoint target
        if (Vector3.Distance(transform.position, patrolPoints[targetPoints].position) < 0.1f)
        {
            increaseTargetInt();
        }

        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoints].position, speed * Time.deltaTime);
    }

    void increaseTargetInt()
    {
        targetPoints++;
        if (targetPoints >= patrolPoints.Length || patrolPoints[targetPoints] == null)
        {
            targetPoints = 0; // Kembali ke waypoint pertama
        }
    }
}
