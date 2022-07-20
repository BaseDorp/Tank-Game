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
        navAgents = FindObjectsOfType(typeof(NavMeshAgent)) as NavMeshAgent[];
        closestPlayer = new Vector3(100, 100, 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (bullets > 0)
        {
            Aim();
        }
        else
        {
           StartCoroutine(Reload());
        }

        this.elapsedTime += Time.deltaTime;


        UpdateTarget();

        // Stop targeting the player if game over
        if (!Gamemode.Instance.CheckPlayerAlive())
        {
            this.enabled = false;
            this.GetComponent<NavMeshAgent>().enabled = false;
        }
    }

    protected override void Aim()
    {
        float distanceFromPlayer = 0;

        for (int i = 0; i < Gamemode.Instance.Players.Count; i++)
        {
            Vector3 rayDir = Gamemode.Instance.Players[i].transform.position - this.transform.position;
            Ray Raycast = new Ray(this.transform.position, rayDir);
            RaycastHit hitInfo;

            if (Physics.Raycast(Raycast, out hitInfo, sightDistance))
            {
                if (hitInfo.collider.GetComponent<PlayerTank>())
                {
                    Debug.DrawLine(Raycast.origin, hitInfo.point, Color.blue);

                    if (hitInfo.distance < distanceFromPlayer || distanceFromPlayer == 0f)
                    {
                        distanceFromPlayer = hitInfo.distance;
                        closestPlayer = Gamemode.Instance.Players[i].transform.position;
                    }

                    // Update the last seen location of that tank
                    Gamemode.Instance.Players[i].UpdateLastKnownLocation();

                    // TODO look at current player position - previous player position 
                    
                    this.Turret.LookAt(new Vector3(closestPlayer.x, this.transform.position.y, closestPlayer.z));
                    FireBullet();
                }
                else
                {
                    Debug.DrawLine(Raycast.origin, hitInfo.point, Color.white);
                }
            }
        }

        // TODO incorporate lastknownlocations with finding closest player
        // line of sight just updates last known location
        // ai's closest target is the closest last known location
        // only fires if the player is in line of sight (raycast)

        foreach (PlayerTank player in Gamemode.Instance.Players)
        {
            distanceFromPlayer = Vector3.Distance(player.lastKnownLocation, this.transform.position);

            if (distanceFromPlayer < Vector3.Distance(closestPlayer, this.transform.position) && player.lastKnownLocation != Vector3.zero)
            {
                closestPlayer = player.transform.position;
            }
        }

        if (distanceFromPlayer < 5) // Resetting the closest player so it doesnt keep comparing it to the same location // TODO what??
        {
            closestPlayer = new Vector3(100, 100, 100);
        }

        // Stretch. Have the AI "lock on" to a target. This way it isnt constitally switching between targets
        // TODO have players in line of sight more of a priority than closest one that isnt in line of sight
        //TODO AI will try to go after a player that it might not have the ability to get to
        // aka judge closest player by route and if it is even possible to reach them not by raycast distance
    }

    void UpdateTarget()
    {
        // Only update destination if there is a destination
        if (closestPlayer != new Vector3(100,100,100))
        {
            foreach (NavMeshAgent agent in navAgents)
            {
                agent.destination = closestPlayer;
            }
        }
    }

    protected override IEnumerator Reload()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        yield return new WaitForSeconds(reloadTime);
        bullets = 3;
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
    }

    private void OnDisable()
    {
        GetComponent<NavMeshAgent>().enabled = false;
    }
}
