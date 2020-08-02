using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    private int ballIndex;
    
    private float spawnRangeLeftX = -18.0f;
    private float spawnRangeRightX = 8.0f;
    
    private float spawnPosY;

    private float counter;
    private float nextWaitTime = 2.0f;
    
    /* Si queremos que el intervalo  entre spawns sea fijo, descomentar el siguiente bloque de código
    [SerializeField, Range(1, 5)]
    private float startDelay = 2.0f;
    [SerializeField, Range(0.5f, 3f)]
    private float spawnInterval = 1.5f;
    */

    private void Start()
    {
        // Guardamos la posición y donde está el Spawn Manager
        spawnPosY = this.transform.position.y;
        
        // Si queremos que el intervalo entre spawns sea fijo, descomentar la siguiente línea
        //InvokeRepeating("SpawnRandomBall", startDelay, spawnInterval);
    }

    void Update()
    {
        // Generamos intervalos de espera aleatorios
        counter += Time.deltaTime;
        if (counter > nextWaitTime)
        {
            SpawnRandomBall();
            Debug.LogFormat("Hemos esperado {0} segundos", nextWaitTime);
            counter = 0;
            nextWaitTime = Random.Range(2.0f, 5.0f);
        }
    }

    private void SpawnRandomBall()
    {
        // Generamos posición x donde va a aparecer el próximo enemigo
        float xRand = Random.Range(spawnRangeLeftX, spawnRangeRightX);
        Vector3 spawnPos = new Vector3(xRand, spawnPosY, 0);

        // Generamos índice aleatorio
        ballIndex = Random.Range(0, enemies.Length);
    
        // Instanciamos enemigo
        Instantiate(enemies[ballIndex],
            spawnPos,
            enemies[ballIndex].transform.rotation);
    }
}
