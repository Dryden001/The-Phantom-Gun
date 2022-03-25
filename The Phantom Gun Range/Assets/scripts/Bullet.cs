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
    public GameObject gunpos;
    public NavMeshAgent agent;
    public PlayerController Pc;

    private void OnCollisionEnter(Collision collision){
        if(bulletstate == "shot"){
            bulletstate = "fly";
            
        }/*else if((bulletstate == "Sit" || bulletstate == "fly")&& collision.gameObject.layer == 11){
            //++Pc.ammo;
            //Destroy(gameObject);
        }*/
    }
void Awake(){
        Pc = GameObject.FindObjectOfType<PlayerController> ();
    }
    void Start(){
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        
        
    }
    void FixedUpdate ()
    {
        if(bulletstate == "Sit"){
        }else if(bulletstate == "fly"){           
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = gunpos.transform.position;
            //agent.destination = new Vector3(0, 0, 0);
        }     
        Debug.Log(bulletstate);   
	}
    void Update(){
        if(Vector3.Distance(this.transform.position, gunpos.transform.position) < 1 && bulletstate != "shot"){
            bulletstate = "loaded";
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = this.transform.position;
            this.transform.position = new Vector3(0,-30, 0);
            ++Pc.ammo;
        }
        Debug.Log(bulletstate);  
    }
}
