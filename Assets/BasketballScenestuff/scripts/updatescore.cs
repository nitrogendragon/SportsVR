using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/** Script focused on updating thje scoreboard in the basketball game particularly the highscore score and also updating the timer when you have started the game.**/
public class updatescore : MonoBehaviour {
    public GameObject basketballstartbutton;//reference to the start button sued to start the basketball game
    BasketballGame basketballscript;// reference the the basketball script that has the main components of the actual baskketball game held within it
    private GameObject Supdater;// reference to the object in the game that udpates the score when the ball colldier enters the score updaters collider
    // Use this for initialization
    void Awake () {
        basketballscript = basketballstartbutton.GetComponent<BasketballGame>();//references the script
        Supdater = GameObject.Find("ScoreUpdater");
         
    }
    
   

    // 1
    /** checks for trigger enter so when a mes hcollider hits a trigger collider. deals with updating the scores**/
    public void OnTriggerEnter(Collider other)
    {
        //check for scoreupdater collision and if the time remaining is greater than or equal to 10 and if the game is running
        if (other.tag == "scoreupdater" && basketballscript.countdown>=10 && basketballscript.startcd == true)
            //updates the displayed score
        {
            print("+2");
            basketballscript.score += 2;
            //checks to see if highscore should be updated and if so it does it
            if (basketballscript.highscore < basketballscript.score)
            {
                basketballscript.highscore += basketballscript.score - basketballscript.highscore;
                
            }
        }
        // if timer is below 10 seconds does the same things as the previous if statements with the eexceptrion o fadding 3 points instead of 2
        else if( other.tag == "scoreupdater" && basketballscript.startcd == true)
        {
            print("+3");
            basketballscript.score += 3;
            if (basketballscript.highscore < basketballscript.score)
            {
                basketballscript.highscore += basketballscript.score - basketballscript.highscore;

            }
        }
    }

}
