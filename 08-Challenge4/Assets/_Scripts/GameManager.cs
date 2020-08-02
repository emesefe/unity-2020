using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    
    public GameObject titlePanel, inGamePanel, gameOverPanel;
    public TextMeshProUGUI scoreText;
    
    private void Start()
    {
        ShowMaxScore();
    }
    
    /// <summary>
    /// Empieza el juego
    /// </summary>
    public void StartGame()
    {
        gameState = GameState.inGame;
        titlePanel.SetActive(false);
        inGamePanel.gameObject.SetActive(true);
        gameOverPanel.SetActive(false);
        
        score = 0;
        UpdateScore(0);
        
    }

    /// <summary>
    ///  Recarga la escena actual
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameState = GameState.gameOver;
        gameOverPanel.SetActive(true);
        
        SetMaxScore();
        ShowMaxScore();
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
}
