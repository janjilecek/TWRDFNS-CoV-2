using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour {
    public Wave[] waves;
    public Enemy enemy;

    public float range = 300.0f;
    int enemiesRemainingToSpawn;
    float nextSpawnTime;
    int enemiesRemAlive;
    Vector3 result;

    Wave currentWave;
    int currentWaveNumber;

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
        public float moveSpeed;
        
    }

    void NextWave()
    {
        currentWaveNumber++;
        if (currentWaveNumber-1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];
            enemiesRemainingToSpawn = currentWave.enemyCount;
            enemiesRemAlive = enemiesRemainingToSpawn;
        }   
    }

    public void Start()
    {
        NextWave();
        result = Vector3.zero;
    }

    public void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            Vector3 randomPoint = Vector2.zero + Random.insideUnitCircle * range;
      
            print(randomPoint);
            NavMeshHit hit;            
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                print(result);
            }

            Enemy spawned = Instantiate(enemy, RandomNavmeshLocation(300f), Quaternion.identity) as Enemy;
            spawned.OnDeath += onEnemyDeath;
            spawned.SetSpeed(currentWave.moveSpeed);
        }
    }

    void onEnemyDeath()
    {
        enemiesRemAlive--;
        if (enemiesRemAlive == 0)
        {
            NextWave();
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
