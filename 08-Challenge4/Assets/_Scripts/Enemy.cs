using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float moveForce = 0.005f;
    private GameObject playerGoal;

    public ParticleSystem explosionPrefab;

    private GameManager gameManager;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerGoal = GameObject.Find("Player Goal");

        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            Vector3 lookDirection = (playerGoal.transform.position - this.transform.position).normalized;
            _rigidbody.AddForce(moveForce * lookDirection, ForceMode.Force);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            EnemyInGoal();
            if (collision.gameObject == playerGoal)
            {
                gameManager.GameOver();
            }
            else
            {
                gameManager.UpdateScore(1);
            }
        }
    }

    /// <summary>
    /// Realiza las acciones comunes cuando el enemigo llega a alguna de las porterías
    /// </summary>
    private void EnemyInGoal()
    {
        Destroy(gameObject);
        Instantiate(explosionPrefab, _rigidbody.position, explosionPrefab.transform.rotation);
    }
}
