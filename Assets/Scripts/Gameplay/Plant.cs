using freakingpig.holders;
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

        [SerializeField, AutoProperty] private SpriteRenderer rend;
        [SerializeField, ReadOnly] private PlantType plantType;
        private Transform playableArea;



        public void SetPlant(PlantType type)
        {
            plantType = type;
            rend.sprite = SelectSprite();
        }

        private Sprite SelectSprite()
        {
            return plantType switch
            {
                PlantType.Potato => SpriteHolder.Instance.potato,
                PlantType.Carrot => SpriteHolder.Instance.carrot,
                PlantType.Beet => SpriteHolder.Instance.beet,
                PlantType.Onion => SpriteHolder.Instance.onion,
                PlantType.Garlic => SpriteHolder.Instance.garlic,
                PlantType.Radish => SpriteHolder.Instance.radish,
                PlantType.Ginger => SpriteHolder.Instance.ginger,
                _ => throw new IndexOutOfRangeException("There are not enough enums in this switch"),
            };
        }

        public PlantType Eat()
        {
            GameObject obj = new();
            obj.transform.position = transform.position;
            obj.transform.parent = GameObject.FindGameObjectWithTag("PlayableArea").transform;
            AstarPath.active.Scan();
            PoolItself();
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
