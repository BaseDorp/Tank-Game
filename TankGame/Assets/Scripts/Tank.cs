using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField]
    protected GameObject Bullet;
    [SerializeField]
    protected Transform Turret;
    [SerializeField]
    protected Transform Base;
    [SerializeField]
    protected Transform bulletSpawnPoint;

    protected float currentSpeed, targetSpeed, rotSpeed;
    protected float turretRotSpeed = 500.0f;
    protected float maxForwardSpeed = 5.0f;
    
    // Bullet shotting rate
    protected float shootRate = 0.5f;
    protected float elapsedTime;

    void Start()
    {

    }
    
    protected void Update()
    {
        // Find current speed
        this.currentSpeed = Mathf.Lerp(this.currentSpeed, this.targetSpeed, 7.0f * Time.deltaTime);
        this.transform.Translate(this.transform.forward * Time.deltaTime * this.currentSpeed, Space.World);

        Debug.Log(currentSpeed);
    }

    protected void FireBullet()
    {
        Instantiate(Bullet, bulletSpawnPoint);
        Debug.Log($"{this.name} fired a bullet");
    }

    protected void MoveUp()
    {
        this.Base.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        this.targetSpeed = maxForwardSpeed;
    }

    protected void MoveDown()
    {
        this.Base.transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        this.targetSpeed = -maxForwardSpeed;
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
}
