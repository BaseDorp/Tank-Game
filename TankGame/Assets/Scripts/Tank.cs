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
    [SerializeField]
    protected Renderer baseRenderer;
    [SerializeField]
    protected Renderer turretRenderer;
    [SerializeField]
    protected ParticleSystem explosionFX;
    [SerializeField]
    private ParticleSystem smokeFX;

    // Tank Movement
    [SerializeField]
    protected float turretRotSpeed = 150.0f;
    protected float baseRotSpeed = 400.0f;
    [SerializeField]
    protected float movementSpeed = 3.0f;
    protected Vector3 moveDirection;
    protected Vector3 input;
    
    // Bullet shotting rate
    protected float shootRate = 1f;
    protected float elapsedTime;

    // Tank Attributes
    public bool bTankAlive = true;
    public enum TankColor { Blue, Green, Red, Yellow };
    public TankColor tankColor;

    public virtual void FireBullet()
    {
        //Debug.Log(""); // TODO mouse click doesnt work with input system

        if (this.elapsedTime >= this.shootRate)
        {
            //Reset the time
            this.elapsedTime = 0.0f;
   
            BulletManager.Instance.SpawnFromPool("bullet", this.bulletSpawnPoint.position, this.bulletSpawnPoint.rotation);
            //fireVFX.Play();
        }
    }

    // Rotation
//     protected void TurnRight()
//     {
//         this.Turret.transform.RotateAround(this.Base.transform.position, Vector3.up, this.turretRotSpeed * Time.deltaTime);
//     }
// 
//     protected void TurnLeft()
//     {
//         this.Turret.transform.RotateAround(this.Base.transform.position, Vector3.down, this.turretRotSpeed * Time.deltaTime);
//     }

    // Rotation of the base of the tank based off of an Vector3
    protected void BaseRotation(Vector3 _lookRotation)
    {
        // Only update the rotation if the tank has moved
        if (_lookRotation != Vector3.zero)
        {
            this.Base.transform.rotation = Quaternion.RotateTowards(this.Base.rotation, Quaternion.LookRotation(_lookRotation), baseRotSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Tank shot
        if (collision.collider.tag == "bullet")
        {
            explosionFX.Play();
            smokeFX.Play();
            //this.enabled = false;
            //this.bTankAlive = false;
            this.baseRenderer.material.SetColor("_Color", Color.gray);
            this.turretRenderer.material.SetColor("_Color", Color.gray);
        }
    }
}
