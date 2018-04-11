using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class baseballscoreboard : MonoBehaviour {
    //int referecnes for distances
    int homerun;
    int grandslam;
    int single;
    int foul;
    int doubler;
    int triple;
    public GameObject bball;//baseball reference
    ballcontact bc;//reference for ball contact script attached to baseball
    bool resetcount;//should we reset count?
    public GameObject skeleton;//pitcher reference
    //references to all the text elements in the game for displaying distances and how many of each type of hit has been made
    public Text d1;
    public Text d2;
    public Text d3;
    public Text d4;
    public Text d5;
    public Text d6;
    public Text d7;
    public Text d8;
    public Text d9;
    public Text d10;
    public Text d11;
    public Text hrtext;
    public Text gstext;
    public Text singletext;
    public Text doublertext;
    public Text tripletext;
    public Text foultext;
    Text[] texts;//lsit for holding all the text elements
    int i = 0;//for for loops and if statements
    public int distancetraveled;//distance traveled
    Vector3 startspot;//start spot
    skeletonthrow st;// pitching script reference
    //function for updating score
    public void updatescoreboard()
    {
        //make sure we are still pitching and if not reset to match throw count
        if (i == 11)
        {
            i = 0;
        }
        //resets scoreboard display
        if (i == 0)
        {
            homerun = 0;
            single = 0;
            doubler = 0;
            triple = 0;
            grandslam = 0;
            foul = 0;
            //sets up the text displayed
            foultext.text = foul.ToString();
            singletext.text = single.ToString();
            doublertext.text = doubler.ToString();
            tripletext.text = triple.ToString();
            hrtext.text = homerun.ToString();
            gstext.text = grandslam.ToString();
            // goes through all the texts and sets the disances to 0
            for (i = 0; i < 11; i++)
                {
                    texts[i].text = "0";

                }
                i = 0;
        }
        //these checks are just for based on distance traveled value determining which text to update the count for and then updating it
        if(distancetraveled== -1)
        {
            foul++;
            foultext.text = foul.ToString();
        }
        else if (distancetraveled < 20 && distancetraveled>=0)
        {
            single++;
            singletext.text = single.ToString();
        }
        else if(distancetraveled>20 && distancetraveled <= 60)
        {
            doubler++;
            doublertext.text = doubler.ToString();
        }
        else if (distancetraveled > 60 && distancetraveled <= 150)
        {
            triple++;
            tripletext.text = triple.ToString();
        }
        else if (distancetraveled > 150 && distancetraveled <= 220)
        {
            homerun++;
            hrtext.text = homerun.ToString();
        }
        else if (distancetraveled > 220)
        {
            grandslam++;
            gstext.text = grandslam.ToString();
        }


        texts[i].text = distancetraveled.ToString();
        
            i++;
        
    }
     void Awake()
    {
        bc = bball.GetComponent<ballcontact>();//script reference
        texts = new Text[11];//sets size of list
        st = skeleton.GetComponent<skeletonthrow>();//script reference
        //fillss list references
        texts[0] = d1;
        texts[1] = d2;
        texts[2] = d3;
        texts[3] = d4;
        texts[4] = d5;
        texts[5] = d6;
        texts[6] = d7;
        texts[7] = d8;
        texts[8] = d9;
        texts[9] = d10;
        texts[10] = d11;
        //makes sure everythign is zero on start
        for (i = 0; i < 11; i++)
        {
            texts[i].text = "0";
        }
        //makes sure i =0 to start;
        i = 0;

    }
    // Update is called once per frame
    void Update()
    {
        
        
    }
}
