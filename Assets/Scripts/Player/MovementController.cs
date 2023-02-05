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
        [SerializeField] private float stamina = 50;

        private readonly float staminaIncPerFrame = 35;
        private readonly float staminaDecPerFrame = 20;
        private readonly float maxStamina = 50;
        private readonly float staminaRegenDelayTime = 3;
        private float staminaRegenTimer = 0;

        private Vector2 velocity = new(0, 1);
        private bool rotate;
        private bool isRunning;
        public ParticleSystem runParticle;


        private void Update()
        {
            velocity = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            rotate = velocity != Vector2.zero;
            animator.SetBool("running", rotate);
            if (rotate)
            {
                runParticle.Play();
            }

            isRunning = Input.GetKey(KeyCode.LeftShift) && stamina > 0;

            if (isRunning)
            {
                Run();
            }
            else if (stamina < maxStamina)
            {
                RegenStamina();
            }
        }

        private void FixedUpdate()
        {
            rb.velocity = velocity * (velocity.x != 0 && velocity.y != 0 ? speed * .75f : speed) * (isRunning ? 1.5f : 1);
            if (!rotate) return;
            Vector3 diff = (transform.position + (Vector3)velocity - transform.position);
            diff.Normalize();
            float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new(0, 0, Mathf.LerpAngle(transform.eulerAngles.z, rotZ - 90, .2f));
        }

        void Run()
        {
            stamina = Mathf.Clamp(stamina - (staminaDecPerFrame * Time.deltaTime), 0.0f, maxStamina);
            staminaRegenTimer = 0;
        }

        void RegenStamina()
        {
            if (staminaRegenTimer >= staminaRegenDelayTime)
                stamina = Mathf.Clamp(stamina + (staminaIncPerFrame * Time.deltaTime), 0.0f, maxStamina);
            else
                staminaRegenTimer += Time.deltaTime;
        }

    }

}
