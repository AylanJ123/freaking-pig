using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using freakingpig.gameplay;
using System;
using MyBox;
using freakingpig.controllers;

namespace freakingpig
{
    public class EatingController : MonoBehaviour
    {
        public Collider2D col;
        public Spawner spawner;
        private readonly Dictionary<PlantType, Buff> buffs = new(7);
        public ParticleSystem eatParticle;
        [SerializeField, AutoProperty] private MovementController controller;

        private void Start()
        {
            foreach (PlantType type in Enum.GetValues(typeof(PlantType))) buffs.Add(type, new Buff());
            eatParticle = transform.GetChild(1).GetComponent<ParticleSystem>();
        }

        void Update()
        {
            foreach (KeyValuePair<PlantType, Buff> pair in buffs)
                if (pair.Value.active && pair.Value.wornOffTime < Time.time)
                {
                    pair.Value.active = false;
                    WearBuffOff(pair.Key);
                }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ContactFilter2D cf = new();
                cf.useLayerMask = true;
                cf.useTriggers = true;
                cf.layerMask = 1 << 3;
                Collider2D[] cols = new Collider2D[1];
                col.OverlapCollider(cf, cols);
                if (cols[0] != null)
                    if (cols[0].TryGetComponent(out Plant plant))
                        Eat(plant.Eat());
            }
        }

        void Eat(PlantType root)
        {
            eatParticle.Play();
            spawner.Eat();
            FieldCreator.Instance.FieldCount--;
            float time = WearBuffOn(root);
            if (time == 0) return;
            buffs[root].wornOffTime = time + Time.time;
            buffs[root].active = true;
        }

        private float WearBuffOn(PlantType type)
        {
            switch (type)
            {
                case PlantType.Potato:
                    return 0;
                case PlantType.Carrot:
                    return 5;
                case PlantType.Beet:
                    controller.SetMaxStamina(150);
                    return 8;
                case PlantType.Onion:
                    return 3;
                case PlantType.Garlic:
                    return 4;
                case PlantType.Radish:
                    return 0;
                case PlantType.Ginger:
                    return 4;
                default:
                    throw new IndexOutOfRangeException("There are not enough enums in this switch");
            }
        }

        private void WearBuffOff(PlantType type)
        {
            switch (type)
            {
                case PlantType.Potato:
                    break;
                case PlantType.Carrot:
                    break;
                case PlantType.Beet:
                    controller.SetMaxStamina(50);
                    break;
                case PlantType.Onion:
                    break;
                case PlantType.Garlic:
                    break;
                case PlantType.Radish:
                    break;
                case PlantType.Ginger:
                    break;
                default:
                    throw new IndexOutOfRangeException("There are not enough enums in this switch");
            }
        }

        private class Buff
        {
            public float wornOffTime;
            public bool active;
        }

    }
}
