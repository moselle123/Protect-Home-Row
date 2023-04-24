using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform oxygenSystem;
    public Transform engineSystem;
    public Transform batterySystem;
    public Transform autopilotSystem;
    public Transform shieldSystem;
    Transform[] systems;
    Transform target;

    public EnemyGFX enemy;

    public float speed = 100f;
    public float nextWaypointDistance = 0.3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        systems = new Transform[] {oxygenSystem, engineSystem, batterySystem, autopilotSystem, shieldSystem};
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        int randomIndex = Random.Range(0, systems.Length);

        target = systems[randomIndex]; 

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            enemy.setStopped();
            enemy.attack();
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (rb.velocity.x < 0)
        {
            enemy.setLeft();
        }
        else if (rb.velocity.x > 0)
        {
            enemy.setRight();
        }
    }
}
