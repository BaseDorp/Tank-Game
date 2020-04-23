using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingTank : AiTank
{
    private NavMeshAgent[] navAgents;

    // Start is called before the first frame update
    void Start()
    {
        // Setting default values
        this.movementSpeed = 3f;
        player1Transform = GameObject.FindGameObjectWithTag("Player1").transform;
        player1LastLoc = new Vector3(0, 0, 0);

        navAgents = FindObjectsOfType(typeof(NavMeshAgent)) as NavMeshAgent[];
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Sight();

        // Time since last bullet fired
        this.elapsedTime += Time.deltaTime;


        UpdateTarget();
    }

    void UpdateTarget()
    {
        foreach(NavMeshAgent agent in navAgents)
        {
            agent.destination = player1LastLoc;
        }
    }

    void Move()
    {
        //this.transform.position = new Vector3.
    }
}
