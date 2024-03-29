﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTank : AiTank
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        shootRate = 3; // TODO should be a serialized field
    }

    // Update is called once per frame
    void Update()
    {
        if (bullets > 0)
        {
            Aim();
        }
        else
        {
            StartCoroutine(Reload());
        }

        // Time since last bullet fired
        this.elapsedTime += Time.deltaTime;
    }

    public override void FireBullet()
    {
        if (this.elapsedTime >= this.shootRate && bullets > 0)
        {
            //Reset the time
            this.elapsedTime = 0.0f;

            BulletManager.Instance.SpawnFromPool("missile", this.bulletSpawnPoint.position, this.bulletSpawnPoint.rotation, closestPlayer);
            bullets--;
        }
    }
}
