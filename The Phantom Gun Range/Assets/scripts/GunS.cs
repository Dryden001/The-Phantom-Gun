using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunS : MonoBehaviour
{
    public PlayerController Pc;


    void Awake(){
        Pc = GameObject.FindObjectOfType<PlayerController> ();
    }
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.layer == 11){
            if(collision.gameObject.GetComponent<Bullet>().bulletstate != "Shot"){
                ++Pc.ammo;
                Destroy(collision.gameObject);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
