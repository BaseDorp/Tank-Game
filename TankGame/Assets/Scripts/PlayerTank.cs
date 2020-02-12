using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : Tank
{
    float horizontalValue = 0;
    float verticalValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void UpdateMovement()
    {
        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");

        if (horizontalValue > 0)
        {
            // Move Up
            Debug.Log("Move Up");
            this.tankRB.AddForce(this.tankTransform.forward * this.movementSpeed);
        }
        else if (horizontalValue < 0)
        {
            // Move Down
        }

        if (verticalValue > 0)
        {
            // Move Right
        }
        else if (verticalValue < 0)
        {
            // Move Left
        }
    }
}
