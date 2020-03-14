using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public Wave[] waves;
    public Enemy enemy;

    int enemiesRemainingToSpawn;
    float nextSpawnTime;

    Wave currentWave;
    int currentWaveNumber;

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
        
    }

    void NextWave()
    {
        currentWaveNumber++;
        currentWave = waves[currentWaveNumber - 1];
        enemiesRemainingToSpawn = currentWave.enemyCount;
    }

    public void Start()
    {
        NextWave();
    }

    public void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            Enemy spawned = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy;
            spawned.OnDeath += onEnemyDeath;
        }
    }

    void onEnemyDeath()
    {
        print("enemy died");
    } 
}
