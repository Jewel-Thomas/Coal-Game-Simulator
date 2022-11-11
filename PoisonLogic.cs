using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoisonLogic : MonoBehaviour
{
    public GameObject poisonEffect;
    public ParticleSystem poisonCloud;
    public bool isPlaying = false;
    public AudioSource audioSource;
    public AudioClip deathTrack;
    public static bool isPoisoning = false;
    public Image poisonImage;
    public float alphaValue = 0;
    public float poisonValue = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if(isPoisoning)
        {
            Poisoning();
        }
        else
        {
            UnPoisoning();
        }
        PoisonDisappear();
        // if(Input.GetKeyDown(KeyCode.H))
        // {
        //     isPoisoning = !isPoisoning;
        // }
    }

    void Poisoning()
    {
        if(alphaValue<=0.99f)
        {
            alphaValue += Time.deltaTime/30;
            var color = poisonImage.color;
            color.a = alphaValue;
            poisonImage.color = color;
        }
        else if(!isPlaying)
        {
            audioSource.PlayOneShot(deathTrack);
            isPlaying = !isPlaying;
        }
        
    }
    void UnPoisoning()
    {
        if(alphaValue>=0)
        {
            alphaValue -= Time.deltaTime/20;
        }
        var color = poisonImage.color;
        color.a = alphaValue;
        poisonImage.color = color;
    }

    [System.Obsolete]
    void PoisonDisappear()
    {
        if(RayCastScript.isValveOpen)
        {
            if(poisonValue>=0)
            {
               poisonValue-= Time.deltaTime/20; 
            }
            var poisonColor = poisonCloud.startColor;
            poisonColor.a = poisonValue;
            poisonCloud.startColor = poisonColor;
            poisonEffect.SetActive(false);
            isPoisoning = !isPoisoning;
        }
    }
}
