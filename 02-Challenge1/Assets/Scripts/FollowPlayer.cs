using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    
    private Vector3 offset = new Vector3(0, 10, -50);
    void Update()
    {
        transform.position = player.transform.TransformPoint(offset);
        transform.rotation = player.transform.rotation;
    }
}
