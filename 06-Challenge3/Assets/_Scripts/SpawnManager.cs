using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject coinPrefab;

    private float spawnMinRangeY = 2.5f,
        spawnMaxRangeY = 15.0f;

    private float startDelay = 2.0f;
    private float startCoinDelay = 3.0f;
    private float repeatRate = 3.0f;
    
    private GameManager gameManager;
    
    private void Start()
    {
        InvokeRepeating("SpawnEnemyPrefab", startDelay, repeatRate);
        InvokeRepeating("SpawnCoinPrefab", startCoinDelay, repeatRate);

        gameManager = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// Genera una posición aleatoria en el eje Y
    /// </summary>
    /// <returns>Posición generada</returns>
    private Vector3 GenerateRandomPositionY()
    {
        float PosY = Random.Range(spawnMinRangeY, spawnMaxRangeY);
        Vector3 randomPos = new Vector3(25.0f, PosY, 0);
        return randomPos;
    }
    
    /// <summary>
    /// Spawnea al enemigo en una posición random del eje Y
    /// </summary>
    void SpawnEnemyPrefab()
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            Instantiate(enemyPrefab, GenerateRandomPositionY(), 
                enemyPrefab.transform.rotation);
        }  
    }
    
    /// <summary>
    /// Spawnea monedas en una posisción random del eje Y
    /// </summary>
    void SpawnCoinPrefab()
    {
        if(gameManager.gameState == GameManager.GameState.inGame)
        {
            Instantiate(coinPrefab, GenerateRandomPositionY(), 
                coinPrefab.transform.rotation);
        }  
    }

}
