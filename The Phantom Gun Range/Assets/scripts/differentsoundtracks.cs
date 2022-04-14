using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class differentsoundtracks : MonoBehaviour
{
    public AudioClip holding;
    public AudioClip notholding;

    private AudioSource audioSource;
    private bool holdingb = false;
   

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playnotgun();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playgun(){
        holdingb = true;

        //audioSource.Pause();

        audioSource.clip = holding;
        audioSource.Play(0);
    }
    public void playnotgun(){
        holdingb = false;

        //audioSource.Pause();

        audioSource.clip = notholding;
        audioSource.Play(0);
    }
}
