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
    public Vector3 lastKnownLocation { get; private set; } // TODO this should probably be in the AiTank class instead of the player

    private float aimValue;
    private Vector3 aim;
    bool mouseInputType;
    Color tankColor;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        Gamemode.Instance.NewPlayer(this);
        lastKnownLocation = Vector3.zero;
        SetColor(Color.blue);
        this.turretRotSpeed = 5f;
    }

    void Update()
    {
        // Time since last bullet fired
        this.elapsedTime += Time.deltaTime;

        if (Gamemode.Instance.CheckEnemyAlive())
        {
            UpdateMovement();
            Aim();
        }
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
        controller.Move(new Vector3(0, gravity, 0));
        this.BaseRotation(this.input);
    }

    public void Aim()
    {
        // check if the player is using keyboard or controller
        if (mouseInputType)
        {
            // get location of mouse on the screen
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                aim = new Vector3(hit.point.x, Turret.position.y, hit.point.z);
                //Turret.LookAt(new Vector3(hit.point.x, Turret.position.y, hit.point.z), Vector3.up);
                Vector3 targetDir = aim - Turret.position;
                this.Turret.rotation = Quaternion.LookRotation(Vector3.RotateTowards(Turret.forward, targetDir, turretRotSpeed * Time.deltaTime, 0.0f));
            }
        }
        else // TODO unnessary, different method is used for gamepad input
        {
            //// 2 value axis aim
            //if (aimValue > 0) // left
            //{
            //    this.Turret.transform.RotateAround(this.Base.transform.position, Vector3.down, this.turretRotSpeed * Time.deltaTime);
            //}
            //else if (aimValue < 0) // right
            //{
            //    this.Turret.transform.RotateAround(this.Base.transform.position, Vector3.up, this.turretRotSpeed * Time.deltaTime);
            //}
        }
    }

    // Sets the Turret rotation to match R-stick value for gamepads
    public void AimGamepad(InputAction.CallbackContext value)
    {
        aim = new Vector3(value.ReadValue<Vector2>().x, 0, value.ReadValue<Vector2>().y);
        if (aim == Vector3.zero) { return; }
        this.Turret.rotation = Quaternion.LookRotation(aim, Vector3.up);
        // TODO ??
        //this.Turret.rotation = Quaternion.LookRotation(Vector3.RotateTowards(Turret.position, aim, turretRotSpeed * Time.deltaTime, 0.0f));
    }

    public void UpdateLastKnownLocation()
    {
        this.lastKnownLocation = this.transform.position;
    }

    public void SetColor(Color newColor)
    {
        if (bTankAlive) //  TODO change this
        {
            tankColor = newColor;
            //baseRenderer.materials[1].SetColor("_Color", newColor);
            //baseRenderer.materials[0].SetColor("_Color", Color.black);
            //this.turretRenderer.materials[1].SetColor("_Color", newColor);
            //turretRenderer.materials[0].SetColor("_Color", Color.black);
            Debug.Log(baseRenderer.materials.Length);
        }
    }

    public void ChangeInputDevice(int value)
    {
        playerInput.SwitchCurrentControlScheme(InputSystem.devices[value]);

        // Checks if script should check for mouse input when updating Aim()
        if (playerInput.currentControlScheme == "Keyboard")
        {
            mouseInputType = true;
        }
        else
        {
            mouseInputType = false;
        }
    }

    private void OnEnable()
    {
        bTankAlive = true;
        SetColor(tankColor);        
        smokeFX.Stop();
    }
}
