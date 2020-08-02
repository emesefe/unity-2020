using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpin : MonoBehaviour
{
    public float rotationSpeed = 50.0f;
    private float horizontalInput;
    public GameObject player;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * horizontalInput);

            this.transform.position = player.transform.position;
        }
    }
}
