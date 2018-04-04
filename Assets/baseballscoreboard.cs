using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class baseballscoreboard : MonoBehaviour {
   
    public GameObject bball;
    ballcontact bc;
    bool resetcount;
    public GameObject skeleton;
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
    Text[] texts;
    int i = 0;
    public int distancetraveled;
    Vector3 startspot;
    skeletonthrow st;

    public void updatescoreboard()
    {

       
        
            texts[i].text = distancetraveled.ToString();
        
            i++;
        
    }
     void Awake()
    {
        bc = bball.GetComponent<ballcontact>();
        texts = new Text[11];
        st = skeleton.GetComponent<skeletonthrow>();
        
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
        for (i = 0; i < 11; i++)
        {
            texts[i].text = "0";
        }
        i = 0;

    }
    // Update is called once per frame
    void Update()
    {
        if (st.count==10 && resetcount == false)
        {
            resetcount = true;
            print("11");
            
            for (i = 0; i < 11; i++)
            {
                texts[i].text = "0";
                print(texts[i].text);
            }
            i = 0;
        }
        if (st.count < 10)
        {
            resetcount = false;
        }
    }
}
