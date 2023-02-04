using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static freakingpig.pooling.PoolingManager;

namespace freakingpig.gameplay
{
    public class Plant : PoolableComponent
    {

        [SerializeField, ReadOnly] private PlantType plantType;
        public void SetPlant(PlantType type)
        {
            plantType = type;
        }

        public PlantType Eat()
        {
            return plantType;
        }

    }

    [Flags]
    public enum PlantType
    {
        Potato = 0,
        Carrot = 1 << 1,
        Beet = 1 << 2,
        Onion = 1 << 3,
        Garlic = 1 << 4,
        Radish = 1 << 5,
        Ginger = 1 << 6,
    }

}
