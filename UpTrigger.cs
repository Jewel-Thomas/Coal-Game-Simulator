using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject parent;
    public CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Upper")
        {
            player.transform.parent = parent.transform;
            characterController.enabled = true;
        }
    }
}
