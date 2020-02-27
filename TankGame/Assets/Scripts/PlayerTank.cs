using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : Tank
{
    float horizontalValue = 0;
    float verticalValue = 0;
    
    void Start()
    {
        // Tank Settings
        rotSpeed = 150.0f;
    }
    
    void Update()
    {
        UpdateMovement();

        // Firing
        if (Input.GetAxis("Fire1") == 1)
        {
            this.FireBullet();
        }
        
    }

    void UpdateMovement()
    {
        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Moving and Rotating
        if (horizontalValue > 0)
        {
            MoveRight();
        }
        else if (horizontalValue < 0)
        {
            MoveLeft();
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

        if (Input.GetKey(KeyCode.RightArrow))
        {
            TurnRight();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            TurnLeft();
        }

        // Find current speed
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 7.0f * Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed, Space.World);
    }
}
