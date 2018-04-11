using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golfballreturn : MonoBehaviour {

    //game object refferencres
    public GameObject golfball;
    public GameObject storage;
    public GameObject winlightingeffect1;
    public GameObject winlightingeffect2;
    public GameObject winlightingeffect3;
    public GameObject winlightingeffect4;
    public GameObject splasheffect;

    //timers
    public float delayMusic = 0;
    private float winEndTimer = 0;
    private float splashtimer = 0;
    private float rocktimer = 0;
    public float depth = -.85f;



    Vector3 initialpos = new Vector3();
    Vector3 lowerbound = new Vector3(-.15f, -.15f, -.15f);
     Vector3 upperbound = new Vector3(.5f, .15f, .15f);
     int timecounter = 0;
     int hitCounter = 0;
     int minhits;
     Rigidbody rb;
    private bool waterproof = true;
    private bool winconditionbool = false;
    private bool timermuicbool = false;
    private bool splashbool = false;
    private bool rockbool = false;


    //audio for the game, backgound music, and splashes.
    public AudioClip splashes1;
    public AudioClip splashes2;
    public AudioClip splashes3;
    public AudioClip splashes4;
    public AudioClip splashes5;
    public AudioClip scoremusic;
    AudioClip[] SplashesList = new AudioClip[5];



    // Use this for initialization
    void Awake () {
        storage.transform.position = golfball.transform.position;
        initialpos = golfball.transform.position;
        rb = golfball.GetComponent<Rigidbody>();
        SplashesList[0] = splashes1;
        SplashesList[1] = splashes2;
        SplashesList[2] = splashes3;
        SplashesList[3] = splashes4;
        SplashesList[4] = splashes5;

    }

    AudioClip selectClip()
    {
        int x = 0;
        x = Random.Range(0, 4);
        return SplashesList[(int)x];
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (golfball.transform.position.y <= -42.45 && waterproof)
        {
           
          
            waterproof = false;
            AudioSource.PlayClipAtPoint(selectClip(), golfball.transform.position);

        }


        if(golfball.transform.position.y <= storage.transform.position.y + depth)
        {

            golfball.transform.position = storage.transform.position;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            waterproof = true;
            
        }

        //starts timer for effects played on score
        if (winconditionbool)
        {
            winEndTimer += (float)Time.deltaTime;
        }
        //kills the effects after 5 seconds.
        if (winEndTimer >= 4f)
        {

            winlightingeffect1.SetActive(false);
            winlightingeffect2.SetActive(false);
            winlightingeffect3.SetActive(false);
            winlightingeffect4.SetActive(false);
            winconditionbool = false;
            winEndTimer = 0;
        }
        //end timer for effects



        //starts the timer for music delay
        if (timermuicbool)
        {
            delayMusic += (float)Time.deltaTime;
        }
        if (delayMusic >= .75f)
        {
            AudioSource.PlayClipAtPoint(scoremusic, GameObject.Find("Camera (ears)").transform.position); //score music 
            timermuicbool = false;
            delayMusic = 0;
        }
        //end timer for music

    }

    private void OnCollisionEnter(Collision collision)

    {

        if (rb.velocity.x > lowerbound.x && rb.velocity.y > lowerbound.y &&
    rb.velocity.z > lowerbound.z && rb.velocity.x < upperbound.x &&
    rb.velocity.y < upperbound.y && rb.velocity.z < upperbound.z && collision.gameObject.CompareTag("replaceHand"))
        {
            storage.transform.position = golfball.transform.position;
        }
            if (collision.gameObject.tag == "inHole")
        {
            timecounter = 0;
        }

        if(collision.gameObject.tag == "replacehand")
        {
            hitCounter++;
        }


        if (collision.gameObject.tag == "inHole")
        {

            //effects played on score

            //starts timers for music dely as well as effects delay.
            winconditionbool = true;
            timermuicbool = true;
            //moving the effects to the proper position
            winlightingeffect1.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + .75f, gameObject.transform.position.z);
            winlightingeffect2.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + .75f, gameObject.transform.position.z);
            winlightingeffect3.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + .75f, gameObject.transform.position.z);
            winlightingeffect4.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + .75f, gameObject.transform.position.z);
            //activating them to be played.
            winlightingeffect1.SetActive(true);
            winlightingeffect2.SetActive(true);
            winlightingeffect3.SetActive(true);
            winlightingeffect4.SetActive(true);




            if (hitCounter < minhits)
            {
                minhits = hitCounter;
            }
            hitCounter = 0;

            storage.transform.position = initialpos;
            golfball.transform.position = initialpos;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }



}




    void OnCollisionStay(Collision theCollision)
    {

        if (theCollision.gameObject.tag == "water")
        {
            golfball.transform.position = storage.transform.position;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }
       


    }


}
