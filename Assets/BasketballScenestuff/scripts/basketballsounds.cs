using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/** script that handles the playing of sounds in the basketball game including basic sound effects**/
public class basketballsounds : MonoBehaviour
{
    //referenced gameobjects,
    public GameObject ground;
    public GameObject basketballmodel;
    public GameObject scoreupdater;
    public GameObject backboard;
    float volumemodify;//modifier for sound volume
    //references for sound files
    public AudioClip hitground1;
    public AudioClip scored1;
    public AudioClip hitbackboard1;
    AudioSource audio;//holds audio clips and plays  them if desired

    
   //checks to see what the basketball collided with and then plays the sound effects accordingly
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
            AudioSource.PlayClipAtPoint(hitbackboard1, gameObject.transform.position, .3f);
        }
        /**else
        {
            if (gameObject.GetComponent<Rigidbody>().velocity.x < .7f && gameObject.GetComponent<Rigidbody>().velocity.x > -.7f
                && gameObject.GetComponent<Rigidbody>().velocity.z < .7f && gameObject.GetComponent<Rigidbody>().velocity.z > -.7f)
            {
                return;
            }
                if ( gameObject.GetComponent<Rigidbody>().velocity.x<1 && gameObject.GetComponent<Rigidbody>().velocity.x > -1 
                && gameObject.GetComponent<Rigidbody>().velocity.z < 1 && gameObject.GetComponent<Rigidbody>().velocity.z > -1)
            {
                //print("slowenough");
                audio.volume =.1f;
                AudioSource.PlayClipAtPoint(hitground1, gameObject.transform.position, audio.volume);
            }
            else
            {
               // print("normal");
                AudioSource.PlayClipAtPoint(hitground1, gameObject.transform.position);
            }
           
        }**/
    }
    // same as other sound checks only difference is trigger collision instead of mesh collider on mesh collider
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == scoreupdater)
        {
            // scored.Play();
            AudioSource.PlayClipAtPoint(scored1, gameObject.transform.position);//plays clip at the position of the gameobject for more realistic sound effects
        }
    }

}
