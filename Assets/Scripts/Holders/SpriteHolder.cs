using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freakingpig.holders
{
    [CreateAssetMenu(fileName = "spHolder", menuName = "ScriptableObjects/SpriteHolder")]
    public class SpriteHolder : ScriptableObject
    {
        private static SpriteHolder _instance;
        public static SpriteHolder Instance
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

        public Sprite potato;
        public Sprite carrot;
        public Sprite beet;
        public Sprite onion;
        public Sprite garlic;
        public Sprite radish;
        public Sprite ginger;

    }
}
