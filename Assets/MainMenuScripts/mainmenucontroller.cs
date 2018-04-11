using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/** script that deals with the whole setup of our main menu interaction. mostly just a lot of deactivating and reactivating buttons and text and then 3 buttons for loading the levels and one for closing the game**/
public class mainmenucontroller : MonoBehaviour
{




    

    public GameObject exitgames;//exitgame button
    public GameObject help;//help button
    public GameObject goback;// return to level 1 of main menu
    public GameObject goback2;// return to help selection
    public GameObject startbaseball;//startsbaseball
    public GameObject startbasketball;//startsbasketball
    public GameObject startminigolf;//startsminigolf
    public GameObject showbasketballhelp;//shows basketball help
    public GameObject showbaseballhelp;//shows baseball help
    public GameObject showminigolfhelp;//shows minigolf help
    public GameObject minigolfhelp;// minigolf controls
    public GameObject basketballhelp;// basketball controls
    public GameObject baseballhelp;//baseball controls
    GameObject[] buttons;//list of buttons
    //bools related to game objects above
    bool exitbool;
    bool helpbool;
    bool gobackbool;
    bool goback2bool;
    bool startbaseballbool;
    bool startbasketballbool;
    bool startminigolfbool;
    bool showbasketballbool;
    bool showbaseballbool;
    bool showminigolfbool;
    bool minigolfhelpbool;
    bool basketballhelpbool;
    bool baseballhelpbool;
    bool[] bools;//list for holding bools
    private SteamVR_TrackedObject trackedObj;//controller reference
    // 1
   
        //grabs specific controller
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        // sets up list reference for all the buttons and text objects
        buttons = new GameObject[13];
        buttons[0] = help;
        buttons[1] = exitgames;
        buttons[2] = baseballhelp;
        buttons[3] = basketballhelp;
        buttons[4] = minigolfhelp;
        buttons[5] = showbaseballhelp;
        buttons[6] = showbasketballhelp;
        buttons[7] = showminigolfhelp;
        buttons[8] = startbaseball;
        buttons[9] = startbasketball;
        buttons[10] = startminigolf;
        buttons[11] = goback;
        buttons[12] = goback2;
        //sets all to be   not active
        for(int i =0; i< buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
        //sets active the initial buttons to be shown
        help.SetActive(true);
        exitgames.SetActive(true);
        startbaseball.SetActive(true);
        startbasketball.SetActive(true);
        startminigolf.SetActive(true);
        //sets up bools list references
        bools = new bool[13];
        bools[0] = helpbool;
        bools[1] = exitbool;
        bools[2] = baseballhelpbool;
        bools[3] = basketballhelpbool;
        bools[4] = minigolfhelpbool;
        bools[5] = showbaseballbool;
        bools[6] = showbasketballbool;
        bools[7] = showminigolfbool;
        bools[8] = startbaseballbool;
        bools[9] = startbasketballbool;
        bools[10] = startminigolfbool;
        bools[11] = gobackbool;
        bools[12] = goback2bool;
        //sets all to false
        for(int i =0; i < bools.Length; i++)
        {
            bools[i] = false;
        }
        //sets trackedobject to be the controller
        trackedObj = GetComponent<SteamVR_TrackedObject>();
       
    }
    //checks if we are triggering any of the buttons
    public void OnTriggerEnter(Collider other)
    {
        for(int i=0; i < buttons.Length; i++)
        {
            //if we are then set their respective bool to true and change the button color
            if(other.gameObject == buttons[i])
            {
                bools[i] = true;
                buttons[i].GetComponent<Renderer>().material.color = Color.blue;
            }
        }
       

    }
    //if we no longer are triggering a button
    public void OnTriggerExit(Collider other)
    {
        //find the button
        for (int i = 0; i < buttons.Length; i++)
        {
            //no longer triggering so change bool to false aand reset color
            if (other.gameObject == buttons[i])
            {
                bools[i] = false;
                buttons[i].GetComponent<Renderer>().material.color = Color.white;
            }




        }
    }
    //function for resetting all button colors
    void resetbuttoncolor()
    {
        for(int i =0; i< buttons.Length; i++)
        {
            buttons[i].GetComponent<Renderer>().material.color = Color.white;
        }
    }
    //function for loading basketball game
     void loadbasketball()
    {

        SceneManager.LoadScene("Basketball");
    }
    //function for loading baseball game
         void loadbaseball()
        {
            SceneManager.LoadScene("Baseball");
        }
    //function for loading minigolf game
        void loadminigolf()
        {
            SceneManager.LoadScene("workinggolf");
        }
    //function for loading help
        void loadhelp()
        {
        // hide all buttons and make sure game doesn't think any of them are being triggered still
            for(int i =0; i<buttons.Length; i++)
            {
                buttons[i].SetActive(false);
                bools[i] = false;
            }
            //sets up buttons to be displayed for new menu
            goback.SetActive(true);
            showbaseballhelp.SetActive(true);
            showbasketballhelp.SetActive(true);
            showminigolfhelp.SetActive(true);

        }
    //function for going back to main main menu
        void loadgoback()
        {
        //same as previous for loop, will continue through other functions so just note that
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
                bools[i] = false;
            }
            //again same things going on just setting relevant buttons and text to be visible/active again
            help.SetActive(true);
            startbaseball.SetActive(true);
            startbasketball.SetActive(true);
            startminigolf.SetActive(true);
            exitgames.SetActive(true);
        }
    // function for retuirning to secondary menu
        void loadgoback2()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
                bools[i] = false;
            }
            goback.SetActive(true);
            showminigolfhelp.SetActive(true);
            showbasketballhelp.SetActive(true);
            showbaseballhelp.SetActive(true);
        }
    //function for showing help text for baseball
        void loadbaseballhelp()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
                bools[i] = false;
            }
            goback2.SetActive(true);
            baseballhelp.SetActive(true);
        }
    //function for showing help text for basketbal
        void loadbasketballhelp()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
                bools[i] = false;
            }
            goback2.SetActive(true);
            basketballhelp.SetActive(true);
        }
    //function for showing help text for minigolf
    void loadminigolfhelp()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
                bools[i] = false;
            }
            goback2.SetActive(true);
            minigolfhelp.SetActive(true);
        }


    /**
    buttons[0] = help;
    buttons[1] = exitgames;
    buttons[2] = baseballhelp;
    buttons[3] = basketballhelp;
    buttons[4] = minigolfhelp;
    buttons[5] = showbaseballhelp;
    buttons[6] = showbasketballhelp;
    buttons[7] = showminigolfhelp;
    buttons[8] = startbaseball;
    buttons[9] = startbasketball;
    buttons[10] = startminigolf;
    buttons[11] = goback;
    buttons[12] = goback2;
    **/
    //function for quitting game
    void loadexit()
    {
            Application.Quit();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //safety check to reset colors whenever trigger is pressed
        if (Controller.GetHairTriggerDown() )
        {
            resetbuttoncolor();
        }
        //trigger pulled reset relevant bool and run relevant function, persists for all these next if statements
        if (Controller.GetHairTriggerDown() && bools[0] == true)
            {
                bools[0] = false;
                
                loadhelp();
            }
            else if (Controller.GetHairTriggerDown() && bools[1] == true)
            {
                bools[1] = false;

                loadexit();
            }
            else if (Controller.GetHairTriggerDown() && bools[2] == true)
            {
                bools[2] = false;
                
            }
            else if (Controller.GetHairTriggerDown() && bools[3] == true)
            {
                bools[3] = false;
            }
            else if (Controller.GetHairTriggerDown() && bools[4] == true)
            {
                bools[4] = false;
            }
            else if (Controller.GetHairTriggerDown() && bools[5] == true)
            {
                bools[5] = false;
                loadbaseballhelp();
            }
            else if (Controller.GetHairTriggerDown() && bools[6] == true)
            {
                bools[6] = false;
                loadbasketballhelp();
            }
            else if (Controller.GetHairTriggerDown() && bools[7] == true)
            {
                bools[7] = false;
                loadminigolfhelp();
            }
            else if (Controller.GetHairTriggerDown() && bools[8] == true)
            {
                bools[8] = false;
                loadbaseball();
            }
            else if (Controller.GetHairTriggerDown() && bools[9] == true)
            {
                bools[9] = false;
                loadbasketball();
            }
            else if (Controller.GetHairTriggerDown() && bools[10] == true)
            {
                bools[10] = false;
                loadminigolf();
            }
            else if (Controller.GetHairTriggerDown() && bools[11] == true)
            {
                bools[11] = false;
                loadgoback();
            }
            else if (Controller.GetHairTriggerDown() && bools[12] == true)
            {
                bools[12] = false;
                loadgoback2();
            }





        }
}
