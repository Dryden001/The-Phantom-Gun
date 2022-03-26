using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBullett : MonoBehaviour
{
    public string bulletstate = "fly"; //Sit, loaded, shot, fly
    public GameObject gunpos;
    private NavMeshAgent agent;
    public PlayerController Pc;
    public GameObject otherbullet;

    /*private void OnCollisionEnter(Collision collision){
        if(bulletstate == "shot"){
            bulletstate = "fly";
            
        }/*else if((bulletstate == "Sit" || bulletstate == "fly")&& collision.gameObject.layer == 11){
            //++Pc.ammo;
            //Destroy(gameObject);
        }
    }*/
void Awake(){
        Pc = GameObject.FindObjectOfType<PlayerController> ();
    }
    void Start(){
        agent = this.GetComponent<NavMeshAgent>();
        this.gameObject.SetActive(false);
        //this.GetComponent<NavMeshAgent>().enabled = false;
    }
    void FixedUpdate ()
    {
        if(bulletstate == "fly"){  
            //this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
              
            //this.GetComponent<NavMeshAgent>().enabled = true;     
            agent.destination = gunpos.transform.position;
            //agent.destination = new Vector3(0, 10, 0);
            Debug.Log(gunpos.transform.position);
        }     
        
	}
    void Update(){
        if(Vector3.Distance(this.transform.position, gunpos.transform.position) < 1 && bulletstate != "shot"){
            
            Debug.Log(bulletstate);  
            //bulletstate = "loaded";
            //this.transform.position = new Vector3(0,-30, 0);
            //agent.destination = this.transform.position;
            
            //this.GetComponent<NavMeshAgent>().enabled = false;
            
            ++Pc.ammo;
            Pc.Updateammo();
            this.gameObject.SetActive(false);
        }
    }
}
