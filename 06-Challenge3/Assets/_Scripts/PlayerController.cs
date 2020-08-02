using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float jumpForce = 10.0f;
    private float gravityMultiplier = 0.05f;
    private float yRange = 14.0f;
    private Vector3 gravity = new Vector3(0, -9.81f, 0);
    
    public ParticleSystem explosion;
    public ParticleSystem fireworks;
    
    public AudioClip jumpSound, crashSound, getCoinSound;
    private float soundVolume = 1f;
    private AudioSource _audioSource, _audioSourceCrash;

    private GameManager gameManager;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        _audioSource = GetComponent<AudioSource>();
        _audioSourceCrash = explosion.gameObject.GetComponent<AudioSource>();
        
        gameManager = FindObjectOfType<GameManager>();
    }
    
    private void Update()
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Fuerza = masa * aceleración
                _rigidbody.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
                _audioSource.PlayOneShot(jumpSound, soundVolume);
            }

            // Si el jugador se sale por arriba
            if (transform.position.y > yRange)
            {
                _rigidbody.position = new Vector3(-_rigidbody.position.x, yRange, 0);
                _rigidbody.velocity = gravityMultiplier * new Vector3(0, -9.81f, 0);
            }
        }

        if (gameManager.gameState == GameManager.GameState.startGame)
        {
            Physics.gravity = Vector3.zero;
        }
        else
        {
            Physics.gravity = gravity;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                GameOverCrashAction();
            }

            if (other.gameObject.CompareTag("Coin"))
            {
                gameManager.UpdateScore(1);
                _audioSource.PlayOneShot(getCoinSound, soundVolume);
                fireworks.transform.position = other.transform.position;
                fireworks.Play();
                Destroy(other.gameObject);
            }

            if (other.gameObject.CompareTag("Bomb"))
            {
                GameOverCrashAction();

                _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
                this.gameObject.SetActive(false);
                Destroy(other.gameObject);
            }
        }
    }

    /// <summary>
    /// Realiza las acciones comunes a cualquier muerte por choque:
    /// * _gameOver = true
    /// * Copia la posición del Player al Sistema de Partículas explosion
    /// * Ejecuta el Sistema de Partículas explosion
    /// * Ejecuta el sonido de choque
    /// </summary>
    private void GameOverCrashAction()
    {
        explosion.transform.position = _rigidbody.position;
        explosion.Play();
        _audioSourceCrash.PlayOneShot(crashSound, soundVolume);
        gameManager.GameOver();
    }
}
