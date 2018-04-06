using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class mainmenucontroller : MonoBehaviour
{
    
   
   
   
    bool gamestarted = false;
    bool startselected = false;
    bool exitselected = false;
    
    
    public GameObject begingame;
    public GameObject exit;
    public GameObject startbox;
    
    private SteamVR_TrackedObject trackedObj;
    // 1
   

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
      
        trackedObj = GetComponent<SteamVR_TrackedObject>();
       
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == begingame)
        {
            begingame.GetComponent<Renderer>().material.color = Color.blue;
            startselected = true;
        }
        if (other.gameObject == exit)
        {
            exit.GetComponent<Renderer>().material.color = Color.cyan;
            exitselected = true;

        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == begingame)
        {
            begingame.GetComponent<Renderer>().material.color = Color.white;
            startselected = false;


        }
        if (other.gameObject == exit)
        {
            exit.GetComponent<Renderer>().material.color = Color.white;
            exitselected = false;
        }
        


    }

    private void loadbasketball()
    {
        SceneManager.LoadScene("Basketball");
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (Controller.GetHairTriggerDown() && exitselected == true)
        {
            exitselected = false;
            exit.GetComponent<Renderer>().material.color = Color.white;
            exitgame();


        }

    }
}
