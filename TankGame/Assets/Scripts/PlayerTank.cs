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
        turretRotSpeed = 100.0f;
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

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        controller.Move(input * this.movementSpeed * Time.deltaTime);

        // Moving and Rotating
        //if (horizontalValue > 0)
        //{
        //    MoveRight();
        //}
        //if (horizontalValue < 0)
        //{
        //    MoveLeft();
        //}
        //if (verticalValue > 0)
        //{
        //    MoveUp();
        //}
        //else if (verticalValue < 0)
        //{
        //    MoveDown();
        //}

        if (Input.GetKey(KeyCode.RightArrow))
        {
            TurnRight();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            TurnLeft();
        }

        //// Find current speed
        //currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 7.0f * Time.deltaTime);
        //transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed, Space.World);
    }
}
