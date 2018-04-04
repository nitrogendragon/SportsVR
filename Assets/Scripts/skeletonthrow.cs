using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonthrow : MonoBehaviour {
    bool threw = false;
    public bool throwagain;
    public int count;
    float throwdelay;
    public GameObject baseball;
    GameObject instantiatedball;
    Rigidbody rb;
        public Animation anim;
        void Awake()
        {
        count = 0;
        throwagain = true;
            anim = GetComponent<Animation>();
            foreach (AnimationState state in anim)
            {
                state.speed = 2.1F;
            }
        }


    

    private void Update()
    {

         
            if(count<=10 && throwagain == true)
        {
            
            anim.Play();
            threw = true;
            throwagain = false;

        }
        if (threw == true && throwdelay<=.8) { 
        throwdelay += Time.deltaTime;
        }
        if (throwdelay >= .8 && threw == true)
        {
            throwdelay = 0;
            threw = false;
            if (instantiatedball)
            {
                Destroy(instantiatedball);
            }
            instantiatedball = Instantiate(baseball, new Vector3(-1,1,0), new Quaternion(0,0,0,1));
            
            rb = instantiatedball.GetComponent<Rigidbody>();
            instantiatedball.SetActive(true);
            rb.AddForce(new Vector3(-3000,300,0));
            
        }

        
    }
}
