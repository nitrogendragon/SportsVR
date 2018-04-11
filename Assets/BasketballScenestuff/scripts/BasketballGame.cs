using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
/**main component of the basketball game, deals with game music, basketballs information storage, scoreboard display, and resetting balls**/
public class BasketballGame : MonoBehaviour {
    public GameObject ballblocker;//stops balls from moving
    public GameObject scorecounter;//updates score when balls hit it
    //basketball references
    public GameObject basketball;
    public GameObject basketball2;
    public GameObject basketball3;
    public GameObject basketball4;
    public GameObject basketball5;
    //lsit for holding balls
    GameObject[] balls;
    //lsit for holding the balls intial positions
    Vector3[] ballpositions;
    public AudioClip gamemusic;//reference to game music
    Vector3 bb1pos;
    Vector3 bb2pos;
    Vector3 bb3pos;
    Vector3 bb4pos;
    Vector3 bb5pos;
    //rigidbody references
    Rigidbody rb;
    Rigidbody rb2;
    Rigidbody rb3;
    Rigidbody rb4;
    Rigidbody rb5;
    Rigidbody[] rbs;//list to hold rigidbodies
    public int score;//score int variable
    public float countdown;//countdown value
    public int highscore;//highscore value
    //text references for displaying scores and time remaining on game
    public Text countdowntext;
    public Text scoretext;
    public Text highscoretext;
    public bool startcd= false;// bool for checking if the game has started
    // Use this for initialization
    void Awake () {
        rbs = new Rigidbody[5];//sets size of list
        //gets rigidnbody references
        rb = basketball.GetComponent<Rigidbody>();
        rb2 = basketball2.GetComponent<Rigidbody>();
        rb3 = basketball3.GetComponent<Rigidbody>();
        rb4 = basketball4.GetComponent<Rigidbody>();
        rb5 = basketball5.GetComponent<Rigidbody>();
        //stores rigidbodies in lsit
        rbs[0] = rb;
        rbs[1] = rb2;
        rbs[2] = rb3;
        rbs[3] = rb4;
        rbs[4] = rb5;
        balls = new GameObject[5];//sets size of list
        //stores balls in list
        balls[0] = basketball;
        balls[1] = basketball2;
        balls[2] = basketball3;
        balls[3] = basketball4;
        balls[4] = basketball5;
        ballpositions = new Vector3[5];//sets size for list
        //finds initial positions of balls and sets them 
        bb1pos = basketball.transform.position;
        bb2pos = basketball2.transform.position;
        bb3pos = basketball3.transform.position;
        bb4pos = basketball4.transform.position;
        bb5pos = basketball5.transform.position;
        //stores inital positions in list
        ballpositions[0] = bb1pos;
        ballpositions[1] = bb2pos;
        ballpositions[2] = bb3pos;
        ballpositions[3] = bb4pos;
        ballpositions[4] = bb5pos;
        //initial values
        score = 0;
        highscore = 0;
        countdown = 45;
        
        //intial text setup
        countdowntext.text = "Time" + ((int)countdown).ToString();
        scoretext.text = "Sc. " + score.ToString();
        highscoretext.text = "High " + highscore.ToString();
    }
    //function for resetting bvasketball positions
    void Restoreposition()
    {
        //gets all balls and resets their positions to their initial positon if they get too far away from it and also resets their velocity
        for (int i = 0; i < 4; i++)
        {
            if (Vector3.Distance(ballpositions[i], balls[i].transform.position) >= 5)
            {
                rbs[i].velocity = Vector3.zero;
                balls[i].transform.position = ballpositions[i];
            }
        }
    }
    //function for updating text when displayed on scoreboard, also resets balls when game ends
    void updateText(int score, int highscore, float countdown)
    {
        //if the game is started set text to whatever the values currently are
        if(startcd==true)
        countdowntext.text = "Time" + ((int)countdown).ToString();
        scoretext.text = "Score " + score.ToString();
        highscoretext.text = "High " + highscore.ToString();
        
        //if game ended reset balls to initialpoints
        if (countdown <= 0 && startcd ==true)
        {
            
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            basketball.transform.position = bb1pos;

            rb2.velocity = Vector3.zero;
            rb2.angularVelocity = Vector3.zero;
            basketball2.transform.position = bb2pos;

            rb3.velocity = Vector3.zero;
            rb3.angularVelocity = Vector3.zero;
            basketball3.transform.position = bb3pos;

            rb4.velocity = Vector3.zero;
            rb4.angularVelocity = Vector3.zero;
            basketball4.transform.position = bb4pos;

            rb5.velocity = Vector3.zero;
            rb5.angularVelocity = Vector3.zero;
            basketball5.transform.position = bb5pos;
            
            
            
            
            
            //reset ballblocker to keep balls from roling down again.
            ballblocker.SetActive(true);
            startcd = false;//game not started
            
        }


    }
    //drops blocker to let balls rolls down by setting it to be inactive, also starts game music
    public void dropblocker()
    {
        ballblocker.SetActive(false);
        AudioSource.PlayClipAtPoint(gamemusic, GameObject.Find("basketballgamemodel").transform.position);
    }
    // Update is called once per frame
   
    void FixedUpdate () {
        //if game is running start countdown timer and run updateText function and RestorePosition function 
        if(startcd == true)
        {
            
            countdown -= Time.deltaTime;
            updateText(score, highscore, countdown);
            
        }
        Restoreposition();
        }
        

 
}
