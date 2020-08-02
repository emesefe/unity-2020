using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleSystem : MonoBehaviour
{
    private float timeBeforeDestruction = 10f;

    private void Start()
    {
        StartCoroutine("DestroyParticles");
    }

    private IEnumerator DestroyParticles()
    {
        yield return new WaitForSeconds(timeBeforeDestruction);
        Destroy(gameObject);
    }
}
