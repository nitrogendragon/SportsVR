using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class BasketballGame : MonoBehaviour {
    public GameObject ballblocker;
    public GameObject scorecounter;
    public GameObject basketball;
    public GameObject basketball2;
    public GameObject basketball3;
    public GameObject basketball4;
    public GameObject basketball5;
    
    Vector3 bb1pos;
    Vector3 bb2pos;
    Vector3 bb3pos;
    Vector3 bb4pos;
    Vector3 bb5pos;

    Rigidbody rb;
    Rigidbody rb2;
    Rigidbody rb3;
    Rigidbody rb4;
    Rigidbody rb5;

    public int score;
    public float countdown;
    public int highscore;
    public Text countdowntext;
    public Text scoretext;
    public Text highscoretext;
    public bool startcd= false;
    // Use this for initialization
    void Awake () {
        score = 0;
        highscore = 0;
        countdown = 45;
        bb1pos = basketball.transform.position;
        bb2pos = basketball2.transform.position;
        bb3pos = basketball3.transform.position;
        bb4pos = basketball4.transform.position;
        bb5pos = basketball5.transform.position;
        rb = basketball.GetComponent<Rigidbody>();
        rb2 = basketball2.GetComponent<Rigidbody>();
        rb3 = basketball3.GetComponent<Rigidbody>();
        rb4 = basketball4.GetComponent<Rigidbody>();
        rb5 = basketball5.GetComponent<Rigidbody>();
        countdowntext.text = "" + ((int)countdown).ToString();
        scoretext.text = "Sc. " + score.ToString();
        highscoretext.text = "High " + highscore.ToString();
    }
     void updateText(int score, int highscore, float countdown)
    {
        if(startcd==true)
        countdowntext.text = "Time" + ((int)countdown).ToString();
        scoretext.text = "Score " + score.ToString();
        highscoretext.text = "High " + highscore.ToString();
        

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
            
            
            
            
            
            
            ballblocker.SetActive(true);
            startcd = false;
            
        }


    }
    
    public void dropblocker()
    {
        ballblocker.SetActive(false);
    }
    // Update is called once per frame
    void FixedUpdate () {
        if(startcd == true)
        {
            
            countdown -= Time.deltaTime;
            updateText(score, highscore, countdown);
            
        }
        }
        

 
}
