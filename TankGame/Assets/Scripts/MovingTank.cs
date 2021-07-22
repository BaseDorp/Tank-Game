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
        gameObject.GetComponent<NavMeshAgent>().speed = movementSpeed;
        Player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerTank>();
        //player1LastLoc = this.transform.position;

        navAgents = FindObjectsOfType(typeof(NavMeshAgent)) as NavMeshAgent[];
    }

    // Update is called once per frame
    void Update()
    {
        if (bullets > 0 && bTankAlive)
        {
            Aim();
        }
        else
        {
           StartCoroutine(Reload());
        }

        // Time since last bullet fired
        this.elapsedTime += Time.deltaTime;


        UpdateTarget();
    }

    void UpdateTarget()
    {
        foreach(NavMeshAgent agent in navAgents)
        {
            //agent.destination = player1LastLoc;
        }
    }

    protected override IEnumerator Reload()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        yield return new WaitForSeconds(reloadTime);
        bullets = 3;
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
    }
}
