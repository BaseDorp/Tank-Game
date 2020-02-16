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
}
