using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidthCollider;

    private void Start()
    {
        startPos = transform.position;
        repeatWidthCollider = GetComponent<BoxCollider>().size.x / 2;
    }

    private void Update()
    {
        // Hay que tener muy en cuenta que nos estamos moviendo a la izquierda
        if (startPos.x - transform.position.x > repeatWidthCollider)
        {
            transform.position = startPos;
        }
    }
}
