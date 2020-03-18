using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static List<Bullet> bulletList;

    public void EnableBullet(Bullet _bullet)
    {
        
    }

    public void DisableBullet(Bullet _bullet)
    {
        _bullet = null;
    }
}
