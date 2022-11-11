using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        PoisonLogic.isPoisoning = true;
    }

    void OnTriggerExit(Collider other)
    {
        PoisonLogic.isPoisoning = false;
    }
}
