using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTank : Tank
{
    float horizontalValue = 0;
    float verticalValue = 0;
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

        //UpdateMovement();

        // Firing
        if (Input.GetAxis("Fire1") == 1)
        {
            this.FireBullet();
        }

        // Changes the Rotation of the base of the tank based of direction of movement
        BaseRotation(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));
    }

//     public void OnMovement(InputAction.CallbackContext value)
//     {
//         Vector2 inputMovement = value.ReadValue<Vector2>();
//         rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
//     }

    public void UpdateMovement(InputAction.CallbackContext value)
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

//     protected override void FireBullet()
//     {
//         base.FireBullet();
//     }

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
