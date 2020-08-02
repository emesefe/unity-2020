using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{
    [Range(0, 10)]
    public int points;
    public GameObject floatingText;
    
    private GameManager gameManager;

    public ParticleSystem explosion;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine("DestroyTarget");
    }

    private void OnMouseDown()
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            gameManager.targetPositions.Remove(transform.position);
            gameManager.UpdateScore(points);
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            Destroy(gameObject);

            if (points > 0)
            {
                GameObject text = Instantiate(floatingText, transform.position, 
                    floatingText.transform.rotation);
                text.GetComponentInChildren<TextMeshProUGUI>().text = points.ToString();
            }

            if (gameObject.CompareTag("Bad"))
            {
                gameManager.GameOver();
            }

            if (gameObject.CompareTag("Power Up"))
            {
                gameManager.PowerUpTime(transform.position);
                GameObject text = Instantiate(floatingText, transform.position, 
                    floatingText.transform.rotation);
                text.GetComponentInChildren<TextMeshProUGUI>().text = "+5";
                text.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
            }
        }
    }

    /// <summary>
    /// Destruye el target tras el tiempo establecido y realiza tantas
    /// disminuciones de escala al Game Object como indiquemos por parámetro
    /// </summary>
    /// <param name = "numberOfReductionsInScale">Número de veces que reducimos la escala</param>
    /// <returns></returns>
    private IEnumerator DestroyTarget()
    {
        yield return new WaitForSeconds(gameManager.timeBeforeDisappear / 2);
        transform.localScale -= Vector3.one;
        yield return new WaitForSeconds(gameManager.timeBeforeDisappear / 2);
        Destroy(gameObject);
        gameManager.targetPositions.Remove(gameObject.transform.position);
    }
}
