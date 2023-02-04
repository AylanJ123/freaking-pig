using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freakingpig.pooling
{
    public class PoolingManager : MonoBehaviour
    {

        /// <summary> <inheritdoc cref="GameManager.Instance"/> </summary>
        public static PoolingManager Instance { private set; get; }

        /// <summary> Container of all <see cref="Pool"/> that exist </summary>
        private readonly Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

        /// <summary> Creates Singleton, avoids deletion from scene loading and adds pools from children </summary>
        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            foreach (Pool child in GetComponentsInChildren<Pool>()) pools.Add(child.name, child);
        }

        /// <summary> Gets a pool from its name. This should be used once and then cached. </summary>
        public Pool this[string name] => GetPool(name);

        /// <summary> <inheritdoc cref="this[string]"/> </summary>
        private Pool GetPool(string name)
        {
            return pools[name];
        }

        public class PoolableComponent : MonoBehaviour
        {
            /// <summary> <inheritdoc cref="Pool"/> </summary>
            private Pool _pool;

            /// <summary> Pool owner, it can be assigned only once </summary>
            public Pool Pool
            {
                get => _pool;
                set
                {
                    if (_pool == null) _pool = value;
                }
            }
            public Rigidbody2D RigidBody { protected set; get; }


            [ButtonMethod] public void PoolItself()
            {
                _pool.PoolIn(gameObject);
            }

            /// <summary> Should be called when this object just got extracted and is ready work </summary>
            public virtual void Extracted() { }

            /// <summary> Should be called when object must reset its state before going to sleep in the pool </summary>
            public virtual void PooledIn() { }
        }

    }
}
