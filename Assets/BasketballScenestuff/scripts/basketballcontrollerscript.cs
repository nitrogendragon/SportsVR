﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basketballcontrollerscript : MonoBehaviour
{
    public GameObject basketballstartbutton;
    BasketballGame basketballscript;
    public GameObject objectposition;
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
        basketballscript = basketballstartbutton.GetComponent<BasketballGame>();
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        print("we found the stuff");

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
        SetCollidingObject(other);

    }

    // 2
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
        if (other.CompareTag("replaceHand") && objectInHand==false)
        {
            SteamVR_Controller.Input((int)trackedObj.index).TriggerHapticPulse(500);
        }
    }

    // 3
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }




       

    private void GrabThrowableObject()
    {
        if (collidingObject)
        {
            objectInHand = collidingObject;
            collidingObject = null;
            throwableinhand = true;
           
                objectInHand.transform.position = new Vector3(objectposition.transform.position.x, objectposition.transform.position.y, objectposition.transform.position.z);
            
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
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity *1.3f;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity*-1;
            
        }
        // 4
        objectInHand = null;
    }
   



    // Update is called once per frame
    void FixedUpdate()
    {
        // 1
       
         if (Controller.GetHairTriggerDown()  && collidingObject && collidingObject.CompareTag("startbasketball") && basketballscript.startcd ==false)
        {
            print("hit button");
            basketballscript.countdown = 45;
            basketballscript.score = 0;
            basketballscript.startcd = true;
            
            basketballscript.dropblocker();
        }
        
        if (Controller.GetHairTriggerDown() && throwableinhand == false && collidingObject.CompareTag("replaceHand"))
        {
            
            GrabThrowableObject();
        }
        else if(Controller.GetHairTriggerDown() && throwableinhand == true)
        {
            throwableinhand = false;
            ReleaseThrowableObject();
            
        }

        // 2
        
       
        if (Controller.GetHairTriggerUp() && throwableinhand == true)
        {
            
                ReleaseThrowableObject();
                throwableinhand = false;
            
        }

    
    }
}