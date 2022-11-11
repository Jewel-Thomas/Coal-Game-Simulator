using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RayCastScript : MonoBehaviour
{
    public GameObject barrels;
    public static bool isBigshowActive = false;
    public float timer = 120;
    public GameObject bigShow;
    public static bool isValveOpen = false;
    public static bool isHelmetPicked = false;
    public GameManager gameManager;
    public Animator switchAnim;
    public Animator valve;
    public GameObject stones;
    public ExplosionScript explosionScript;
    public Rigidbody explosionBody;
    public GameObject blockage;
    public TextMeshProUGUI helmetText;
    public TextMeshProUGUI hammerText;
    public TextMeshProUGUI gogglesText;
    public TextMeshProUGUI blockageText;
    public TextMeshProUGUI doorOpenText;
    public TextMeshProUGUI operateLiftText;
    public TextMeshProUGUI rotateValveText;
    public TextMeshProUGUI bigShowText;
    public TextMeshProUGUI timerText;
    public AudioSource audioSource;
    public AudioClip timedExplosion;
    public AudioClip liftSound;
    public AudioClip switchSound;
    public AudioClip valveSound;
    public AudioSource escapeTone;
    public static int itemCollected = 0;

    // Start is called before the first frame update
    void Start()
    {
        itemCollected = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        PickupHelmet();
        PickupHammer();
        PickupGoggles();
        PlaceDynamite();
        DoorOpen();
        OperateLift();
        RotateValve();
        BigShow();
        if(isBigshowActive)
        {
            Timer();
        }
        else if(!isBigshowActive)
        {
            if(escapeTone.volume>0)
            {
                escapeTone.volume -= Time.deltaTime/15;
            }
        }
    }

    void PickupHelmet()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward, out hit,10f))
        {
            if(hit.transform.gameObject.tag == "Helmet")
            {
                helmetText.enabled = true;
                hammerText.enabled = false;
                gogglesText.enabled = false;
                if(Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("Helmet Picked!");
                    hit.transform.gameObject.SetActive(false);
                    isHelmetPicked = true;
                    itemCollected++;
                }
                
            }
            else
            {
                helmetText.enabled = false;
            }
        }
        else
        {
            helmetText.enabled = false;
        }
    }

    void PickupHammer()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward, out hit,10f))
        {
            if(hit.transform.gameObject.tag == "Hammer")
            {
                helmetText.enabled = false;
                hammerText.enabled = true;
                gogglesText.enabled = false;
                if(Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("Hammer Picked!");
                    hit.transform.gameObject.SetActive(false);
                    itemCollected++;
                }
                
            }
            else
            {
                hammerText.enabled = false;
            }
            
        }
        else
        {
            hammerText.enabled = false;
        }
    }

    void PickupGoggles()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward, out hit,10f))
        {
            if(hit.transform.gameObject.tag == "Goggles")
            {
                helmetText.enabled = false;
                hammerText.enabled = false;
                gogglesText.enabled = true;
                if(Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("Goggles Picked!");
                    hit.transform.gameObject.SetActive(false);
                    itemCollected++;
                }
                
            }
            else
            {
                gogglesText.enabled = false;
            }
        }
        else
        {
            gogglesText.enabled = false;
        }
    }

    void PlaceDynamite()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward, out hit,10f))
        {
            if(hit.transform.gameObject.tag == "Blockage")
            {
                blockageText.enabled = true;
                if(Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("Dynamite Placed!");
                    hit.transform.GetChild(0).gameObject.SetActive(true);
                    audioSource.PlayOneShot(timedExplosion);
                    StartCoroutine(BlowUpDynamite());
                }
            }
            else
            {
                blockageText.enabled = false;
            }
        }
    }

    void DoorOpen()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward, out hit,10f))
        {
            if(hit.transform.gameObject.tag == "LiftDoor")
            {
                doorOpenText.enabled = true;
                helmetText.enabled = false;
                hammerText.enabled = false;
                gogglesText.enabled = false;
            }
            else
            {
                doorOpenText.enabled = false;
            }
        }
    }

    void OperateLift()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward, out hit,10f))
        {
            if(hit.transform.gameObject.tag == "Switch")
            {
                operateLiftText.enabled = true;
                helmetText.enabled = false;
                hammerText.enabled = false;
                gogglesText.enabled = false;
                if(Input.GetKeyDown(KeyCode.G) && itemCollected>=3)
                {
                    switchAnim.SetTrigger("SwitchPress");
                    gameManager.LiftMove();
                    audioSource.PlayOneShot(switchSound);
                    audioSource.PlayOneShot(liftSound);
                }
            }
            else
            {
                operateLiftText.enabled = false;
            }
        }
    }

    void RotateValve()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward, out hit,10f))
        {
            if(hit.transform.gameObject.tag == "Valve")
            {
                rotateValveText.enabled = true;
                if(Input.GetKeyDown(KeyCode.F))
                {
                    valve.SetTrigger("RotateValve");
                    audioSource.PlayOneShot(valveSound);
                    isValveOpen = true;
                }
            }
            else
            {
                rotateValveText.enabled = false;
            }
        }
        else
        {
            rotateValveText.enabled = false;
        }
    }

    void BigShow()
    {
        if(isValveOpen)
        {
            bigShow.SetActive(true);
        }
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward, out hit,10f))
        {
            if(hit.transform.gameObject.tag == "BigShow")
            {
                bigShowText.enabled = true;
                if(Input.GetKeyDown(KeyCode.F))
                {
                    escapeTone.Play();
                    isBigshowActive = true;
                    timerText.enabled = true;
                }
            }
            else
            {
                bigShowText.enabled = false;
            }
        }
        else
        {
            bigShowText.enabled = false;
        }
    } 

    void Timer()
    {
        timer = timer - Time.deltaTime;
        timerText.text = Mathf.Ceil(timer) + "s";
        if(escapeTone.volume<1)
        {
            escapeTone.volume += Time.deltaTime/30;
        }
        
    }

    public IEnumerator BlowUpDynamite()
    {
        yield return new WaitForSeconds(7f);
        blockage.SetActive(false);
        explosionScript.Explode();
        yield return new WaitForSeconds(60f);
        stones.SetActive(false);
    }

    public IEnumerator BlowUpBigShow()
    {
        yield return new WaitForSeconds(120f);
        barrels.SetActive(false);
    }
}
