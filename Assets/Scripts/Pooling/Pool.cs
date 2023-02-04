using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static freakingpig.pooling.PoolingManager;

namespace freakingpig.pooling
{
    public class Pool : MonoBehaviour
    {
        /// <summary> The amount of instances to create during load screen </summary>
        [SerializeField, InitializationField]
        private int initialBatch;

        /// <summary> The poolable element </summary>
        [SerializeField, InitializationField]
        private GameObject prefab;

        /// <summary> Creates the initial batch of objects </summary>
        void Start()
        {
            for (int i = 0; i < initialBatch; i++) Spawn();
        }

        /// <summary> Looks for pooled objects or spawns a new one if none are pooled </summary>
        /// <returns> Object from the pool </returns>
        public GameObject Extract()
        {
            GameObject obj = transform.childCount != 0 ? transform.GetChild(0).gameObject : Spawn();
            obj.transform.parent = null;
            obj.SetActive(true);
            obj.GetComponent<PoolableComponent>().Extracted();
            return obj;
        }

        /// <summary> Shortcut of <see cref="Extract"/> and <see cref="GameObject.GetComponent{T}"/> </summary>
        /// <returns> Component from pooled object </returns>
        public T Extract<T>() where T : Component
        {
            return Extract().GetComponent<T>();
        }

        /// <summary> Sends the object to the pool </summary>
        /// <param name="obj"> The GameObject to pool, is not necesary to take care of it beforehand </param>
        public void PoolIn(GameObject obj)
        {
            obj.GetComponent<PoolableComponent>().PooledIn();
            obj.transform.parent = transform;
            obj.SetActive(false);
        }

        /// <summary> Creates a new object and sends it to the pool </summary>
        /// <returns> For comfort, also returns the object </returns>
        [ButtonMethod] private GameObject Spawn()
        {
            GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
            PoolableComponent poolable = obj.GetComponent<PoolableComponent>();
            poolable.PooledIn();
            poolable.Pool = this;
            obj.SetActive(false);
            return obj;
        }

    }
}
