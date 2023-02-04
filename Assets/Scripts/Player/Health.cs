using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freakingpig
{
    public class Health : MonoBehaviour
    {
        public int health = 10;
        public int maxHealth = 10;

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
