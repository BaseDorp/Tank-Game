using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{

    public GameObject Bullet;

    protected Transform Turret;
    protected Transform bulletSpawnPoint;
    protected float currentSpeed, targetSpeed, rotSpeed;
    protected float turretRotSpeed = 5.0f;
    protected float maxForwardSpeed = 5.0f;
    
    // Bullet shotting rate
    protected float shootRate = 0.5f;
    protected float elapsedTime;

    void Start()
    {

    }
    
    void Update()
    {
        
    }

    protected void FireBullet()
    {
        Instantiate(Bullet, bulletSpawnPoint);
        Debug.Log($"{this.name} fired a bullet");
    }

    protected void MoveUp()
    {
        this.targetSpeed = maxForwardSpeed;
    }

    protected void MoveDown()
    {
        this.targetSpeed = -maxForwardSpeed;
    }

    protected void TurnRight()
    {
        this.transform.Rotate(0, rotSpeed * Time.deltaTime, 0.0f);
    }

    protected void TurnLeft()
    {
        this.transform.Rotate(0, -rotSpeed * Time.deltaTime, 0.0f);
    }
}
