using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class endUI : MonoBehaviour
{
    //Tracks game score
    public Text gscore;
    public Text hgscore;
    public GameObject canvasscore;
    public GameObject Rayguy;
    
    string gamescore;
    string hgamescore;

    

    //pass game over to player controller script
    private PlayerController Pcntrl;

    void Awake(){
        Pcntrl = GameObject.FindObjectOfType<PlayerController> ();
    }
    //turns off end screen in case it gets turned on.
    void Start()
    {
        canvasscore.SetActive(false);
        Rayguy.SetActive(false);
        
    }

    void Update()
    {
        
    }

    //Game has finished and end screen gets shown
    public void score(string gamescore, string hgamescore){
        Debug.Log(gamescore);
        Debug.Log(hgamescore);
        
        gscore.text = gamescore;
        hgscore.text = hgamescore;
        Pcntrl.gameactive(false);
        canvasscore.SetActive(true);
        Rayguy.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //Time.timeScale = 0;
    }
    //button to go to start screen
    public void home(){
        Time.timeScale = 1;
        Debug.Log("start");
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.Buttonpress);
        SceneManager.LoadScene("StartScreen");

    }
    
}
