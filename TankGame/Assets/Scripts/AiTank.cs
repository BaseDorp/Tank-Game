using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTank : Tank
{
    protected static PlayerTank Player1;
    public static List<Vector3> LastPlayerLocations;
    List<Ray> Raycasts;

    [SerializeField]
    protected int bullets = 3;
    [SerializeField]
    protected float reloadTime = 3f;

    Vector3 rayDir;
    Ray ray;
    RaycastHit hitInfo;

    // Start is called before the first frame update
    void Start()
    {
        // Makes the Raycast list the same size of number of players
        Raycasts = new List<Ray>(Gamemode.Instance.Players.Count);
        foreach (PlayerTank players in Gamemode.Instance.Players)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Raycasts.Count; i++) // TODO move to aim?
        {
            Vector3 rayDir = Gamemode.Instance.Players[i].transform.position - this.transform.position;
            Raycasts[i] = new Ray(this.transform.position, rayDir);
            RaycastHit hitInfo;

            // TODO change distance so that it covers entire map
            if (Physics.Raycast(Raycasts[i], out hitInfo, 100))
            {
                // Checks if the AI has line of sight to player
                if (hitInfo.collider.GetComponent<PlayerTank>())
                {
                    Debug.DrawLine(Raycasts[i].origin, hitInfo.point, Color.blue);
                    // If hit, update the last known location of that player
                    LastPlayerLocations[i] = this.transform.position;
                }
                else
                {
                    Debug.DrawLine(Raycasts[i].origin, hitInfo.point, Color.white);
                }
            }
        }
        // make AI go to closest player location // firing distance for ai tank?


        if (bullets > 0 && bTankAlive)
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

    protected void Aim()
    {
        // Gets the direction of the player to the AI
        rayDir = Player1.transform.position - this.transform.position;
        // Sets that array to point at the player
        ray = new Ray(this.transform.position, this.rayDir);

        // TODO change distance so that it covers entire map
        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            if (hitInfo.collider.GetComponent<PlayerTank>())
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.blue);
                LastPlayerLocations[0] = hitInfo.transform.position;
                // TODO look at current player position - previous player position 
                this.Turret.LookAt(new Vector3(LastPlayerLocations[0].x, this.transform.position.y, LastPlayerLocations[0].z));
                FireBullet();
            }
            else
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.white);
            }
        }
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
