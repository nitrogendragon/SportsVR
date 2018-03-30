using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonthrow : MonoBehaviour {


        public Animation anim;
        void Start()
        {
            anim = GetComponent<Animation>();
            foreach (AnimationState state in anim)
            {
                state.speed = 2F;
            }
        }
    private void Update()
    {
        anim.Play();
    }
}
