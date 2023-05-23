using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy1; 
    public GameObject enemy2;
    public GameObject enemy3;
    public float spawnRate = 10f;
    public float spawnRadius = 0f;

    int enemy1Counter = 0;
    int enemy2Counter = 0;
    int enemy3Counter = 0;

    private float spawnTimer;      


    private void Update()
    {
        if (FindObjectOfType<GameController>().getShieldDown())
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnRate)
            {
                spawnTimer = 0f;

                Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;

                int randomIndex = Random.Range(1, 3);

                if (randomIndex == 1)
                {
                    GameObject spawnedEnemy = Instantiate(enemy1, spawnPosition, Quaternion.identity);
                    spawnedEnemy.tag = "Enemy";
                    enemy1Counter++;
                }
                else if (randomIndex == 2)
                {
                    GameObject spawnedEnemy = Instantiate(enemy2, spawnPosition, Quaternion.identity);
                    spawnedEnemy.tag = "Enemy";
                    enemy2Counter++;
                }
                else
                {
                    GameObject spawnedEnemy = Instantiate(enemy3, spawnPosition, Quaternion.identity);
                    spawnedEnemy.tag = "Enemy";
                    enemy3Counter++;
                }

            }
        }
        
    }
}
