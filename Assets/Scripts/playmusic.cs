using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
/** simple fucntion for playing music**/
public class playmusic : MonoBehaviour
{
    
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();//gets audiosource component attached to gameobject script is attached to
        audio.Play();//play the audio 
        audio.Play(44100);//delay audio
    }
}