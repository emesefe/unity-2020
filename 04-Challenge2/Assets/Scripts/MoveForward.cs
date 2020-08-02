using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 20.0f;
    
    void Update()
    {
        transform.Translate (speed * Time.deltaTime * Vector3.forward);
    }
}
