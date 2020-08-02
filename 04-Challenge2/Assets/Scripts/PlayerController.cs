using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;

    private float counter = 2.0f;
    private float minTimeBetweenShooting = 1.0f;

    private void Update()
    {
        counter += Time.deltaTime;
        if (counter > minTimeBetweenShooting)
        {
            // Acciones del personaje
            if (Input.GetKeyDown(KeyCode.Space))
            {
                counter = 0;
                // Si entramos aquí, hay que lanzar un proyectil
                Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            }
        }
    }
}
