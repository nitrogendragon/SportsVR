using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golfballreturn : MonoBehaviour {

    public float depth = -.85f;
    public int timeTillMove = 150;
    private Vector3 initialpos = new Vector3();
    private Vector3 storage = new Vector3();
    private Vector3 lowerbound = new Vector3(-.25f, -.25f, -.25f);
    private Vector3 upperbound = new Vector3(.25f, .25f, .25f);
    private int timecounter = 0;
    private int hitCounter = 0;
    private int minhits;
    private Rigidbody rb;


    // Use this for initialization
    void Awake () {
        storage = gameObject.transform.position;
        initialpos = gameObject.transform.position;
        rb = gameObject.GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {

        if(gameObject.transform.position.y <= storage.y + depth)
        {
            rb.velocity = Vector3.zero;
            gameObject.transform.position = storage;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "inHole")
        {
            timecounter = 0;
        }

        if(collision.gameObject.tag == "replacehand")
        {
            hitCounter++;
        }
    }


    void OnCollisionStay(Collision theCollision)
    {
        if (rb.velocity.x > lowerbound.x && rb.velocity.y > lowerbound.y &&
    rb.velocity.z > lowerbound.z && rb.velocity.x < upperbound.x &&
    rb.velocity.y < upperbound.y && rb.velocity.z < upperbound.z)
        {
            if (theCollision.gameObject.tag == "golfballcheck")
            {
                storage = gameObject.transform.position;
                
            }
        }

        if (theCollision.gameObject.tag == "inHole")
        {
            timecounter++;

            if (timecounter >= timeTillMove)
            {
                if (hitCounter < minhits)
                {
                    minhits = hitCounter;
                }
                hitCounter = 0;
                rb.velocity = Vector3.zero;
                gameObject.transform.position = initialpos;
            }
        }

    }


}
