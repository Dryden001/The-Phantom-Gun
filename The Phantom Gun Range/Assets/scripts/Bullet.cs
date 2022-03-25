using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{
    /*//remove bullet on dissapear
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    //remove bullet on collision
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }*/

    public string bulletstate = "Sit"; //Sit, loaded, shot, fly
    public Transform gunpos;
    private NavMeshAgent agent;

    private void OnCollisionEnter(Collision collision){
        if(bulletstate == "shot"){
            bulletstate = "fly";
            
        }else if((bulletstate == "Sit" || bulletstate == "fly")&& collision.gameObject.layer == 11){
            //++Pc.ammo;
            //Destroy(gameObject);
        }
    }

    void Start(){
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
    }
    void Update ()
    {
        if(bulletstate == "Sit"){

        }else if(bulletstate == "fly"){
           
            agent.destination = gunpos.position;
        }        
	}
}
