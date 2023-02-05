using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using freakingpig.gameplay;

namespace freakingpig
{
    [Serializable]
    public class PhaseRule
    {
        public float perc;
        public GameObject[] awakeables;
    }

    public class Spawner : MonoBehaviour
    {
        [SerializeField] private int counter = 0;
        [SerializeField] private PhaseRule[] rules = {};
        
        public void Eat()
        {
            counter++;
            foreach (PhaseRule rule in rules)
            {
                if (rule.perc >= counter / FieldCreator.Instance.FieldCount)
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
