using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    
    private int _scoreCoins; // Contiene el valor
    private int scoreCoins // Maneja el valor
    {
        set
        {
            _scoreCoins = Mathf.Clamp(value, 0, 99999);
        }

        get
        {
            return _scoreCoins;
        }
    }
    private const string TOTAL_CONS = "TOTAL_COINS";
    
    public GameObject titlePanel, inGamePanel, gameOverPanel;
    public TextMeshProUGUI scoreText;

    /// <summary>
    /// Empieza el juego
    /// </summary>
    public void StartGame()
    {
        gameState = GameState.inGame;
        titlePanel.SetActive(false);
        inGamePanel.SetActive(true);
        gameOverPanel.SetActive(false);

        scoreCoins = 0;
        UpdateScore(0);
    }

    /// <summary>
    /// Recarga el juego
    /// </summary>
    public void RestartGame()
    {
        gameState = GameState.startGame;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    ///  Finaliza el juego
    /// </summary>
    public void GameOver()
    {
        gameState = GameState.gameOver;
        gameOverPanel.SetActive(true);
    }
    
    /// <summary>
    /// Actualiza la puntuación y pinta el texto en la caja de texto
    /// </summary>
    /// <param name = "scoreToAdd">Puntos que se quieren añadir a la puntuación global</param>
    public void UpdateScore(int scoreToAdd)
    {
        scoreCoins += scoreToAdd;
        scoreText.text = "Coins:\n" + scoreCoins;
    }
}
