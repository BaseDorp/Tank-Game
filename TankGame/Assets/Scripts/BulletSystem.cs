using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class BulletSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        // Runs on every entity with a BulletComponent
        Entities.ForEach((ref BulletComponent bulletComponent) =>
        {
            bulletComponent.movementSpeed += 1f * Time.DeltaTime;
        });
            
    }
}
