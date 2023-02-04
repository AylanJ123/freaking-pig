using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static freakingpig.pooling.PoolingManager;

namespace freakingpig.utils
{
    public class AutoPooler : MonoBehaviour
    {

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PoolableComponent poolable)) poolable.PoolItself();
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PoolableComponent poolable)) poolable.PoolItself();
        }

    }
}
