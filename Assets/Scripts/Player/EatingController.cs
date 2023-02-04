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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Layer 3 is plants
                ContactFilter2D cf = new ContactFilter2D();
                cf.useLayerMask = true;
                cf.useTriggers = true;
                cf.layerMask = 1 << 3;

                Collider2D[] cols = new Collider2D[1];
                col.OverlapCollider(cf, cols);  
                
                if (cols.Length >= 1)
                {
                    Eat();
                }
            }
        }

        void Eat()
        {
            //ñam ñam
            Debug.Log("keikomido");
        }
    }
}
