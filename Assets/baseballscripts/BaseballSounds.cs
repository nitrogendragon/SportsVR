using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballSounds : MonoBehaviour {



    //references to gameobjects
    public GameObject baseball;
    public GameObject batholder1;
    public GameObject batholder2;
    public GameObject ground;
    public GameObject skeleton;
    //reference to scoreboard script
    GameObject scoreboard;
    //OB gameobject
    public GameObject OB;
    baseballscoreboard bsb;//scoreboard script reference
    //audio references
    public AudioClip hitbat;
    public AudioClip homerun;
    //public AudioClip backgroundsounds;
    public AudioClip foulball;
    public AudioClip normalhit;
   
    
    void Awake()
    {
        scoreboard = GameObject.Find("ScoreBoard");//find scorebaord
        bsb = scoreboard.GetComponent<baseballscoreboard>();//grabs script form scorebaord

    }
    // check for trigger enters and play relevant audio based on trigger entered
    private void OnTriggerEnter(Collider other)
    {
        //if it is one of these objects then play the corresponding clip at the specified point
        if (other.gameObject == batholder1 || other.gameObject == batholder2)
        {
            AudioSource.PlayClipAtPoint(hitbat, GameObject.Find("CameraRig").transform.position);
        }
    }
     //check to see if the ball hits the ground/inbound or the OB plane
    void OnCollisionEnter(Collision other)
    {

        //if ground and distance is x or more then play sound
        if (other.gameObject == ground && bsb.distancetraveled>=160)
        {
            
            AudioSource.PlayClipAtPoint(homerun, skeleton.transform.position);
        }
        //if OB then  play sound
        else if(other.gameObject == OB)
        {
          
            AudioSource.PlayClipAtPoint(foulball, skeleton.transform.position);
        }
        
    }

   

}
