using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTank : Tank
{
    public static Transform player1Transform;
    protected static Vector3 player1LastLoc;

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
        player1Transform = GameObject.FindGameObjectWithTag("Player1").transform;
        player1LastLoc = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (bullets <= 0)
        {
            StartCoroutine(Reload());
        }
        else
        {
            Aim();
            Sight();
        }

        // Time since last bullet fired
        this.elapsedTime += Time.deltaTime;
    }

    protected void Aim()
    {
        if (bullets > 0)
        {
            this.Turret.LookAt(new Vector3(player1LastLoc.x, this.transform.position.y, player1LastLoc.z));
        }
    }

    protected void Sight()
    {
        // Gets the direction of the player to the AI
        rayDir = player1Transform.transform.position - this.transform.position;
        // Sets that array to point at the player
        ray = new Ray(this.transform.position, this.rayDir);

        // TODO change distance so that it covers entire map
        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            if (hitInfo.collider.tag == "Player1")
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.blue);
                player1LastLoc = hitInfo.transform.position;
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
