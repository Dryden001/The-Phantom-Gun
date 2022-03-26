using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public PlayerController Pc;
    public GameObject otherbullet;

    private Vector3 lastpos;

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.layer != 11){
           bulletstate = "fly";
           
        }
    }

    void Awake(){
        Pc = GameObject.FindObjectOfType<PlayerController> ();
    }
    void Start(){
        this.gameObject.SetActive(false);
    }
    void Update(){
        if(bulletstate == "Sit"){
            if(Vector3.Distance(this.transform.position, gunpos.transform.position) < 1 && bulletstate != "shot"){
                
                Debug.Log(bulletstate);  
                bulletstate = "loaded";
                //this.transform.position = new Vector3(0,-30, 0);
                //agent.destination = this.transform.position;
                
                //this.GetComponent<NavMeshAgent>().enabled = false;
                
                ++Pc.ammo;
                Pc.Updateammo();
                this.gameObject.SetActive(false);
            }
        }
        if(lastpos == this.transform.position && bulletstate == "fly"){
            
           otherbullet.SetActive(true);
           otherbullet.transform.position = this.transform.position;
           this.gameObject.SetActive(false);
        }else if(bulletstate == "fly"){
            lastpos = this.transform.position;
        }
    }
    /*bool CheckIfGrounded(){
        Vector3 rayStart = transform.TransformPoint(this.transform.position);
        //float rayLength = character.center.y + 0.01f;
        float rayLength = character.center.y + 1f;
        bool hasHit = Physics.SphereCast(rayStart, 0.1f, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }*/
}
