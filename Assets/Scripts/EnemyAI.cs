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
    string targetName;

    public EnemyGFX enemy;

    public float speed = 50f;
    public float nextWaypointDistance = 0.3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool attacking = false;
    bool moving = true;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        setNewPath();
    }

    int setTarget()
    {
        List<int> brokenSystems = new List<int> { };
        if (!FindObjectOfType<GameController>().getOxygenDown())
        {
            brokenSystems.Add(0);
            Debug.Log(FindObjectOfType<GameController>().getOxygenDown());
        }
        if (!FindObjectOfType<GameController>().getEngineDown())
        {
            brokenSystems.Add(1);
            Debug.Log(FindObjectOfType<GameController>().getEngineDown());
        }
        if (!FindObjectOfType<GameController>().getBatteryDown())
        {
            brokenSystems.Add(2);
            Debug.Log(FindObjectOfType<GameController>().getBatteryDown());
        }
        if (!FindObjectOfType<GameController>().getAutopilotDown())
        {
            brokenSystems.Add(3);
            Debug.Log(FindObjectOfType<GameController>().getAutopilotDown());
        }
        if (FindObjectOfType<GameController>().getShieldDown())
        {
            brokenSystems.Add(4);
            Debug.Log(FindObjectOfType<GameController>().getShieldDown());
        }
        if (brokenSystems.Count == 0)
        {
            return 10;
        }
        else
        {
            int randomIndex = Random.Range(0, brokenSystems.Count);
            Debug.Log(brokenSystems.ToString());
            return brokenSystems[randomIndex];
        }
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

    void Update() {
        if(!reachedEndOfPath)
        {
            setPaths();
        }
        else
        {
            if (targetName == "autopilotSystem" && FindObjectOfType<GameController>().getAutopilotDown())
            {
                setNewPath();
            }
            else if (targetName == "engineSystem" && FindObjectOfType<GameController>().getEngineDown())
            {
                setNewPath();
            }
            else if (targetName == "shieldSystem" && FindObjectOfType<GameController>().getShieldDown())
            {
                setNewPath();
            }
            else if (targetName == "oxygenSystem" && FindObjectOfType<GameController>().getOxygenDown())
            {
                setNewPath();
            }
            else if (targetName == "batterySystem" && FindObjectOfType<GameController>().getBatteryDown())
            {
                setNewPath();
            }
            else
            {
                enemy.attack();
            }
        }
    }

    void setPaths()
    {
        if (path == null)
        {
            if (!attacking)
            {
                enemy.attack();
                attacking = true;
            }
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            enemy.setStopped();
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
            if (currentWaypoint == path.vectorPath.Count)
            {
                reachedEndOfPath = true;
            }
            else
            {
                currentWaypoint++;
            }
            
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

    void setNewPath()
    {
        systems = new Transform[] { oxygenSystem, engineSystem, batterySystem, autopilotSystem, shieldSystem };
        currentWaypoint = 0;
        

        if (setTarget() != 10)
        {
            target = systems[setTarget()];
        }
        else
        {
            target = shieldSystem;
        }

        if (target == shieldSystem)
        {
            targetName = "shieldSystem";
        }
        else if (target == oxygenSystem)
        {
            targetName = "oxygenSystem";
        }
        else if (target == engineSystem)
        {
            targetName = "engineSystem";
        }
        else if (target == batterySystem)
        {
            targetName = "batterySystem";
        }
        else
        {
            targetName = "autopilotSystem";
        }


        InvokeRepeating("UpdatePath", 0f, 0.5f);
        seeker.StartPath(rb.position, target.position, OnPathComplete);

        reachedEndOfPath = false;
        if (attacking)
        {
            attacking = false;
            enemy.stopAttack();
        }
        if (!moving)
        {
            moving = true;
            enemy.setMoving();
        }
        
    }
}