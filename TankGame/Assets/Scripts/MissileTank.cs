using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTank : AiTank
{
    [SerializeField]
    Missile missile;

    // Start is called before the first frame update
    void Start()
    {
        Player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerTank>();
        player1LastLoc = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (bullets > 0 && Player1.tankState == TankState.Alive)
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

    protected override void FireBullet()
    {
        if (this.elapsedTime >= this.shootRate && bullets > 0)
        {
            //Reset the time
            this.elapsedTime = 0.0f;

            BulletManager.Instance.SpawnFromPool("missile", this.bulletSpawnPoint.position, this.bulletSpawnPoint.rotation);
            bullets--;
        }
    }
}
