using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/**handles all thigns related to the controller in the baseball game including menus and holding the baseball bat**/
public class baseballcontroller : MonoBehaviour
{
    bool nextcheck;//bool check
    public GameObject scoreboard;//scoreboard gameobject
    baseballscoreboard bsb;//reference to scorebaord script
    bool batinhand = false;//checks if abat is in hand
    public Transform headpos;//position of the head
    public GameObject batholder;// object reference that holds and orients the bat
    float timetilmenushows;//delay for showing menu when game ends
    float timetilgamestart;//delay for starting game after you tell it to play
    bool gamestarted = false;//has the game started?
    bool startselected = false;// start button is being interacted with or not
    bool exitselected = false;// exit button is being interacted with or not
    bool startmenutimer;// bool for determining whether to start delay for menu appearance/disappearance
    public GameObject baseballbat;//reference to bat
    public GameObject begingame;//reference to button for starting game
    public GameObject exit;//reference to button for exiting game
    public GameObject skeleton;//reference to our lovely skeleton pitcher credit of turbosquid
    skeletonthrow st;//reference to skeleton throw script
    public GameObject startbox;// no longer used
    bool inhand = false;// another check for if object is in hand
    bool throwableinhand = false;// checks if specific type of object is in hand
    private SteamVR_TrackedObject trackedObj;//reference to the controller
    // 1
    private GameObject collidingObject;// reference to object that controller is colliding with
    // 2
    private GameObject objectInHand;//reference to objects in hand

    private SteamVR_Controller.Device Controller//controller reference of another kind
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }// grabs specific controller
    }

    void Awake()
    {
        /** grabs script components attached to objects for use to reference**/
        st = skeleton.GetComponent<skeletonthrow>();
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        bsb = scoreboard.GetComponent<baseballscoreboard>();

    }






    // checks to see what trigger colliders are being entered
    public void OnTriggerEnter(Collider other)
    {
        //changes color of button on trigger enter and tells the bool that we are triggering the trigger of the start game button
        if (other.gameObject == begingame)
        {
            begingame.GetComponent<Renderer>().material.color = Color.blue;
            startselected = true;
        }
        //changes color of button on trigger enter and tells the bool that we are triggering the trigger of the exit game button
        if (other.gameObject == exit)
        {
            exit.GetComponent<Renderer>().material.color = Color.cyan;
            exitselected = true;

        }

    }

    // 2


    // checks if you exit collison with a trigger object
    public void OnTriggerExit(Collider other)
    {
        // changes bool back to false from trigger enter and returns color of button to previous state
        if (other.gameObject == begingame)
        {
            begingame.GetComponent<Renderer>().material.color = Color.white;
            startselected = false;


        }
        // changes bool back to false from trigger enter and returns color of button to previous state
        if (other.gameObject == exit)
        {
            exit.GetComponent<Renderer>().material.color = Color.white;
            exitselected = false;
        }
        //potentially unnecessary code
        if (!collidingObject)
        {
            return;
        }


    }

    //function for grabbing locked object not meant to be thrown
    private void GrabLockedObject()
    {
        /**sets bat to be active in the game and sets its position and rotation to be the same as the batholders and makes the object in hand be the baseball bat,
         * additionally changes bool for inhand so that it knows there is something being held and then lastly creates a joint to connnect them and undoes the batinhand bool**/
        baseballbat.SetActive(true);
            baseballbat.transform.position = batholder.transform.position;
            baseballbat.transform.rotation = batholder.transform.rotation;
            objectInHand = baseballbat;
            
            inhand = true;

            var joint = AddFixedJoint();
            joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
        batinhand = false;
    }



    // the actual function for adding the joint and sets up the breakforce so it doesn't just fly out of your hand when it collides with the ball
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 200000;
        fx.breakTorque = 200000;
        return fx;
    }



    //starts the baseball game specifically the pitching animation
    void startgame()
    {
        //count check for how many throws have been thrown
        if (st.count == 0)
        {
            st.throwagain = true;//tell the pitcher it should throw again
        }
    }
    //function for exiting game
    private void exitgame()
    {
        SceneManager.LoadScene("Main Menu");//loads the main menu scene
    }
    //lots of checks
    void FixedUpdate()
    {
        //if count equals limit
        if (st.count == 10)
        {
            nextcheck = true;//proceed
        }
            if (bsb.d11.text != "0" && nextcheck==true)//makes sure the last ball has hit the ground
            {
            nextcheck = false;
                startmenutimer = true;//starts timer for displaying the menu
            }
        
        if (startmenutimer == true)
        {
            timetilmenushows += Time.deltaTime;//starts delay timer
        }
        if (timetilmenushows >= 3)
        {
           
            timetilmenushows = 0;//reset timer
            begingame.SetActive(true);// button reappears
            exit.SetActive(true);//button reappears
            startmenutimer = false;//dont run last if statement
            baseballbat.SetActive(false);//turn off bat
        }

        
        // starts game on trigger press
        if (Controller.GetHairTriggerDown() && startselected == true)
        {
            begingame.GetComponent<Renderer>().material.color = Color.white;//reset button color
            begingame.SetActive(false);// hide button
            exit.SetActive(false);//hide button
            gamestarted = true;//game has started
            startselected = false;//button not being interacted with anymore, safety code check
            batinhand = true;// you are now holding a bat or should be soon 
        }
        //if game has started..
        if (gamestarted == true)
        {
            baseballbat.SetActive(true);//turn bat on
            timetilgamestart += Time.deltaTime;//start timer for delaying initial pitch
        }
        //timer reached 3 seconds
        if (timetilgamestart >= 3)
        {
            gamestarted = false;//set false now so game can be restarted again later and functions don't run accidentally that aren't wanted
            startgame();//run game starting function
            timetilgamestart = 0;//reset timer
        }
        //if button down and triggering exit button..
        else if (Controller.GetHairTriggerDown() && exitselected == true)
        {
            exitselected = false;//deselect
            exit.GetComponent<Renderer>().material.color = Color.white;//reset color
            exitgame();//exit game 


        }
        //check  if holding bat, should only happen if start button has been pressed though it can bug sometimes
        if (batinhand == true)
        {
            GrabLockedObject();//run function to be put into hand
        }
        //safety check in case something wonky happens
        if (Controller.GetHairTriggerDown() && batinhand == false)
        {
            GrabLockedObject();//puts bat in hand again 
        }





    }
}


/**using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class baseballcontroller : MonoBehaviour
{
    public Transform headpos;
    public GameObject batholder;
    float timetilmenushows;
    float timetilgamestart;
    bool gamestarted;
    bool startselected = false;
    bool exitselected = false;
    bool startmenutimer;
    public GameObject baseballbat;
    public GameObject begingame;
    public GameObject exit;
    public GameObject skeleton;
    skeletonthrow st;
    public GameObject startbox;
    bool inhand = false;
    bool throwableinhand = false;
    private SteamVR_TrackedObject trackedObj;
    // 1
    private GameObject collidingObject;
    // 2
    private GameObject objectInHand;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        st = skeleton.GetComponent<skeletonthrow>();
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        

    }
   
    


    private void SetCollidingObject(Collider col)
    {
        // 1
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        // 2
        collidingObject = col.gameObject;
    }

    // 1
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject == begingame)
        {
            begingame.GetComponent<Renderer>().material.color = Color.blue;
            startselected = true;
        }
        if (other.gameObject == exit)
        {
            exit.GetComponent<Renderer>().material.color = Color.cyan;
            exitselected = true;
            
        }
        else
        {
            SetCollidingObject(other);
        }
    }

    // 2
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    // 3
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == begingame)
        {
            begingame.GetComponent<Renderer>().material.color = Color.white;
            startselected = false;


        }
        if (other.gameObject == exit)
        {
            exit.GetComponent<Renderer>().material.color = Color.white;
            exitselected = false;
        }
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }


    private void GrabLockedObject()
    {

        if (collidingObject == baseballbat)
        {
           
            collidingObject.transform.position = batholder.transform.position;
            collidingObject.transform.rotation = batholder.transform.rotation;
            objectInHand = collidingObject;
            collidingObject = null;

            inhand = true;
        }


      

        // 2
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private void GrabThrowableObject()
    {
        if (collidingObject)
        {
            objectInHand = collidingObject;
            collidingObject = null;
            throwableinhand = true;
        }
        // 2
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    // 3
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 200000;
        fx.breakTorque = 200000;
        return fx;
    }

    private void ReleaseThrowableObject()
    {
        // 1
        if (GetComponent<FixedJoint>())
        {
            // 2
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // 3
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }
        // 4
        objectInHand = null;
    }
    private void ReleaseLockedObject()
    {
        // 1
        if (GetComponent<FixedJoint>())
        {
            // 2
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // 3
            //objectInHand.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            //objectInHand.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            //objectInHand.GetComponent<Rigidbody>().rotation = new Quaternion(0, 0, 0, 1);
            
        }
        // 4
        objectInHand = null;
    }
    //starts the baseball game specifically the pitching animation
    void startgame()
    {
        if (st.count == 0)
        {
            st.throwagain = true;
        }
    }
    private void exitgame()
    {
        SceneManager.LoadScene("Basketball");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // begingame.transform.LookAt(headpos);
        // exit.transform.LookAt(headpos);
        if (st.count == 10)
        {
            print("at 10");
            startmenutimer = true;
        }
        if (startmenutimer == true)
        {
            timetilmenushows += Time.deltaTime;
        }
        if (timetilmenushows >= 3)
        {
            print("should be showing menu");
            timetilmenushows = 0;
            begingame.SetActive(true);
            exit.SetActive(true);
            startmenutimer = false;
        }


        // 1
        if (Controller.GetHairTriggerDown() && startselected == true)
        {
            begingame.GetComponent<Renderer>().material.color = Color.white;
            begingame.SetActive(false);
            exit.SetActive(false);
            gamestarted = true;
            startselected = false;
        }
        if(gamestarted == true)
        {
            timetilgamestart += Time.deltaTime;
        }
        if (timetilgamestart >= 3)
        {
            gamestarted = false;
            startgame();
            timetilgamestart = 0;
        }
        
        else if (Controller.GetHairTriggerDown() && exitselected == true)
        {
            exitselected = false;
            exit.GetComponent<Renderer>().material.color = Color.white;
            exitgame();
            

        }
        if (Controller.GetHairTriggerDown() && inhand == false && collidingObject.CompareTag("replaceHand"))
        {
            GrabLockedObject();
        }
        else if (Controller.GetHairTriggerDown() && inhand == true)
        {
            if (objectInHand)
            {
                ReleaseLockedObject();
                inhand = false;
            }
        }
        else if (Controller.GetHairTriggerDown() && throwableinhand == false && !collidingObject.CompareTag("replaceHand"))
        {
            GrabThrowableObject();
        }

        // 2
        if (Controller.GetHairTriggerUp() && throwableinhand == true && inhand == true)
        {
            if (objectInHand)
            {
                ReleaseThrowableObject();
                throwableinhand = false;
                inhand = false;
            }
        }
        else if (Controller.GetHairTriggerUp() && throwableinhand == true)
        {
            if (objectInHand)
            {
                ReleaseThrowableObject();
                throwableinhand = false;
            }
        }


    }
}**/
