using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicplayer : MonoBehaviour {



	// Use this for initialization
	void Start()
    {
        //AudioSource.PlayClipAtPoint(music1, GameObject.Find("Plane").transform.position, 1);
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        audio.Play(44100);
    }
	
	// Update is called once per frame
	void Update ()
    {
       
    }
}
