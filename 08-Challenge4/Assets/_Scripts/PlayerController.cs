using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private float moveForce = 5.0f;
    private float shootForce = 5.0f;
    private float turboBoost = 10.0f;
    private Rigidbody _rigidbody;
    
    private GameObject focalPoint;
    
    public bool hasPowerUp;
    private float powerUpForce = 20.0f;
    private float powerUpTime = 10.0f;
    public GameObject powerUpIndicator;
    
    private int enemyCount;
    
    public GameObject startPos;

    public ParticleSystem powerUpParticle;
    public ParticleSystem smokeParticle;
    
    private GameManager gameManager;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");

        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            float forwardInput = Input.GetAxis("Vertical");

            // F = m * a
            _rigidbody.AddForce(focalPoint.transform.forward * moveForce * forwardInput,
                ForceMode.Force);

            powerUpIndicator.transform.position = this.transform.position + 0.5f * Vector3.down;
            
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (enemyCount == 0)
            {
                RestartPlayerPosition();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rigidbody.AddForce(focalPoint.transform.forward * turboBoost, ForceMode.Impulse);
                smokeParticle.Play();
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            if (other.CompareTag("Power Up"))
            {
                hasPowerUp = true;

                Instantiate(powerUpParticle, other.transform.position, transform.rotation);
                Destroy(other.gameObject);
                StartCoroutine(PowerUpCountDown());
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = collision.gameObject.transform.position - this.transform.position;
                
                if (hasPowerUp)
                {
                    enemyRigidbody.AddForce(awayFromPlayer * powerUpForce, ForceMode.Impulse);
                }
                else
                {
                    enemyRigidbody.AddForce(awayFromPlayer * shootForce, ForceMode.Impulse);
                }
            }
        }
    }

    IEnumerator PowerUpCountDown()
    {
        powerUpIndicator.gameObject.SetActive(true);
        yield return new WaitForSeconds(powerUpTime);
        
        powerUpIndicator.gameObject.SetActive(false);
        hasPowerUp = false;
    }

    /// <summary>
    /// Devuelve al jugador a la posición y rotación inciales
    /// </summary>
    private void RestartPlayerPosition()
    {
        transform.position = startPos.transform.position;
        focalPoint.transform.rotation = startPos.transform.rotation;
    }
}
