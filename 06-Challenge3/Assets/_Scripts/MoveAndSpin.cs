using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndSpin : MonoBehaviour
{
    public float turnSpeed = 100.0f;
    public float translateSpeed = 10.0f;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            transform.localPosition += translateSpeed * Time.deltaTime * Vector3.left;
        }
        transform.Rotate(turnSpeed * Time.deltaTime * Vector3.up);
    }
}
