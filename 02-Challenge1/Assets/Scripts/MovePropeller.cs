using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePropeller : MonoBehaviour
{
    private float turnSpeed = 1000.0f;
    
    void Update()
    {
        transform.Rotate(turnSpeed * Time.deltaTime * Vector3.forward);
    }
}
