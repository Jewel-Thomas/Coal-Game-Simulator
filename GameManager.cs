using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image pointer;
    public StarterAssetsInputs starterAssetsInputs;
    public Camera playerCam;
    public Camera dozerCam;
    public bool isDriving = false;
    public GameObject bullDozer;
    public GameObject warningPanel;
    public GameObject lift;
    public GameObject door;
    public Animator liftAnim;
    public bool isLiftDown = false;
    public Animator doorAnim;
    public bool isDoorOpen = false;
    public CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && RayCastScript.itemCollected>=3)
        {
            DoorLogic();
        }
        else if(Input.GetKeyDown(KeyCode.T) && Vector3.Distance(door.transform.position,transform.position)<10)
        {
            warningPanel.SetActive(true);
            starterAssetsInputs.cursorLocked = false;
            starterAssetsInputs.SetCursorState(starterAssetsInputs.cursorLocked);
        }
        // if(Input.GetKeyDown(KeyCode.G) && RayCastScript.itemCollected>=3)
        // {
        //     LiftMove();
        // }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            TruckLogic();
        }
    }

    public void LockMouse()
    {
        starterAssetsInputs.cursorLocked = true;
        starterAssetsInputs.SetCursorState(starterAssetsInputs.cursorLocked);
    } 

    void TruckLogic()
    {
        if(!isDriving)
        {
            CarControllerScript.isActive = true;
            bullDozer.GetComponent<AudioSource>().enabled = true;
            characterController.enabled = false;
            dozerCam.enabled = true;
            playerCam.enabled = false;
            pointer.enabled = false;
            isDriving = !isDriving;
        }
        else
        {
            CarControllerScript.isActive = false;
            bullDozer.GetComponent<AudioSource>().enabled = false;
            characterController.enabled = true;
            playerCam.enabled = true;
            dozerCam.enabled = false;
            pointer.enabled = true;
            isDriving = !isDriving;
        }

    }

    void DoorLogic()
    {
        if(!isDoorOpen && Vector3.Distance(door.transform.position,transform.position)<10)
        {
            doorAnim.SetTrigger("DoorOpen");
            isDoorOpen = !isDoorOpen;
        }
        else if(isDoorOpen && Vector3.Distance(door.transform.position,transform.position)<10)
        {
            doorAnim.SetTrigger("DoorClose");
            isDoorOpen = !isDoorOpen;
        }
    }

    public void LiftMove()
    {
        characterController.enabled = false;
        transform.parent = lift.transform;
        if(!isLiftDown)
        {
            liftAnim.SetTrigger("LiftDown");
            isLiftDown = !isLiftDown;
        }
        else
        {
            liftAnim.SetTrigger("LiftUp");
            isLiftDown = !isLiftDown;
        }
    }
}
