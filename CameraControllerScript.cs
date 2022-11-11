using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    public Transform player;
    public Rigidbody playerRb;
    public Vector3 offset;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerForward = (playerRb.velocity + player.transform.forward).normalized;
        transform.position = Vector3.Lerp(transform.position,player.position + player.transform.TransformVector(offset)
         + playerForward * (-5f),speed*Time.deltaTime);
        transform.LookAt(player);
    }
}
