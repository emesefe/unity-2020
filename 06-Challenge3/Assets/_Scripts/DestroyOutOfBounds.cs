using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float leftBound = -10.0f;
    
    void Update()
    {
        if (this.transform.position.x < leftBound)
        {
            Destroy(this.gameObject);
        }
    }
}
