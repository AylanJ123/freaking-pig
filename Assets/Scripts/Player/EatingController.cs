using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freakingpig
{
    public class EatingController : MonoBehaviour
    {
        public Collider2D col;

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)){
                //Layer 3 is plants
                if(col.IsTouchingLayers(3)){
                    Eat();
                }
            }
        }

        void Eat(){
            //ñam ñam
        }
    }
}
