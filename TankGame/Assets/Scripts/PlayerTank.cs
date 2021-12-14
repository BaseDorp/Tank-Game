using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTank : Tank
{
    float gravity = 0;

    [SerializeField]
    protected CharacterController controller;
    [SerializeField]
    protected PlayerInput playerInput;
    [SerializeField]
    public Vector3 lastKnownLocation { get; private set; }

    private void Start()
    { 
        Gamemode.Instance.NewPlayer(this);
        lastKnownLocation = Vector3.zero;
    }

    void Update()
    {
        // Time since last bullet fired
        this.elapsedTime += Time.deltaTime;

        UpdateMovement();

        // Firing
//         if (Input.GetAxis("Fire1") == 1)
//         {
//             this.FireBullet();
//         }

        // Changes the Rotation of the base of the tank based of direction of movement
        //BaseRotation(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));
    }

//     public void OnMovement(InputAction.CallbackContext value)
//     {
//         Vector2 inputMovement = value.ReadValue<Vector2>();
//         rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
//     }

    public void UpdateInput(InputAction.CallbackContext value)
    {
        // Movement Input
        this.input = new Vector3(value.ReadValue<Vector2>().x, 0, value.ReadValue<Vector2>().y);
        

        // Aim Input
        

        // Firing Input

    }

    public void UpdateMovement()
    {
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
        controller.Move(input * this.movementSpeed * Time.deltaTime);
        this.BaseRotation(this.input);

        // player rotation
        //         if (Input.GetKey(KeyCode.RightArrow))
        //         {
        //             TurnRight();
        //         }
        //         if (Input.GetKey(KeyCode.LeftArrow))
        //         {
        //             TurnLeft();
        //         }
    }

    public void Aim(InputAction.CallbackContext value)
    {
        if (value.ReadValue<float>() > 0) // left
        {
            this.Turret.transform.RotateAround(this.Base.transform.position, Vector3.down, this.turretRotSpeed * Time.deltaTime);
        }
        else if (value.ReadValue<float>() < 0) // right
        {
            this.Turret.transform.RotateAround(this.Base.transform.position, Vector3.up, this.turretRotSpeed * Time.deltaTime);
        }
    }

    public void UpdateLastKnownLocation()
    {
        this.lastKnownLocation = this.transform.position;
    }

    public void SetColor(Color newColor)
    {
        if (bTankAlive)
        {
            this.baseRenderer.material.SetColor("_Color", newColor);
            this.turretRenderer.material.SetColor("_Color", newColor);
        }
    }
}
