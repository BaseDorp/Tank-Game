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

        // Time since last bullet fired
        this.elapsedTime += Time.deltaTime;


        UpdateTarget();
    }

    protected override void Aim()
    {
        float distanceFromPlayer = 0;

        for (int i = 0; i < Gamemode.Instance.Players.Count; i++)
        {
            Vector3 rayDir = Gamemode.Instance.Players[i].transform.position - this.transform.position;
            Ray Raycast = new Ray(this.transform.position, rayDir);
            RaycastHit hitInfo;

            // TODO change distance so that it covers entire map
            if (Physics.Raycast(Raycast, out hitInfo, 100))
            {
                // Checks if the AI has line of sight to player
                if (hitInfo.collider.GetComponent<PlayerTank>())
                {
                    Debug.DrawLine(Raycast.origin, hitInfo.point, Color.blue);

                    // Check which player is closest
                    if (hitInfo.distance < distanceFromPlayer || distanceFromPlayer == 0f)
                    {
                        distanceFromPlayer = hitInfo.distance;
                        closestPlayer = Gamemode.Instance.Players[i].transform.position;
                    }

                    // Update the last seen location of that tank
                    Gamemode.Instance.Players[i].UpdateLastKnownLocation();

                    // TODO look at current player position - previous player position 
                    this.Turret.LookAt(new Vector3(closestPlayer.x, this.transform.position.y, closestPlayer.z));
                    //FireBullet(); TODO uncommoent
                    Debug.Log("SEE PLAYER");
                }
                else
                {
                    Debug.DrawLine(Raycast.origin, hitInfo.point, Color.white);
                }
            }

            // If tank does not see any players directly // and player has to have a last known location
            if (distanceFromPlayer == 0 && Gamemode.Instance.Players[i].lastKnownLocation != Vector3.zero)
            {
                rayDir = Gamemode.Instance.Players[i].lastKnownLocation - this.transform.position;
                // gets closest last known player location
                if (rayDir.magnitude < distanceFromPlayer)
                {
                    closestPlayer = Gamemode.Instance.Players[i].lastKnownLocation;
                    Debug.Log("GOING TO CLOSEST PLAYER");
                }
            }
        }
    }

    void UpdateTarget()
    {
        foreach(NavMeshAgent agent in navAgents)
        {
            agent.destination = closestPlayer;
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
