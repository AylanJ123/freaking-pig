using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace freakingpig.controllers
{
    public class MovementController : MonoBehaviour
    {

        [SerializeField, InitializationField] private float speed = 10;
        [SerializeField, AutoProperty] private Rigidbody2D rb;
        [SerializeField, AutoProperty] private Animator animator;
        private Vector2 velocity = new(0, 1);
        private bool rotate;

        private void Update()
        {
            velocity = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            rotate = velocity != Vector2.zero;
            animator.SetBool("running", rotate);
        }

        private void FixedUpdate()
        {
            rb.velocity = velocity * speed;
            if (!rotate) return;
            Vector3 diff = (transform.position + (Vector3) velocity - transform.position);
            diff.Normalize();
            float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, rotZ - 90);
        }

    }

}
