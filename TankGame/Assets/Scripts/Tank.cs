using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField]
    protected GameObject Bullet;
    [SerializeField]
    protected CharacterController controller;
    [SerializeField]
    protected Transform Turret;
    [SerializeField]
    protected Transform Base;
    [SerializeField]
    protected Transform bulletSpawnPoint;

    enum TankState
    {
        Attack,
        Patrol,
        Dead,
    }
    TankState tankState;
    
    protected float turretRotSpeed = 500.0f;
    protected float movementSpeed = 5.0f;
    protected Vector3 moveDirection;
    
    // Bullet shotting rate
    protected float shootRate = 1f;
    protected float elapsedTime;

    void Start()
    {
        tankState = TankState.Patrol;
    }
    
    void Update()
    {
        //// Find current speed
        //this.currentSpeed = Mathf.Lerp(this.currentSpeed, this.targetSpeed, 7.0f * Time.deltaTime);
        //this.transform.Translate(this.transform.forward * Time.deltaTime * this.currentSpeed, Space.World);

        //this.moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        //controller.Move(this.moveDirection * this.movementSpeed * Time.deltaTime);
    }

    protected void FireBullet()
    {
        elapsedTime += Time.deltaTime;
        if (this.elapsedTime >= this.shootRate)
        {
            tankState = TankState.Attack;
            //Reset the time
            this.elapsedTime = 0.0f;

            Instantiate(this.Bullet, this.bulletSpawnPoint.position, this.bulletSpawnPoint.rotation);
            Debug.Log($"{this.name} fired a bullet");
        }
    }

    protected void MoveUp()
    {
        this.Base.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }

    protected void MoveDown()
    {
        this.Base.transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
    }

    protected void MoveLeft()
    {
        this.Base.transform.rotation = new Quaternion(0f, 270, 0f, 0f);
    }

    protected void MoveRight()
    {
        this.Base.transform.rotation = new Quaternion(0f, 90f, 0f, 0f);
    }

    // Rotation
    protected void TurnRight()
    {
        this.Turret.transform.RotateAround(this.Base.transform.position, Vector3.up, this.turretRotSpeed * Time.deltaTime);
    }

    protected void TurnLeft()
    {
        this.Turret.transform.RotateAround(this.Base.transform.position, Vector3.down, this.turretRotSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Tank got shot
        if (collision.collider.tag == "bullet")
        {
            //this.enabled = false;
            tankState = TankState.Dead;
        }
    }
}
