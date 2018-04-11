using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ballcontact : MonoBehaviour {
    public GameObject ground;//reference to ground or in bounds plane
    public GameObject OB;//reference to out of bounds plane
    public GameObject PlayerBox;// reference to plane tha tplayer can teleport on
    public GameObject initialpoint;// reference to object used for calculating distance ball travels
    public GameObject skeleton;//reference to skeleton pitcher taken from turbosquid
    public GameObject scoreboard;// reference to scoreboard
    baseballscoreboard bsb;//reference to scoreboard script
    public int distancetraveled;//distance ball travels integer variable
    Vector3 startspot;// the actual point where the initial point is
    skeletonthrow st;//pitching script reference
    //collsion enter checks
    private void OnCollisionEnter(Collision other)
    {
        //if out of bounds distance sets to -1 and updates scoreboard with value and type of hit, in this case it is foul;
        if(other.gameObject == OB)
        {
            bsb.distancetraveled = -1;
            bsb.updatescoreboard();
            //if then dont throw again and reset count for how many balls have been thrown
            if (st.count == 10)
            {
                st.count = 0;
                st.throwagain = false;

            }
            //otherwise throwagain and increment count and destroy ball
            else
            {

                st.count += 1;
                st.throwagain = true;
            }
            Destroy(gameObject);
        }
        //if we hit the ground then update the distance and set the scoreboard scripts distance to distancetraveled and update the scorebaords values and in general what it displays
        else if(other.gameObject == ground)
        {
            distancetraveled = (int)Vector3.Distance(gameObject.transform.position, startspot);
            bsb.distancetraveled = distancetraveled;

            bsb.updatescoreboard();
            //check throws count and if then reset count and dont throw again
            if (st.count == 10)
            {
                st.count = 0;
                st.throwagain = false;
                
            }
            //otherwise thow again, increment count and destroy the ball
            else
            {
                
                st.count += 1;
                st.throwagain = true;
            }
            Destroy(gameObject);
            
        }
        //if it manages to hit the playerbox dont do anything but destroy the ball and say throw another one, basically makes sure awful pitches don't get considered
        else if (other.gameObject == PlayerBox)
        {
            st.throwagain = true;
            Destroy(gameObject);
        }
    }
    private void Awake()
    {
        //actual references to script components
        bsb = scoreboard.GetComponent<baseballscoreboard>();
        st = skeleton.GetComponent<skeletonthrow>();
        startspot = initialpoint.transform.position;//sets startspot to position of initial point
        
        
    }
    // Update is called once per frame
    void Update () {
        //honestly not sure what this was supposed to be but it hasn't broken  the game it seems.
       if(st.count ==0 && st.throwagain == true)
        {
            
        }
    }
}
