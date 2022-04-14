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
    private Vector3 gunpos2d;

    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
        this.gameObject.SetActive(false);
        
        //this.GetComponent<NavMeshAgent>().enabled = false;
    }
    
    void FixedUpdate ()
    {
        if(bulletstate == "fly"){  
            RaycastHit hit;
            if(Physics.Raycast(gunpos.transform.position, new Vector3(0,-2,0), out hit, Mathf.Infinity)){
                agent.destination = hit.point;
            }else{
               agent.destination = gunpos.transform.position; 
            }  
            
            //agent.destination = gunpos.transform.position;
            
            Debug.Log(gunpos.transform.position);
        }     
        
        gunpos2d = new Vector3(gunpos.transform.position.x, this.transform.position.y, gunpos.transform.position.z);

        if(Vector3.Distance(this.transform.position, gunpos2d) < 1 && bulletstate != "shot"){
            
            Debug.Log(bulletstate);  
            //bulletstate = "loaded";
            //this.transform.position = new Vector3(0,-30, 0);
            //agent.destination = this.transform.position;
            
            //this.GetComponent<NavMeshAgent>().enabled = false;
            
            ++Pc.ammo;
            Pc.Updateammo();
            //audioSource.PlayOneShot(SoundManager.Instance.reloadS);
            this.gameObject.SetActive(false);
        }
	}
    /*void Update(){
        gunpos2d = new Vector3(gunpos.transform.position.x, this.transform.position.y, gunpos.transform.position.z);

        if(Vector3.Distance(this.transform.position, gunpos2d) < 1 && bulletstate != "shot"){
            
            Debug.Log(bulletstate);  
            //bulletstate = "loaded";
            //this.transform.position = new Vector3(0,-30, 0);
            //agent.destination = this.transform.position;
            
            //this.GetComponent<NavMeshAgent>().enabled = false;
            
            ++Pc.ammo;
            Pc.Updateammo();
            this.gameObject.SetActive(false);
        }
    }*/
}
