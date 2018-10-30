using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgonyCubeMainStage {
    public class StepController : MonoBehaviour {

        public GameObject Wall;
        public GameObject Floor;
        public GameObject[] HitCheckCollider;
        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {



        }

        public bool StepMoveCheck() {

            if (Floor.transform.position.y <= (transform.position.y + 0.5)) {
                //床が下にある場合
                if (Wall.GetComponent<Wall>().FloorCheck == false) {
                    //Ray stepray = new Ray(transform.position, -transform.forward);
                    //RaycastHit hit;


                    //if (Physics.Raycast(stepray, out hit, 3, PlayerLayer)) {
                    if(HitCheckCollider[0].GetComponent<HitCheckCollider>().PlayerHit == true || 
                        HitCheckCollider[1].GetComponent<HitCheckCollider>().PlayerHit == true) { 
                       
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    return false;
                }

            }
            else {
                return false;
            }
        }

        public GameObject StayStepMove(int frontandbehind) {
            if(!(HitCheckCollider[frontandbehind].GetComponent<HitCheckCollider>().HitBlock == null)) {
                return HitCheckCollider[frontandbehind].GetComponent<HitCheckCollider>().HitBlock;
            }
            else {
                return null;
            }
        }

    }
}