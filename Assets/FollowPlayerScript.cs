using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, .5f, 0);
    public float speed;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMasterHandlerScript>().transform;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position+offset, speed * Time.deltaTime);
    }
}
