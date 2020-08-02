using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [Range (0, 30), SerializeField,
     Tooltip ("Velocidad lineal máxima del avión")] 
    private float speed = 20.0f;
    
    [Range (0, 90), SerializeField,
     Tooltip ("Velocidad de giro máxima del avión")]
    private float turnSpeed = 50.0f;

    private float horizontalInput, verticalInput;
    
    void Update()
    {
        horizontalInput = Input.GetAxis ("Horizontal");
        verticalInput = Input.GetAxis ("Vertical");
        
        /*
         Espacio = Velocidad * tiempo * dirección
         s = s0 + v * t * d
         La velocidad va en metros por segundo
         */
        
        transform.Translate (speed * Time.deltaTime * Vector3.forward);
        transform.Rotate(turnSpeed * Time.deltaTime * Vector3.up * horizontalInput);
        transform.Rotate(turnSpeed * Time.deltaTime * Vector3.right * verticalInput);
    }
}