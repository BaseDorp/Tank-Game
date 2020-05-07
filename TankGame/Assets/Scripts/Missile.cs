using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Missile : MovingTank
{
    [SerializeField]
    float missileSpeed;

    private NavMeshAgent[] navAgents;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<NavMeshAgent>().speed = missileSpeed;
        player1Transform = GameObject.FindGameObjectWithTag("Player1").transform;
        player1LastLoc = this.transform.position;

        navAgents = FindObjectsOfType(typeof(NavMeshAgent)) as NavMeshAgent[];
    }

    // Update is called once per frame
    void Update()
    {
        foreach (NavMeshAgent agent in navAgents)
        {
            agent.destination = player1LastLoc;
        }
    }


}
