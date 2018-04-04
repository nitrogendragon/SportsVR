using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basketballsounds : MonoBehaviour
{
    public GameObject ground;
    public GameObject basketballmodel;
    public GameObject scoreupdater;
    public GameObject backboard;
   
    public AudioClip hitground1;
    public AudioClip scored1;
    public AudioClip hitbackboard1;
    

    // Use this for initialization
   
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == ground || basketballmodel)
        {
           // hitground.Play();
            AudioSource.PlayClipAtPoint(hitground1, gameObject.transform.position);
        }
        
        if (other.gameObject == backboard)
        {
           // hitbackboard.Play();
            AudioSource.PlayClipAtPoint(hitbackboard1, gameObject.transform.position);
        }
        else
        {
            AudioSource.PlayClipAtPoint(hitground1, gameObject.transform.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == scoreupdater)
        {
            // scored.Play();
            AudioSource.PlayClipAtPoint(scored1, gameObject.transform.position);
        }
    }

}
