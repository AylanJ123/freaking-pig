using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freakingpig.holders
{
    [CreateAssetMenu(fileName = "sHolder", menuName = "ScriptableObjects/SoundHolder")]
    public class SoundHolder : ScriptableObject
    {
        private static SoundHolder _instance;
        public static SoundHolder Instance
        {
            get
            {
                return _instance;
            }
            set
            {
                if (_instance == null) _instance = value;
                else throw new AccessViolationException("This will be set only once at start-up, what are you trying to do????");
            }
        }

        public AudioClip mouseHover;
        public AudioClip mouseClick;
        public AudioClip introSound;
        public AudioClip gameStart;
        public AudioClip gameMid;
        public AudioClip gameGameEnd;
        public AudioClip mainMenuMusic;
        public AudioClip eat1;
        public AudioClip eat2;
        public AudioClip eat3;
    }
}
