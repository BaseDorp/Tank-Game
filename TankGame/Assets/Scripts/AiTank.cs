using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTank : Tank
{
    public static Transform player1Transform;
    //public Transform player1LastLoc;

    Vector3 rayDir;
    Ray ray;
    RaycastHit hitInfo;
    LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        this.movementSpeed = 3f;

        player1Transform = GameObject.FindGameObjectWithTag("Player1").transform;
    }

    // Update is called once per frame
    void Update()
    {
        FireBullet();
        Aim();
        Sight();

        // Time since last bullet fired
        this.elapsedTime += Time.deltaTime;

        

        
    }

    void Aim()
    {
        // Determines if the player is left or right of the AI
        // Code referenced from: https://forum.unity.com/threads/left-right-test-function.31420/
        //Vector3 heading = player1LastLoc.position - this.Turret.transform.position;
        //Vector3 perp = Vector3.Cross(this.Turret.transform.forward, heading);
        //float dir = Vector3.Dot(perp, this.Turret.transform.up);

        //// Moves the turret in the direction of the player
        //if (dir > 0f)
        //{
        //    this.TurnRight();
        //}
        //else if (dir < 0f)
        //{
        //    this.TurnLeft();
        //}
    }

    void Sight()
    {
        // Gets the direction of the player to the AI
        rayDir = player1Transform.transform.position - this.transform.position;
        // Sets that arrow to point at the player
        ray = new Ray(this.transform.position, this.rayDir);

        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            if (hitInfo.collider.tag == "Player1")
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.blue);
            }
            else
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.white);
            }
        }
    }
}
