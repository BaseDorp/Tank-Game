using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTank : Tank
{
    [SerializeField]
    protected int bullets = 3;
    [SerializeField]
    protected float reloadTime = 3f;
    [SerializeField]
    protected float sightDistance = 100f;

    protected Vector3 closestPlayer; // TODO might have to be private so that each tank has their own closestPlayer

    [SerializeField]
    protected Sprite QuestionMark;
    [SerializeField]
    protected Sprite ExclamationPoint;
    [SerializeField]
    protected SpriteRenderer ThinkingSpriteRenderer;

    void Start()
    { 
        closestPlayer = new Vector3(100, 100, 100);   
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

            if (Physics.Raycast(Raycast, out hitInfo, sightDistance))
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
                    //ThinkingSpriteRenderer.sprite = ExclamationPoint; // TODO should probably move this SOC
                    this.Turret.LookAt(new Vector3(closestPlayer.x, this.transform.position.y, closestPlayer.z));
                    FireBullet(); //TODO uncommoent
                }
                else
                {
                    Debug.DrawLine(Raycast.origin, hitInfo.point, Color.white);
                }
            }
        }
    }

    protected virtual IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        bullets = 3;
    }

    public override void FireBullet()
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
