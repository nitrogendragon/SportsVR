using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class basketballcontrollerscript : MonoBehaviour
{
    public GameObject quitbutton;//quitbutton
    public GameObject basketballstartbutton;//startbutton
    BasketballGame basketballscript;//basketball game script
    public GameObject objectposition;//positon object
    bool throwableinhand = false;//holding a throwable?
    bool quitbuttonselected = false;//are we interacting with the quit button?
    private SteamVR_TrackedObject trackedObj;//reference for controller
    // 1
    private GameObject collidingObject;//reference for object controller is colliding with if it is
    // 2
    private GameObject objectInHand;//object in hand
    //gets the specific controller
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        basketballscript = basketballstartbutton.GetComponent<BasketballGame>();//grabs script
        trackedObj = GetComponent<SteamVR_TrackedObject>();//grabs component and sets trackedObj to it
      

    }
    //Sets colliding object up
    private void SetCollidingObject(Collider col)
    {
        // are we collding with colliding object or somethign that doesn't have a rigidbody? if so do nothing
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        // set colliding object to the gameobject being collided with
        collidingObject = col.gameObject;
    }

    // if we hit a trigger object..
    public void OnTriggerEnter(Collider other)
    {
        //if quit button cahnge color and say it is selected
        if (other.gameObject == quitbutton)
        {
            quitbutton.GetComponent<Renderer>().material.color = Color.blue;
            quitbuttonselected = true;
        }
        //otherwise run setcollidingobject with this object as the variable
        else
        {
            SetCollidingObject(other);
        }
    }

    //   still colliding with trigger object?
    public void OnTriggerStay(Collider other)
    {
        // if quit button then do nothing
        if (other.gameObject == quitbutton)
        {
            return;
        }
        //otherwise set the colliding object again
        else
        {
            SetCollidingObject(other);
        }
        //if it is an object that in this case would be a basketball and we aren't holding anything give some haptic feedback
            if (other.CompareTag("replaceHand") && objectInHand==false)
        {
            SteamVR_Controller.Input((int)trackedObj.index).TriggerHapticPulse(500);
        }
    }

    // on exitting the trigger collision object
    public void OnTriggerExit(Collider other)
    {
        //if it was the quit button then reset color and say we are not touching it anymore
        if (other.gameObject == quitbutton)
        {
            quitbutton.GetComponent<Renderer>().material.color = Color.white;
            quitbuttonselected = false;
        }
        //otherwise there is no colliding object
        else
        {
            collidingObject = null;
        }
        //do nothing if not colliding with collding object... safety line?
        if (!collidingObject)
        {
            return;
        }
        
    }




       
    //function fo rpicking up a throwable object in this case it would be the basketballs
    private void GrabThrowableObject()
    {
        //is it the colliding object? if so..
        if (collidingObject)
        {
            
            objectInHand = collidingObject;// the objec tiin hanbd gets set to it
            collidingObject = null;//no colidingobject anymore
            throwableinhand = true;//we are holding a throwable
           //set the position of the object to the position of objectposition which is a gameobject attached to controller model to make it easier to properly adjust the postion of the ball in your hand when testing and adjusting values
                objectInHand.transform.position = new Vector3(objectposition.transform.position.x, objectposition.transform.position.y, objectposition.transform.position.z);
            
        }
        // add joint to create attachment to hand
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    // function for adding joint and setting force required to break joint
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();//adds joint component
        fx.breakForce = 200000;//sets break force
        fx.breakTorque = 200000;//sets break torque
        return fx;//returns the joint fx
    }
    //script for throwing basketball
    private void ReleaseThrowableObject()
    {
        // make sure there is a joint
        if (GetComponent<FixedJoint>())
        {
           
            // destroy the joint
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // set the velocity of the thrrown ball to the velocity of the controller , also sets angualr velocity in same manner
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity *1.3f;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity*-1;
            
        }
        // null the object in hand
        objectInHand = null;
    }



    private void LateUpdate()
    {
        /**if (Controller.GetHairTriggerDown() && throwableinhand == true)
        {
            throwableinhand = false;
            ReleaseThrowableObject();

        }**/

        // 2

        //if press trigger and holdinga  throwable release it and there is no throwable anymore in the hand, used in late update to make sure the ball doesn't get stuck in your hand
        if (Controller.GetHairTriggerUp() && throwableinhand == true)
        {

            ReleaseThrowableObject();
            throwableinhand = false;

        }
    }
    
    void FixedUpdate()
    {
        //if gamestarted then hide quit button
        if (basketballscript.startcd == true)
        {
            quitbutton.SetActive(false);
        }
        //otherwsie show quit button
        else
        {
            quitbutton.SetActive(true);
        }
        // if trigger down and button selected load the main menu
        if(Controller.GetHairTriggerDown() && quitbuttonselected == true)
        {
            SceneManager.LoadScene("Main Menu");
        }
        // if trigger down and no collidingObject then do nothing
        if(Controller.GetHairTriggerDown() && !collidingObject)
        {
            return;
        }// if trigger down and colliding with something and it is the button for starting basketball and the game isn't running then reset game timer value and score and change bool so that it knows the game has started, also run function for dropping blocker to let the basketballs free.
         if (Controller.GetHairTriggerDown()  && collidingObject && collidingObject.CompareTag("startbasketball") && basketballscript.startcd ==false)
        {
            //print("hit button");
            basketballscript.countdown = 45;
            basketballscript.score = 0;
            basketballscript.startcd = true;
            
            basketballscript.dropblocker();
        }
        //if trigger pressed and object is a basketball/has the replaceHand tag then grab it
        if (Controller.GetHairTriggerDown() && throwableinhand == false && collidingObject.CompareTag("replaceHand"))
        {
            
            GrabThrowableObject();
        }
        

    
    }
}