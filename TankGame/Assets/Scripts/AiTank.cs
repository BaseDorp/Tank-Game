using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTank : Tank
{
    [SerializeField]
    public Transform playerTransform;

    public Transform lastPlayerLocation;

    // Start is called before the first frame update
    void Start()
    {
        this.movementSpeed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        FireBullet();
        Aim();

        UpdateMovement();
    }

    void Aim()
    {
        // Determines if the player is left or right of the AI
        // Code referenced from: https://forum.unity.com/threads/left-right-test-function.31420/
        Vector3 heading = playerTransform.position - this.Turret.transform.position;
        Vector3 perp = Vector3.Cross(this.Turret.transform.forward, heading);
        float dir = Vector3.Dot(perp, this.Turret.transform.up);

        // Moves the turret in the direction of the player
        if (dir > 0f)
        {
            this.TurnRight();
        }
        else if (dir < 0f)
        {
            this.TurnLeft();
        }
    }

    void UpdateMovement()
    {
        this.lastPlayerLocation = playerTransform;
        this.transform.position = Vector3.MoveTowards(this.transform.position, lastPlayerLocation.position, this.movementSpeed * Time.deltaTime);
    }
}
