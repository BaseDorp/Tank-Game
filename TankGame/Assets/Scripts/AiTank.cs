using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTank : Tank
{
    public static Transform player1Transform;
    protected static Vector3 player1LastLoc;

    Vector3 rayDir;
    Ray ray;
    RaycastHit hitInfo;

    // Start is called before the first frame update
    void Start()
    {
        // Setting default values
        this.movementSpeed = 3f;
        player1Transform = GameObject.FindGameObjectWithTag("Player1").transform;
        player1LastLoc = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Sight();

        // Time since last bullet fired
        this.elapsedTime += Time.deltaTime;
    }

    protected void Aim()
    {
        this.Turret.LookAt(player1LastLoc);
    }

    protected void Sight()
    {
        // Gets the direction of the player to the AI
        rayDir = player1Transform.transform.position - this.transform.position;
        // Sets that array to point at the player
        ray = new Ray(this.transform.position, this.rayDir);

        // TODO change distance so that it covers entire map
        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            if (hitInfo.collider.tag == "Player1")
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.blue);
                player1LastLoc = hitInfo.transform.position;
                FireBullet();
            }
            else
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.white);
            }
        }
    }
}
