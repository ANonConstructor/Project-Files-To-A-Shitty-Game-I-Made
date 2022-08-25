using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;
    private Vector3Int offset = new Vector3Int(0,0,-10);

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
