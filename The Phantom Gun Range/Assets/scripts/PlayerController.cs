using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    //game object attachement
    public GameObject bulletPrefab;
    public GameObject bulletO;
    public GameObject Gun;
    public Rigidbody Gunrigid;
    public Transform facingdirection;
    public Transform Headposition;
    public Transform launchPosition;
    public Transform AimPosition;
    public Transform GunHeldPos;

    public Rigidbody thisplayerrigid;
    public GameObject thisplayer;
    public bool plforward = false;
    public bool plbackward = false;
    public bool pljump = false;

    //private bool IsGrounded = true;
    public int jumpspeed;
    private Vector3 moveplayer;
    

    //Camera follow gun [dead code]
    /*public GameObject Cam;
    public GameObject HeadJoint;*/

    //Game modifiers
    public int projectilespeedmod;
    public int throwspeed;
    public int height;
    public int heightd;
    public int startingammo;
    
    //Gun info
    private Vector3 GunPos = Vector3.zero;
    //private int GunState = 0;
    public static int ammomax = 6;
    public int ammo;
    //public static int ammo;
    

    //Stops game at the end
    private bool isplaying = true;

    //sound
    private AudioSource audioSource;

    //gets pass to UI script
    private UI UoI;


    void Awake(){
        UoI = GameObject.FindObjectOfType<UI> ();
    }
    //set initial values and grabs audio component
	void Start ()
    {
        //thisplayerrigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        ammo = startingammo;
        UoI.UpdateAmmo(ammo,ammomax);
        
	}
    
	public void shoot (){
        if(ammo != 0){ //can shoot?
                    //shot = true;
                    fireBullet(); //invokes bullet
                    --ammo;
                    UoI.UpdateAmmo(ammo,ammomax);
        }
        
    }
    public void Updateammo(){
        UoI.UpdateAmmo(ammo,ammomax);
    }
	void Update ()
    {
        
	}
    
    void fireBullet(){
        audioSource.PlayOneShot(SoundManager.Instance.gunFire);
        
        bulletO.transform.position = launchPosition.position;
        bulletO.SetActive(true);
        Bullet bulletscript = bulletO.GetComponent<Bullet>();
        bulletscript.bulletstate = "shot";
        Debug.Log("shot");  
        bulletO.GetComponent<Rigidbody>().velocity = launchPosition.forward * projectilespeedmod;
    }

    /*//shoot gun
    void fireBullet()
    {
        audioSource.PlayOneShot(SoundManager.Instance.gunFire);
        Rigidbody bullet = createBullet();
        //bullet.velocity = (AimPosition.position - launchPosition.position) / projectilespeedmod;
        bullet.velocity = launchPosition.forward * projectilespeedmod;
    }
    //bullet been shot
    private Rigidbody createBullet(){
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        Bullet bulletscript = bullet.GetComponent<Bullet>();
        bulletscript.bulletstate = "shot";
        bullet.transform.position = launchPosition.position;
        return bullet.GetComponent<Rigidbody>();
    }*/
    

    //game has been ended
    public void gameactive(bool GA){
        isplaying = GA;
    }

    
}
