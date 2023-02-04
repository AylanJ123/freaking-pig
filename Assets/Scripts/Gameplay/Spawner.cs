using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freakingpig
{
    public class PhaseRule : MonoBehaviour
    {
        public float perc;
        public GameObject[] awakeables;
    }

    public class Spawner : MonoBehaviour
    {
        [SerializeField] private int counter = 0;
        [SerializeField] private float expectedPercent = 1;
        [SerializeField] private PhaseRule[] rules = {};
        
        void Eat()
        {
            counter++;
            foreach (PhaseRule rule in rules)
            {
                if (rule.perc >= counter / expectedPercent)
                {
                    foreach (GameObject item in rule.awakeables)
                    {
                        item.SetActive(true);
                    }
                    break;
                }
            }
        }


    }
}
