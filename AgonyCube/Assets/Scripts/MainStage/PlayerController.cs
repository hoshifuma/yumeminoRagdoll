using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgonyCubeMainStage {
    public class PlayerController : MonoBehaviour {

        CharacterController Controller;
        public float Speed;
        public float Gravity;
        Vector3 MoveDirection;
        private Animator animator;
        public Vector3 target;

        public bool checkPleyrMove = false;
        // Use this for initialization
        void Start() {
            Controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update() {

            if (checkPleyrMove == true) {
                if (Controller.isGrounded) {
                    MoveDirection = new Vector3(0, 0, 1);
                    MoveDirection = transform.TransformDirection(MoveDirection);
                    MoveDirection *= Speed;
                }
                MoveDirection.y -= Gravity * Time.deltaTime;
                Controller.Move(MoveDirection * Time.deltaTime);

                float distance = Mathf.Abs(target.x - transform.position.x);
                distance += Mathf.Abs(target.z - transform.position.z);

                if (distance <= 0.01 ) {
                    Vector3 pos = target;
                    pos.y = transform.position.y;
                    transform.position = pos;
                    checkPleyrMove = false;
                }
            }
        }

        public void SetPlayerTarget(GameObject gameObject) {
            target = gameObject.transform.position;


            Vector3 nexttarget;
            nexttarget = target;
            nexttarget.y = transform.position.y;
            transform.LookAt(nexttarget);
            checkPleyrMove = true;
        }
    }
}
