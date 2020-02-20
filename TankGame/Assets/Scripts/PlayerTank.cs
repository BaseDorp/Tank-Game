using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : Tank
{
    float horizontalValue = 0;
    float verticalValue = 0;
    
    [SerializeField]
    [Tooltip("How fast the tank speeds up and slows down")]
    float resistanceSpeed = 10;
    
    void Start()
    {
        // Tank Settings
        rotSpeed = 150.0f;

        // Get the turret
        Turret = gameObject.transform.GetChild(0).transform;
        bulletSpawnPoint = Turret.GetChild(0).transform;
    }
    
    void Update()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");

        // Moving and Rotating
        if (horizontalValue > 0)
        {
            TurnRight();
        }
        else if (horizontalValue < 0)
        {
            TurnLeft();
        }
        else
        {
            targetSpeed = 0;
        }

        if (verticalValue > 0)
        {
            MoveUp();
        }
        else if (verticalValue < 0)
        {
            MoveDown();
        }
        else
        {
            targetSpeed = 0;
        }

        // Firing
        if (Input.GetAxis("Fire1") == 1)
        {
            this.FireBullet();
        }

        // Find current speed
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, resistanceSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed);
    }


    
}
