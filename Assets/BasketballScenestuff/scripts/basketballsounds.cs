using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basketballsounds : MonoBehaviour
{
    public GameObject ground;
    public GameObject basketballmodel;
    public GameObject scoreupdater;
    public GameObject backboard;
    public AudioClip hitground;
    public AudioClip scored;
    public AudioClip hitbackboard;
    AudioSource a1;
    AudioSource a2;
    AudioSource a3;

    // Use this for initialization
    void Awake()
    {
        a1.clip = hitground;
        a2.clip = scored;
        a3.clip = hitbackboard;
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == ground || basketballmodel)
        {
            a1.Play();
        }
        else if (other.gameObject == scoreupdater)
        {
            a2.Play();
        }
        else if (other.gameObject == backboard)
        {
            a3.Play();
        }
    }
  
}
