using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgonyCubeMainStage {
    public class CharaterController : MonoBehaviour {

        CharacterController Controller;

        public float Speed;
        public float Gravity;
        Vector3 MoveDirection;
        private Animator animator;
        public Transform target;


        // Use this for initialization
        void Start() {
            Controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update() {


            if (Controller.isGrounded) {
                MoveDirection = new Vector3(0, 0, 1);
                MoveDirection = transform.TransformDirection(MoveDirection);
                MoveDirection *= Speed;
            }
            //MoveDirection.y -= Gravity * Time.deltaTime;
            transform.LookAt(target);

            // Controller.Move(MoveDirection * Time.deltaTime);
        }

    }
}
