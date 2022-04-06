using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //start game button
    public void startgame(string level){
        Debug.Log("start");
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.Buttonpress);
        SceneManager.LoadScene(level);
    }
    //exit game button
    public void exitgame(){
        Debug.Log("exit");
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.Buttonpress);
        Application.Quit();
    }
}
