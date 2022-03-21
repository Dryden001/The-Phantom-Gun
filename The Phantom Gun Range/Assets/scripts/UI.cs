using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class UI : MonoBehaviour
{
    public Text Ammo;
    public Text Score;
    public Text gTime;
    //screen UI

    float gametime;
    //current gametime

    public int Targets;
    private int maxtargets;
    //target trackers


    float msec;
    float sec;
    float min;
    float hmsec;
    float hsec;
    float hmin;
    string gamescore;
    string ghscore;
    bool bsave;
    //Game time / score

    //highscore
    public Text Starthighscore;

    private endUI EUI;
    //pass to end game

    void Awake(){
        EUI = GameObject.FindObjectOfType<endUI> ();
        //find end game UI
    }
    

    //set ammo
    public void UpdateAmmo(int ammo, int ammomax){
        Ammo.text = "Ammo: " + ammo + "/" + ammomax;
    }
    //target been hit
    public void JScore(){
        --Targets;
        Score.text = "# of Targets: " + Targets + "/" + maxtargets;
        if(Targets <= 0){
            Debug.Log("endgame");
            highscore();
            
            //send to end game
        }
    }
    
    void Start()
    {
        bsave = false;
        LoadGame();
        Starthighscore.text = "High Score: " + string.Format("{0:00}:{1:00}:{2:00}",hmin,hsec,hmsec);
        maxtargets = Targets;
        Score.text = "# of Targets: " + Targets + "/" + maxtargets;
        StartCoroutine("StopWatch");

        
    }

    void Update()
    {      
        
    }

    //game time minutes:seconds:milliseconds
    IEnumerator StopWatch(){
        while(true){
            gametime += Time.deltaTime;
            msec = (int)((gametime-(int)gametime)*100);
            sec = (int)(gametime % 60);
            min = (int)(gametime/60%60);

            gamescore = string.Format("{0:00}:{1:00}:{2:00}",min,sec,msec);
            gTime.text = gamescore;


            yield return null;
        }
    }

    void highscore(){
        bsave = false;
        if(hmin >= min){
            if(hmin > min){
                bsave = true;
            } else if(hsec >= sec){
                if(hsec > sec){
                    bsave = true;
                }else if(hmsec >= msec){
                    if(hmsec > msec){
                        bsave = true;
                    }
                }
            }
        }
        if(bsave){
            SaveGame();
        }

        bsave = false;
        string ghscore = string.Format("{0:00}:{1:00}:{2:00}",hmin,hsec,hmsec);
        //string ghscore = PlayerPrefs.GetString("Highscore", "00:00:00");

        EUI.score(gamescore, ghscore);
    }

    private Save CreateSaveGameObject(){
        Save save = new Save();
        save.highscoremin = min;
        save.highscoresec = sec;
        save.highscoremsec = msec;
        hmin = min;
        hsec = sec;
        hmsec = msec;
        return save;
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamevrsave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved");
    }

    public void LoadGame(){ 
        if (File.Exists(Application.persistentDataPath + "/gamevrsave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamevrsave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();
            hmin = save.highscoremin;
            hsec = save.highscoresec;
            hmsec = save.highscoremsec;
            Debug.Log("Game Loaded");
        }else{
            hmin = 99;
            hsec = 99;
            hmsec = 99;
            Debug.Log("No game saved!");
        }   
    }


}
