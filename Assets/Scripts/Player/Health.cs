using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freakingpig
{
    public class Health : MonoBehaviour
    {
        [SerializeField, InitializationField] private int maxHealth = 10;
        [SerializeField, ReadOnly] private int health;

        private void Start()
        {
            health = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            //estaaa morrtoooooo
        }

        public void Heal(int heal)
        {
            health += heal;
            if (health > maxHealth)
            {
                health = maxHealth;
            }

        }
    }
}
