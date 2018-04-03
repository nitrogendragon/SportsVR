using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basketballsounds : MonoBehaviour {
    public GameObject ground;
    public GameObject basketballmodel;
    public GameObject scoreupdater;
    public GameObject backboard;
    public AudioSource hitground;
    public AudioSource scored;
    public AudioSource hitsurface;
    
	// Use this for initialization
	void Start () {
		
	}
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject == ground || basketballmodel)
        {
            hitground.Play();
        }
        else if(other.gameObject == scoreupdater)
        {
            scored.Play();
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
