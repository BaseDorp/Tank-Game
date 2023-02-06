using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Missile : MonoBehaviour
{
    [SerializeField]
    float startSpeed;
    [SerializeField]
    float endSpeed;
    [SerializeField]
    float currentSpeed;
    [SerializeField]
    float lifeTime;
    [SerializeField]
    float endSpeedTime;

    float timeAlive;

    [SerializeField]
    NavMeshAgent navAgent;
    private NavMeshAgent[] navAgents;
    private GameObject targetPlayer;

    void OnEnable()
    {
        // By default, goes after player 1
        targetPlayer = Gamemode.Instance.Players[0].gameObject;
        // See if any other player is closer.
        foreach (PlayerTank player in Gamemode.Instance.Players)
        {
            float currentDistance = Vector3.Distance(targetPlayer.transform.position, this.transform.position);
            float otherDistance = Vector3.Distance(player.transform.position, this.transform.position);
            if (currentDistance > otherDistance)
            {
                targetPlayer = player.gameObject;
            }
        }

        currentSpeed = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        navAgent.speed = startSpeed;
        //gameObject.GetComponent<NavMeshAgent>().speed = startSpeed;
        currentSpeed = startSpeed;

        navAgents = FindObjectsOfType(typeof(NavMeshAgent)) as NavMeshAgent[];
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();

        navAgent.destination = targetPlayer.transform.position;

        if (timeAlive >= lifeTime) 
        {
            this.gameObject.SetActive(false);        
        }
        timeAlive += Time.deltaTime;

        

        navAgent.speed = currentSpeed;        
    }

    private void FixedUpdate()
    {
        if (timeAlive >= endSpeedTime)
        {
            currentSpeed = endSpeed;
        }
        else
        {
            currentSpeed = endSpeed * (timeAlive / endSpeedTime);
        }

        Debug.Log(currentSpeed);
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
