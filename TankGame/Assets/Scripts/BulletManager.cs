using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Code from Brackeys Object Pooling youtube tutorial
/// https://www.youtube.com/watch?v=tdSmKaJvCoA
/// </summary>

public class BulletManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int poolSize;
    }

    public Dictionary<string, Queue<GameObject>> bulletPool;
    public List<Pool> pools;

    // Singleton
    public static BulletManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        bulletPool = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject gameObject = Instantiate(pool.prefab);
                gameObject.SetActive(false);
                objectPool.Enqueue(gameObject);
            }

            bulletPool.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string _tag, Vector3 _pos, Quaternion _rot)
    {
        if (!bulletPool.ContainsKey(_tag))
        {
            Debug.LogWarning("Tag does not match object pool");
            return null;
        }

        GameObject spawnObject = bulletPool[_tag].Dequeue();
        spawnObject.SetActive(true);
        spawnObject.transform.position = _pos;
        spawnObject.transform.rotation = _rot;

        bulletPool[_tag].Enqueue(spawnObject);

        return spawnObject;
    }
}
