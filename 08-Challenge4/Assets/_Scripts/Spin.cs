using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    private float rotationSpeed = 100.0f;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            transform.Rotate(rotationSpeed * Vector3.up * Time.deltaTime);
        }   
    }
}
