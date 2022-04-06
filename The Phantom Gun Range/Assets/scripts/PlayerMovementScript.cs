using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerMovementScript : MonoBehaviour
{
    public float speed = 1;
    public XRNode inputSourceLeft;
    public XRNode inputSourceRight;
    //public float gravity = -9.81f;
    public LayerMask groundLayer;
    public float additionalHeight = 0.3f;
    //private float fallingspeed;
    private XRRig rig;
    private Vector2 inputAxis;
    private CharacterController character;
    //private bool jump = false;
    private bool TPGun = false;
    private bool catchGun = false;
    private bool menuon = false;
    private int wasmenuon = -1;
    private bool menuononce = false;
    public float jumpspeed;

    InputDevice deviceLeftmove;
    InputDevice deviceLeftcatch;
    InputDevice deviceLeftTP;
    InputDevice devicemenu;
    //InputDevice deviceRightjump;

    public GameObject Gun;
    private bool gunheld = false;
    private bool gunbelt = true;

    public GameObject gamevis;
    public GameObject gamevisend;
    public Transform cam;

    private endUI EUI;
    private UI Gui;
    
    void Awake(){
        EUI = GameObject.FindObjectOfType<endUI> ();
        Gui = GameObject.FindObjectOfType<UI> ();
        //find end game UI
    }
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
        
        VRstart();
    }
    void VRstart(){
        deviceLeftmove = InputDevices.GetDeviceAtXRNode(inputSourceLeft);
        deviceLeftcatch = InputDevices.GetDeviceAtXRNode(inputSourceLeft);
        deviceLeftTP = InputDevices.GetDeviceAtXRNode(inputSourceLeft);
        //deviceRightjump = InputDevices.GetDeviceAtXRNode(inputSourceRight);
        devicemenu = InputDevices.GetDeviceAtXRNode(inputSourceLeft);
    }
    // Update is called once per frame
    void Update()
    {
        if(!deviceLeftmove.isValid || !devicemenu.isValid || !deviceLeftcatch.isValid || !deviceLeftTP.isValid){
            VRstart();
        } else{

            deviceLeftmove.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
            deviceLeftcatch.TryGetFeatureValue(CommonUsages.primaryButton, out catchGun);
            deviceLeftTP.TryGetFeatureValue(CommonUsages.secondaryButton, out TPGun);
            devicemenu.TryGetFeatureValue(CommonUsages.menuButton, out menuon);
            //deviceRightjump.TryGetFeatureValue(CommonUsages.primaryButton, out jump);
        }
        gamevis.transform.eulerAngles = new Vector3(0, cam.eulerAngles.y, 0);
        gamevisend.transform.eulerAngles = new Vector3(0, cam.eulerAngles.y, 0);
        gamevis.transform.position = new Vector3(cam.position.x, gamevis.transform.position.y, cam.position.z);
        gamevisend.transform.position = new Vector3(cam.position.x, gamevisend.transform.position.y, cam.position.z);
    }
    private void FixedUpdate(){
        CapsuleFollowHeadset();

        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        character.Move(direction * Time.fixedDeltaTime * speed);

        bool isGrounded = CheckIfGrounded();
        

        /*if(isGrounded && jump && deviceRightjump.TryGetFeatureValue(CommonUsages.primaryButton, out jump)){            
            //this.GetComponent<Rigidbody>().AddForce(0,jumpspeed,0, ForceMode.Impulse);
            this.GetComponent<Rigidbody>().velocity = new Vector3(0,jumpspeed,0);
        }*/

        if(!gunheld && gunbelt){
            Gun.transform.position = this.transform.position + new Vector3(0, 3/4, 0);
            Gun.transform.eulerAngles = new Vector3(0, cam.eulerAngles.y, 0);
            Gun.GetComponent<Rigidbody>().useGravity = false;
        }

        if(deviceLeftcatch.TryGetFeatureValue(CommonUsages.primaryButton, out catchGun) && catchGun && !TPGun){
            if(!gunheld){
                Gun.transform.position = this.transform.position + new Vector3(0, 3/4, 0);
                gunbelt = true;
            }
        }
        if(deviceLeftTP.TryGetFeatureValue(CommonUsages.secondaryButton, out TPGun) && TPGun && !catchGun){
            if(!gunheld){
                this.transform.position = Gun.transform.position + new Vector3(0,1/2,0); //adds height to position to prevent falling through floor
                //Gun.transform.position = this.transform.position + new Vector3(0, 1/2, 0);
                Gun.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                gunbelt = true;
            }
        }
        if(gunheld){
            gunbelt = false;
            
        } else{
            Gun.GetComponent<Rigidbody>().useGravity = true;
        }
        if(devicemenu.TryGetFeatureValue(CommonUsages.menuButton, out menuon)&& menuon && !menuononce){
            
            menuononce = true;
            wasmenuon *= -1;
            if(wasmenuon == 1){
                EUI.score(Gui.gamescore, "--:--:--");
            }else{
                EUI.closemenu();
            }
                
        }else if(devicemenu.TryGetFeatureValue(CommonUsages.menuButton, out menuon)&& !menuon){menuononce = false;}
        
    }
void CapsuleFollowHeadset(){
    character.height = rig.cameraInRigSpaceHeight + additionalHeight;
    Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
    character.center = new Vector3(capsuleCenter.x, character.height/2 + character.skinWidth, capsuleCenter.z);
}

    bool CheckIfGrounded(){
        Vector3 rayStart = transform.TransformPoint(character.center);
        //float rayLength = character.center.y + 0.01f;
        float rayLength = character.center.y + 1f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }
    public void Gunheld(){
        gunheld = true;
    }
    public void GunNothel(){
        gunheld = false;
    }

}
