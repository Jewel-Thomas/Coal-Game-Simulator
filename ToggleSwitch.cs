using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip torchOn;
    public AudioClip torchOff;
    public static bool isSwitchedOn = false;
    Light torch;
    // Start is called before the first frame update
    void Start()
    {
        torch = this.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && RayCastScript.isHelmetPicked)
        {
            if(!isSwitchedOn)
            {
                torch.enabled = true;
                audioSource.PlayOneShot(torchOn);
                isSwitchedOn = !isSwitchedOn;
            }
            else
            {
                torch.enabled = false;
                audioSource.PlayOneShot(torchOff);
                isSwitchedOn = !isSwitchedOn;
            }
        }
    }
}
