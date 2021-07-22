using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : Tank
{
    float horizontalValue = 0;
    float verticalValue = 0;
    float gravity = 0;

    [SerializeField]
    protected CharacterController controller;

    private void Start()
    {
        Gamemode.Instance.Players.Add(this);
    }

    void Update()
    {
        // Time since last bullet fired
        this.elapsedTime += Time.deltaTime;

        UpdateMovement();

        // Firing
        if (Input.GetAxis("Fire1") == 1)
        {
            this.FireBullet();
        }

        // Changes the Rotation of the base of the tank based of direction of movement
        BaseRotation(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));
    }

    void UpdateMovement()
    {
        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");

        // Gravity
        if (controller.isGrounded)
        {
            gravity = 0;
        }
        else
        {
            gravity -= 10f * Time.deltaTime;
        }

        // player movement
        this.input = new Vector3(Input.GetAxis("Horizontal"), gravity, Input.GetAxis("Vertical"));
        controller.Move(input * this.movementSpeed * Time.deltaTime);

        // player rotation
        if (Input.GetKey(KeyCode.RightArrow))
        {
            TurnRight();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            TurnLeft();
        }
    }
}
