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
        public AudioClip music;
        public AudioClip voice;
    }

    public class Spawner : MonoBehaviour
    {
        [SerializeField] private int counter = 0;
        [SerializeField] private PhaseRule[] rules = {};
        private int cachedCount;
        private void Start()
        {
            cachedCount = FieldCreator.Instance.FieldCount;
        }

        public void Eat()
        {
            counter++;
            foreach (PhaseRule rule in rules)
            {
                if (rule.perc < counter / (float) cachedCount && !rule.awakeables[0].activeSelf)
                {
                    rule.awakeables[0].SetActive(true);
                    SPlayer.SwitchTrack(rule.music, .15f, .1f, 1);
                    SPlayer.PlaySFX(rule.voice, .35f);
                    break;
                }
            }
        }


    }
}
