using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTank : Tank
{
    float gravity = 0;

    [SerializeField]
    CharacterController controller;
    public PlayerInput playerInput;
    [SerializeField]
    public Vector3 lastKnownLocation { get; private set; }

    private float aimValue;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        Gamemode.Instance.NewPlayer(this);
        lastKnownLocation = Vector3.zero;
    }

    void Update()
    {
        // Time since last bullet fired
        this.elapsedTime += Time.deltaTime;

        UpdateMovement();
        Aim();
    }

    public void UpdateMovementInput(InputAction.CallbackContext value)
    {
        this.input = new Vector3(value.ReadValue<Vector2>().x, 0, value.ReadValue<Vector2>().y);
    }

    public void UpdateAimingInput(InputAction.CallbackContext value)
    {
        aimValue = value.ReadValue<float>();
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
    }

    public void Aim()
    {
        if (aimValue > 0) // left
        {
            this.Turret.transform.RotateAround(this.Base.transform.position, Vector3.down, this.turretRotSpeed * Time.deltaTime);
        }
        else if (aimValue < 0) // right
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

    public void ChangeInputDevice(int value)
    {
        playerInput.SwitchCurrentControlScheme(InputSystem.devices[value]);
    }
}
