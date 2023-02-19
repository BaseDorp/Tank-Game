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
    [SerializeField]
    float rotationSpeed;

    float timeAlive;

    private GameObject targetPlayer;

    [SerializeField]
    ParticleSystem explosionFX;


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
        currentSpeed = startSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();

        if (timeAlive >= lifeTime) 
        {
            this.gameObject.SetActive(false);        
        }
        timeAlive += Time.deltaTime;      
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

        transform.position += transform.forward * currentSpeed * Time.deltaTime;
    }

    // Lerps the current rotation to start rotating towards the target
    void Rotate()
    {
        Vector3 direction = targetPlayer.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        transform.eulerAngles= new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Removes object if colliding tank or another bullet
        if (other.tag == "Player" || other.tag == "bullet" || other.tag == "tank" || other.tag == "Wall")
        {
            explosionFX.Play();
            //float particleLifetime = explosionFX.main explosionFX.main.duration + explosionFX.main.startLifetime;

            //Invoke("DeactivateSelf", particleLifetime);
        }
    }

    private void DeactivateSelf()
    {
        this.gameObject.SetActive(false);
    }
}
