using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using freakingpig.gameplay;
using System.Windows.Threading;

namespace freakingpig
{
    public class EatingController : MonoBehaviour
    {
        public Collider2D col;
        [SerializeField] private bool buffStatus;
        [SerializeField] private string buffType;

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
                PlantType root = 0; //change
                
                if (cols.Length >= 1)
                {
                    Eat(root);
                }
            }
        }

        void Eat(PlantType root)
        {
            ActivateBuff();
            DelayAction(3000);
            DeactivateBuff();
        }


        void ActivateBuff()
        {
            buffStatus = true;

        }
        void DeactivateBuff()
        {
            buffStatus = false;
        }

        public static void DelayAction(int millisecond)
        {
            var timer = new DispatcherTimer();
            timer.Tick += delegate
            {
                
                timer.Stop();
            };

            timer.Interval = System.TimeSpan.FromMilliseconds(millisecond);
            timer.Start();
        }
    }
}
