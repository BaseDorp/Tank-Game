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

    void Start()
    {
        //EntityManager entityManager =  World.DefaultGameObjectInjectionWorld.EntityManager;
        //Entity entity = entityManager.CreateEntity(typeof(BulletComponent));

        //entityManager.SetComponentData(entity, new BulletComponent { movementSpeed = 50 });

        Destroy(gameObject, LifeTime);
    }

    void Update()
    {
        if (yes)
        {
            this.transform.position += this.transform.forward * this.Speed * Time.deltaTime;
        }
        else
        {
            this.transform.position -= this.transform.forward * this.Speed * Time.deltaTime;
        }


        //this.transform.Translate(Vector3.forward * Time.deltaTime * Speed, Space.Self);

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
            Debug.Log(this.name);
            // Make sure the bullet doesn't keep bouncing off into the same wall
            //if (this.lastColName != collision.collider.name)
            //{
            //    this.lastColName = collision.collider.name;


            //    //this.transform.rotation = Quaternion.Inverse(this.transform.rotation);
            //}


            Vector3 v = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            float rot = 90 - Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);

            //yes = !yes;
        }
    }
}
