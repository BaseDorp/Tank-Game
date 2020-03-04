using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class Bullet : MonoBehaviour
{
    protected float Speed = 10;
    protected float LifeTime = 3.0f;
    protected int damage = 50;

    protected string lastColName;

    Ray ray;
    RaycastHit hitRay;
    LayerMask layerMask;
    bool yes = true;
    int wallBounce = 2;

    void Start()
    {
        //EntityManager entityManager =  World.DefaultGameObjectInjectionWorld.EntityManager;
        //Entity entity = entityManager.CreateEntity(typeof(BulletComponent));

        //entityManager.SetComponentData(entity, new BulletComponent { movementSpeed = 50 });

        Destroy(gameObject, LifeTime);
    }

    void Update()
    {
        this.transform.position += this.transform.forward * this.Speed * Time.deltaTime;

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
        //Instantiate(Explosion, contact.point, Quaternion.identity);
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
                Destroy(this.gameObject);
            }
        }
        if (collision.collider.tag == "Player" || collision.collider.tag == "bullet")
        {
            Destroy(this.gameObject);
        }
    }
}
