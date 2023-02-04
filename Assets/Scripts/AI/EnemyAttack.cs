using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace freakingpig
{
    public class EnemyAttack : MonoBehaviour
    {

        [SerializeField, InitializationField] private Collider2D damageCol;
        [SerializeField, InitializationField] private float attackTime;
        [SerializeField, InitializationField] private int damage;
        [SerializeField, AutoProperty] private Animator anim;
        private float attackCooldown;
        public UnityEvent<float> attacking;
        public Action<float, int, Collider2D, Collider2D, Health> managedResponsed;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (Time.time > attackCooldown && collision.TryGetComponent(out Health player)) StartCoroutine(WaitAndAttack(collision, player));
        }

        IEnumerator WaitAndAttack(Collider2D col, Health player)
        {
            anim.ResetTrigger("Attack");
            anim.SetTrigger("Attack");
            attacking.Invoke(attackTime);
            attackCooldown = Time.time + attackTime;
            if (managedResponsed == null)
            {
                yield return new WaitForSeconds(attackTime);
                ContactFilter2D cf = new();
                cf.useLayerMask = true;
                cf.useTriggers = true;
                cf.layerMask = 1 << 7;
                Collider2D[] cols = new Collider2D[1];
                damageCol.OverlapCollider(cf, cols);
                if (cols[0] != null && cols[0] == col) player.TakeDamage(damage);
            }
            else managedResponsed.Invoke(attackTime, damage, damageCol, col, player);
        }

    }
}
