using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace AgonyCube.MainStage {
    public class PlayerGoal : MonoBehaviour
    {
        private Animator animator;
        public GameObject Parent;
        public GameObject Goal;

        private void Start()
        {

            animator = GetComponent<Animator>();
            
        }

        private void Update()
        {
            
        }


    }  
}