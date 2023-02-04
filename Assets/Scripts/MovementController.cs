using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freakingpig.controllers
{
    public class MovementController : MonoBehaviour
    {

        public Rigidbody2D rb;

        [SerializeField]
        private float speed = 10;
        [SerializeField]
        private Vector2 velocity = new Vector2();

        // Start is called before the first frame update
        // Update is called once per frame
        void Update()
        {
            if (!Input.anyKey)
            {
                velocity = Vector2.zero;
            }
            else
            {
                velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * 2;
            }

            rb.velocity = velocity * speed;
        }
    }

}
