using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        EntityManager entityManager =  World.DefaultGameObjectInjectionWorld.EntityManager;
        Entity entity = entityManager.CreateEntity(typeof(BulletComponent));

        entityManager.SetComponentData(entity, new BulletComponent { movementSpeed = 50 });
    }
}
