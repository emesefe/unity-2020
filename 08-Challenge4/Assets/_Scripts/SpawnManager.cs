using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    private float spawnRangeX = 19.0f,
        spawnRangeMinZ = 12.5f,
        spawnRangeMaxZ = 17.0f;
    
    private int enemyCount;
    private int enemyWave = 1;
    
    public GameObject powerUpPrefab;

    private GameManager gameManager;
    
    private void Start()
    {
        SpawnEnemyWave(enemyWave);
        gameManager = FindObjectOfType<GameManager>();
    }
    
    private void Update()
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (enemyCount == 0)
            {
                enemyWave++;
                SpawnEnemyWave(enemyWave);
                Instantiate(powerUpPrefab, GenerateRandomPosition(0.5f),
                    powerUpPrefab.transform.rotation);
            }
        }
    }

    /// <summary>
    /// Genera una posición aleatoria, a no ser que se indique lo contrario
    /// </summary>
    /// <param name = "PosY">Posición en el eje Y</param>>
    /// <returns>Posición generada</returns>
    private Vector3 GenerateRandomPosition(float PosY = 0f)
    {
        float PosX = Random.Range(-spawnRangeX, spawnRangeX); 
        float PosZ = Random.Range(spawnRangeMinZ, spawnRangeMaxZ);
        
        Vector3 randomPos = new Vector3(PosX, PosY, PosZ);
        return randomPos;
    }

    /// <summary>
    /// Genera olas de un número determinado de enemigos en pantalla
    /// </summary>
    /// <param name = "numberEnemiesPerWave">Número de Enemigos que se crean en la ola (5 por defecto)</param>
    private void SpawnEnemyWave(int numberEnemiesPerWave = 5)
    {
        for (int i = 0; i < numberEnemiesPerWave; i++)
        {
            Instantiate(enemyPrefab, GenerateRandomPosition(),
                enemyPrefab.transform.rotation);
        }
    }
}
