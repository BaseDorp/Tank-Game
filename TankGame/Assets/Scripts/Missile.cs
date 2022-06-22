using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Missile : MonoBehaviour
{
    [SerializeField]
    float missileSpeed;

    private NavMeshAgent[] navAgents;
    private Vector3 closestPlayer;

    void OnEnable()
    {
        // TODO find closest player
        closestPlayer = Gamemode.Instance.Players[0].transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<NavMeshAgent>().speed = missileSpeed;

        navAgents = FindObjectsOfType(typeof(NavMeshAgent)) as NavMeshAgent[];
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();

        foreach (NavMeshAgent agent in navAgents)
        {
            agent.destination = closestPlayer;
            Debug.Log(agent.destination);
        }
    }

    void Rotate()
    {
        if (closestPlayer != Vector3.zero)
        {
            this.transform.LookAt(closestPlayer);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Removes object if colliding tank or another bullet
        if (collision.collider.tag == "Player" || collision.collider.tag == "bullet" || collision.collider.tag == "tank" || collision.collider.tag == "Wall")
        {
            //this.gameObject.SetActive(false);
        }
    }

}
