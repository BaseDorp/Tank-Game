using JetBrains.Annotations;
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
    bool isAlive;

    private GameObject targetPlayer;

    [SerializeField]
    ParticleSystem explosionFX;
    [SerializeField]
    GameObject missileMesh;
    CapsuleCollider collider;


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

        collider = this.gameObject.GetComponent<CapsuleCollider>();

        currentSpeed = 1;
        currentSpeed = startSpeed;
        isAlive = true;
        collider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Rotate();

            if (timeAlive >= lifeTime)
            {
                explosionFX.Play();
                isAlive = false;
                collider.enabled= false;
                missileMesh.SetActive(false);
            }
            timeAlive += Time.deltaTime;

        }
        else if (!explosionFX.isPlaying)
        {
            this.gameObject.SetActive(false);
        }

    }

    private void FixedUpdate()
    {
        if (isAlive)
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
    }

    // Lerps the current rotation to start rotating towards the target
    void Rotate()
    {
        Vector3 direction = targetPlayer.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        transform.eulerAngles= new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private void OnCollisionEnter(Collision other)
    {
        // Removes object if colliding tank or another bullet
        if (other.collider.tag == "Player" || other.collider.tag == "bullet" || other.collider.tag == "tank" || other.collider.tag == "Wall" && isAlive)
        {
            explosionFX.Play();
            isAlive = false;
            missileMesh.SetActive(false);
            collider.enabled = false;
        }
    }
}
