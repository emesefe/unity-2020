using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        startGame,
        inGame,
        gameOver
    }
    public GameState gameState;
    
    public List<GameObject> targetPrefabs;
    private float spawnRate = 2.0f;
    public float timeBeforeDisappear = 5.0f;
    public List<Vector3> targetPositions;
    private Vector3 randomPos;
    
    private int _score; // Contiene el valor
    private int score // Maneja el valor
    {
        set
        {
            _score = Mathf.Clamp(value, 0, 99999);
        }

        get
        {
            return _score;
        }
    }
    private const string MAX_SCORE = "MAX_SCORE";

    private float currentTime;
    private float startingTime = 60f;
    
    public GameObject titlePanel, inGamePanel, gameOverPanel;
    public TextMeshProUGUI scoreText, timeText;

    private float powerUpPlusTime = 5.0f;
    
    private void Start()
    {
        ShowMaxScore();
    }

    private void Update()
    {
        if (gameState == GameState.inGame)
        {
            currentTime = Mathf.Clamp(currentTime, 0, 60);
            currentTime -= 1 * Time.deltaTime;
            timeText.text = "TIME: " + currentTime.ToString("0");
            
            if (currentTime <= 0)
            {
                currentTime = 0;
                GameOver();
            }
        }
    }

    /// <summary>
    /// Inicia la partida cambiando el valor del estado del juego
    /// </summary>
    /// <param name = "difficulty">Dificultad del juego (easy: 1; medium: 2; difficult: 3) </param>
    public void StartGame(int difficulty)
    {
        gameState = GameState.inGame;
        titlePanel.SetActive(false);
        inGamePanel.gameObject.SetActive(true);
        gameOverPanel.SetActive(false);

        spawnRate /= difficulty;
        timeBeforeDisappear /= difficulty;
        
        currentTime = startingTime;
        
        StartCoroutine("SpawnTarget");
        
        score = 0;
        UpdateScore(0);
    }
    
    /// <summary>
    /// Gestiona el Game Over
    /// </summary>
    public void GameOver()
    {
        gameState = GameState.gameOver;
        gameOverPanel.SetActive(true);
        
        SetMaxScore();
        ShowMaxScore();
    }
    
    /// <summary>
    /// Recarga la escena actual
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Corrutina que spawnea un objeto aleatorio en una posición aleatoria
    /// </summary>
    private IEnumerator SpawnTarget()
    {
        while (gameState == GameState.inGame)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);
            randomPos = RandomSpawnPosition();
            while (targetPositions.Contains(randomPos))
            {
                randomPos = RandomSpawnPosition();
            }
            Instantiate(targetPrefabs[index], randomPos, targetPrefabs[index].transform.rotation);
            targetPositions.Add(randomPos);
        }
    }

    /// <summary>
    /// Genera un vector aleatorio en 3D
    /// </summary>
    /// <returns>Posición aleatoria en 3D, con la coordenada z = 0</returns>
    private Vector3 RandomSpawnPosition()
    {
        float posX = -3.75f + 2.5f * Random.Range(0, 4); 
        float posY = -3.75f + 2.5f * Random.Range(0, 4);
        
        return new Vector3(posX, posY, 0);
    }

    /// <summary>
    /// Actualiza la puntuación y pinta el texto en la caja de texto
    /// </summary>
    /// <param name = "scoreToAdd">Puntos que se quieren añadir a la puntuación global</param>
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "SCORE:\n" + score;
    }
    
    /// <summary>
    /// Muestra la puntuación máxima
    /// </summary>
    public void ShowMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0);
        scoreText.text = "Max Score:\n " + maxScore;
    }

    /// <summary>
    /// Guarda la puntuación máxima
    /// </summary>
    private void SetMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0);
        if (score > maxScore)
        {
            PlayerPrefs.SetInt(MAX_SCORE, score);    
        }        
    }

    /// <summary>
    /// Incrementa el tiempo de juego
    /// </summary>
    /// <param name="powerUpPos">Posición del powerUp</param>
    public void PowerUpTime(Vector3 powerUpPos)
    {
        currentTime += powerUpPlusTime;
    }
}
