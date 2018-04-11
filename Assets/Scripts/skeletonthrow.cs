using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/** script tha tin general handles animation of the pitcher and throwing the balls**/
public class skeletonthrow : MonoBehaviour {
    bool threw = false;//check for if the ball has been thrown
    public bool throwagain;//check for if the ball should be thrown again
    public int count;//counter
    float throwdelay;//timer reference
    public GameObject baseball;//baseball
    GameObject instantiatedball;//sets up instantiation to be used later
    Rigidbody rb;//rigidbody reference
        public Animation anim;
        void Awake()
        {
        count = 0;//throw counter is 0 to start since no pitches have been thrown
        throwagain = false;//dont throw
            anim = GetComponent<Animation>();//gets component for animation
            foreach (AnimationState state in anim)
            {
                state.speed = 2.1F;//sets speed of all animation clips
            }
        }


    

    private void Update()
    {

         //checks to see if the ball should be thrown again
            if(count<=10 && throwagain == true)
        {
            throwdelay += Time.deltaTime;//starts delay throw timer
            if (throwdelay >= .55f)//time to make new check
            {
                throwdelay = 0;//reset timer
                anim.Play();//plays pitcher animation
                threw = true;//has thrown
                throwagain = false;//should not throwagain until further checks
            }

        }
            //restarts throwdelay timer
        if (threw == true && throwdelay<=.7) { 
        throwdelay += Time.deltaTime;
        }
        //another time and bool check, used to allign animation of pitcher with instantiation of ball for a little more realism  os as to not throw off the player
        if (throwdelay >= .7 && threw == true)
        {
            throwdelay = 0;
            threw = false;
            //checks for the ball instantiation
            if (instantiatedball)
            {
                //makes sure the ball does get destroyed before another pitch
                Destroy(instantiatedball);
            }
            //instantiates new ball at specified coordinates
            instantiatedball = Instantiate(baseball, new Vector3(.6f,1f,.3f), new Quaternion(0,0,0,1));
            //finds the rigidbody attached
            rb = instantiatedball.GetComponent<Rigidbody>();
            //sets the ball to be active in the game scene
            instantiatedball.SetActive(true);
            //applies force to throw ball
            rb.AddForce(new Vector3(-3000+Random.Range(-200,0),290+Random.Range(-10,0),0));
            
        }

        
    }
}
