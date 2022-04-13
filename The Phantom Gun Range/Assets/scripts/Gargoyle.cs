using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gargoyle : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject player;
    private string gargoylestate = "search";
    public Animator gargoyleanim;
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
        }else if(gargoylestate == "reached"){
            reached();
        }
    }
    private void idle(){
        if(Vector3.Distance(this.transform.position, player.transform.position) >= 1){
            gargoylestate = "search";
            gargoyleanim.SetInteger("Gargoyleanimstate", 1);
        }
    }
    private void search(){
        agent.destination = player.transform.position;
        if(Vector3.Distance(this.transform.position, player.transform.position) < 1){
            gargoylestate = "reached";
        }
    }
    private void reached(){
        gargoyleanim.SetInteger("Gargoyleanimstate", 0);
    }
}
