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
        [SerializeField]
        private float angle;

        [SerializeField]
        private Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

                angle = Mathf.Atan2(Input.GetAxis("Horizontal") * -1, Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
                rb.SetRotation(angle);
                rb.velocity = velocity * speed;
                animator.SetBool("running", true);
            }
            else
            {
                velocity = Vector2.zero;
                animator.SetBool("running", false);
            }

        }
    }

}
