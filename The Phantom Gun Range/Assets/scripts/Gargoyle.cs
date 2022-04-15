using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gargoyle : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject player;
    public string gargoylestate = "idle";
    public Animator gargoyleanim;
    

    private endUI EUI;
    private PlayerController PC;

    void Awake(){
        EUI = GameObject.FindObjectOfType<endUI> ();
        PC = GameObject.FindObjectOfType<PlayerController> ();
        //find end game UI
    }
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate(){
        if(gargoylestate == "idle"){
            idle();
        }else if(gargoylestate == "search"){
            search();
        }
    }
    private void idle(){    
        agent.destination = player.transform.position;  
        if(Vector3.Distance(agent.destination, this.transform.position) >= 3){  
            gargoylestate = "search";
            gargoyleanim.SetInteger("Gargoyleanimstate", 1);            
        }
    }
    private void search(){
        /*if(gargoyleanim.GetCurrentAnimatorStateInfo(0).IsName("fly")){
            agent.destination = player.transform.position;
        }*/
        if(Vector3.Distance(agent.destination, this.transform.position) < 3){
            StartCoroutine(searchtoidle());
        }
        
        if(Vector3.Distance(new Vector3(this.transform.position.x , 0,this.transform.position.z), new Vector3(player.transform.position.x, 0, player.transform.position.z)) < 3){
            if(Mathf.Abs(this.transform.position.y - player.transform.position.y) < 6){
                gargoylestate = "idle";
                gargoyleanim.SetInteger("Gargoyleanimstate", 0);
                StartCoroutine(died());
            }else{Debug.Log("Heightissue");}
        }
    }
    
    IEnumerator searchtoidle(){
        gargoyleanim.SetInteger("Gargoyleanimstate", 0);
        yield return new WaitForSeconds(2f);
        gargoylestate = "idle";        
    }

    public GameObject deathnote;
    IEnumerator died(){
        PC.isplaying = false;
        deathnote.SetActive(true);
        //Time.timeScale = 0;
        yield return new WaitForSeconds(1f);
        //Time.timeScale = 1;
        EUI.home();       
    }
}
