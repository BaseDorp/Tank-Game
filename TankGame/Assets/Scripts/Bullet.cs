using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float Speed = 10;
    protected float LifeTime = 5.0f;
    protected int damage = 50;

    protected string lastColName;

    Ray ray;
    RaycastHit hitRay;
    LayerMask layerMask;
    int wallBounce = 2;

    void OnEnabled()
    {
        // disables the object after a certain amount of time
        Invoke("Disable", LifeTime);
    }

    void Update()
    {
        // Movment
        this.transform.position += this.transform.forward * this.Speed * Time.deltaTime;

        // gets direction of wall bounce
        ray = new Ray(this.transform.position, this.transform.forward);
        if (Physics.Raycast(ray, out hitRay, Time.deltaTime * Speed + .1f, layerMask))
        {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hitRay.normal);
            float rotation = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rotation, 0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Removes the bullet if the bullet has collided with a wall more times than the wallBounce variable
        if (collision.collider.tag == "Wall")
        {
            if (this.wallBounce > 0)
            {
                this.wallBounce--;
                Vector3 v = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
                float rot = 90 - Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, rot, 0);
            }
            else
            {
                Disable();
            }
        }
        // Removes object if colliding tank or another bullet
        if (collision.collider.tag == "Player1" || collision.collider.tag == "Player2" || collision.collider.tag == "bullet" || collision.collider.tag == "tank")
        {
            Disable();
        }
    }

    // Removes the object from the scene
    void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
