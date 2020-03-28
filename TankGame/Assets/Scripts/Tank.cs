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

    protected enum TankState
    {
        Attack,
        Patrol,
        Dead,
    }
    protected TankState tankState;
    
    protected float turretRotSpeed = 500.0f;
    protected float baseRotSpeed = 400.0f;
    protected float movementSpeed = 5.0f;
    protected Vector3 moveDirection;
    protected Vector3 input;
    
    // Bullet shotting rate
    protected float shootRate = 1f;
    protected float elapsedTime;

    void Start()
    {
        tankState = TankState.Patrol;
    }
    
    void Update()
    {
        
    }

    protected void FireBullet()
    {
        if (this.elapsedTime >= this.shootRate)
        {
            tankState = TankState.Attack;
            //Reset the time
            this.elapsedTime = 0.0f;
   
            BulletManager.Instance.SpawnFromPool("bullet", this.bulletSpawnPoint.position, this.bulletSpawnPoint.rotation);
        }
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

    // Rotation of the base of the tank based off of an Vector3
    protected void BaseRotation(Vector3 _lookRotation)
    {
        // Only update the rotation if the tank has moved
        if (_lookRotation != Vector3.zero)
        {
            this.Base.transform.rotation = Quaternion.RotateTowards(this.Base.rotation, Quaternion.LookRotation(_lookRotation), baseRotSpeed * Time.deltaTime);
        }
    }

    // Collision
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
