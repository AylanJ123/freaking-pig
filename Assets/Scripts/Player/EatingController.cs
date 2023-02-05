using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using freakingpig.gameplay;
using System;
using MyBox;
using freakingpig.controllers;
using freakingpig.holders;
using UnityEngine.SceneManagement;

namespace freakingpig
{
    public class EatingController : MonoBehaviour
    {
        public Collider2D col;
        public Spawner spawner;
        private readonly Dictionary<PlantType, Buff> buffs = new(7);
        public ParticleSystem eatParticle;
        public static int CropsEaten = 0;
        [SerializeField, AutoProperty] private MovementController controller;
        [SerializeField, AutoProperty] private Health health;

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
            float rand = UnityEngine.Random.value;
            SPlayer.PlaySFX(rand < .33f ? SoundHolder.Instance.eat1 : rand < .66f ? SoundHolder.Instance.eat2 : SoundHolder.Instance.eat3, .05f);
            eatParticle.Play();
            spawner.Eat();
            FieldCreator.Instance.FieldCount--;
            if (FieldCreator.Instance.FieldCount <= 0)
            {
                Transitions.Transition(1, 0, () =>
                {
                    foreach (Plant plant in FindObjectsOfType<Plant>()) plant.PoolItself();
                    SceneManager.LoadScene("WinMenu", LoadSceneMode.Single);
                    SPlayer.SwitchTrack(SoundHolder.Instance.gameStart, .3f, .2f);
                }
            );
            }
            CropsEaten++;
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
                    Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 2, 1 << 8);
                    foreach (Collider2D collider in cols)
                        if (collider.TryGetComponent(out EnemyAI ai))
                            ai.LockDown(5);
                    return 3;
                case PlantType.Garlic:
                    StartCoroutine(GarlicBreath());
                    return 4;
                case PlantType.Radish:
                    health.Heal(1);
                    return 0;
                case PlantType.Ginger:
                    controller.SetSpeed(15);
                    return 4;
                default:
                    throw new IndexOutOfRangeException("There are not enough enums in this switch");
            }
        }

        IEnumerator GarlicBreath()
        {
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(1);
                Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 2, 1 << 8);
                foreach (Collider2D collider in cols)
                    if (collider.TryGetComponent(out EnemyAI ai))
                        ai.LockDown(1);
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
                    controller.SetSpeed(10);
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
