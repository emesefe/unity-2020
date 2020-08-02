using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float leftBound = -40.0f;
    private float lowerBound = -13.0f;

    private void Update()
    {
        // Si el objeto sale por la izquierda, se elimina
        if (this.transform.position.x < leftBound)
        {
            Destroy(this.gameObject);
        }
        
        // Si el objeto sale por abajo, se elimina y fin de la partida
        if (this.transform.position.y < lowerBound)
        {
            Debug.Log("GAME OVER");
            Destroy(this.gameObject);

            Time.timeScale = 0;
        }
    }

    
}
