using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTank : Tank
{
    [SerializeField]
    protected int bullets = 3;
    [SerializeField]
    protected float reloadTime = 3f;

    protected Vector3 closestPlayer; // TODO might have to be private so taht each tank has their own closestPlayer

    void Start()
    {
        
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

    protected virtual void Aim()
    {
        float distanceFromPlayer = 0;

        for (int i = 0; i < Gamemode.Instance.Players.Count; i++)
        {
            Vector3 rayDir = Gamemode.Instance.Players[i].transform.position - this.transform.position;
            Ray Raycast = new Ray(this.transform.position, rayDir);
            RaycastHit hitInfo;

            // TODO change distance so that it covers entire map
            if (Physics.Raycast(Raycast, out hitInfo, 100))
            {
                // Checks if the AI has line of sight to player
                if (hitInfo.collider.GetComponent<PlayerTank>())
                {
                    Debug.DrawLine(Raycast.origin, hitInfo.point, Color.blue);

                    // Check which player is closest
                    if (hitInfo.distance < distanceFromPlayer || distanceFromPlayer == 0f)
                    {
                        distanceFromPlayer = hitInfo.distance;
                        closestPlayer = Gamemode.Instance.Players[i].transform.position;
                    }

                    // Update the last seen location of that tank
                    Gamemode.Instance.Players[i].UpdateLastKnownLocation();

                    // TODO look at current player position - previous player position 
                    this.Turret.LookAt(new Vector3(closestPlayer.x, this.transform.position.y, closestPlayer.z));
                    //FireBullet(); TODO uncommoent
                }
                else
                {
                    Debug.DrawLine(Raycast.origin, hitInfo.point, Color.white);
                }
            }
        }

        // OLD CODE
        //// Gets the direction of the player to the AI
        //rayDir = Player1.transform.position - this.transform.position;
        //// Sets that array to point at the player
        //ray = new Ray(this.transform.position, this.rayDir);

        //// TODO change distance so that it covers entire map
        //if (Physics.Raycast(ray, out hitInfo, 100))
        //{
        //    if (hitInfo.collider.GetComponent<PlayerTank>())
        //    {
        //        Debug.DrawLine(ray.origin, hitInfo.point, Color.blue);
        //        LastPlayerLocations[0] = hitInfo.transform.position;
        //        // TODO look at current player position - previous player position 
        //        this.Turret.LookAt(new Vector3(LastPlayerLocations[0].x, this.transform.position.y, LastPlayerLocations[0].z));
        //        FireBullet();
        //    }
        //    else
        //    {
        //        Debug.DrawLine(ray.origin, hitInfo.point, Color.white);
        //    }
        //}
    }

    protected virtual IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        bullets = 3;
    }

    protected override void FireBullet()
    {
        if (this.elapsedTime >= this.shootRate && bullets > 0)
        {
            //Reset the time
            this.elapsedTime = 0.0f;

            BulletManager.Instance.SpawnFromPool("bullet", this.bulletSpawnPoint.position, this.bulletSpawnPoint.rotation);
            bullets--;
        }
    }
}
