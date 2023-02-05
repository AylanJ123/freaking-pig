using freakingpig.gameplay;
using freakingpig.holders;
using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace freakingpig
{
    public class Health : MonoBehaviour
    {
        [SerializeField, InitializationField] private float maxHealth = 10;
        [SerializeField, ReadOnly] private float health;
        public UnityEvent<float> HealthChanged;
        public Animator animator;

        private void Start()
        {
            health = maxHealth;
            animator = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            health -= damage;

            float percentage = health / maxHealth;
            HealthChanged.Invoke(percentage);

            if (health <= 0)
            {
                Die();
            }

        }

        public void Die()
        {
            Transitions.Transition(1, 0, () =>
            {
                foreach (Plant plant in FindObjectsOfType<Plant>()) plant.PoolItself();
                SceneManager.LoadScene("LooseMenu", LoadSceneMode.Single);
                SPlayer.SwitchTrack(SoundHolder.Instance.gameStart, .3f, .2f);
            }
            );
            animator.SetTrigger("die");
        }

        public void Heal(int heal)
        {
            health += heal;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            float percentage = health / maxHealth;
            HealthChanged.Invoke(percentage);
        }
    }
}
