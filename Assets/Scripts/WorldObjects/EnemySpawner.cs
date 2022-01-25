using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate = 4f;
    public int limit = 10;


    float nextSpawn = 0;

    public List<ObjectBaseData> datas;
    public WorldObject prefab;

    HashSet<WorldObject> spawned = new HashSet<WorldObject>();

    void Update()
    {
        if(Time.time >= nextSpawn)
        {
            if(nextSpawn > 0)
            {
                SpawnEnemy();
            }
            nextSpawn = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        spawned.RemoveWhere(x => !x);

        if (spawned.Count >= limit) return;

        float radiusX = transform.localScale.x / 2f;
        float radiusY = transform.localScale.y / 2f;

        var randomPos = new Vector3(Random.Range(-radiusX, radiusX), Random.Range(-radiusY, radiusY));
        var instance = Instantiate(prefab, transform.position + randomPos, Quaternion.identity);
        var randomData = datas[Random.Range(0, datas.Count - 1)];
        instance.Init(randomData);

        spawned.Add(instance);

        //instance.maxHealth = Random.Range(10, 50);
    }
}
