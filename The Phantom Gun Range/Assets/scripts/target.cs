using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class target : MonoBehaviour
{
    //target status
    public bool isup = true;
    public Transform player;
    public GameObject ttarget;
    public bool isgargoyle = false;

    //target identifies UI script
    private UI UoI;

    //target identifies who UI is
    void Awake(){
        UoI = GameObject.FindObjectOfType<UI> ();
    }

    void Start()
    {
         
    }
    //target follows player to allways look at it
    void Update()
    {
        if(!isgargoyle){
            this.transform.rotation = player.rotation;
        }
    }
    //target gets shot
    void OnTriggerEnter(Collider other){
        if (isup && other.tag =="hittarget"){
            shot();
        }
    }
    //target gets hidden and tells UI its been shot
    public void shot(){
        ttarget.SetActive(false);
        UoI.JScore();
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.targethit);
    }
}
