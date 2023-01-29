using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Missile : MonoBehaviour
{
    [SerializeField]
    float missileSpeed;
    [SerializeField]
    float lifeTime;
    float timeAlive;

    private NavMeshAgent[] navAgents;
    private GameObject targetPlayer;

    void OnEnable()
    {
        // TODO find closest player
        targetPlayer = Gamemode.Instance.Players[0].gameObject;
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
            agent.destination = targetPlayer.transform.position;
            Debug.Log(agent.destination);
        }

        if (timeAlive >= lifeTime) 
        {
            this.gameObject.SetActive(false);        
        }
        timeAlive += Time.deltaTime;
    }

    void Rotate()
    {
        if (targetPlayer.transform.position != Vector3.zero)
        {
            this.transform.LookAt(targetPlayer.transform.position);
            transform.eulerAngles= new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Removes object if colliding tank or another bullet
        if (collision.collider.tag == "Player" || collision.collider.tag == "bullet" || collision.collider.tag == "tank" || collision.collider.tag == "Wall")
        {
            this.gameObject.SetActive(false);
        }
    }

}
