using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< Updated upstream
namespace freakingpig.controllers
{
    public class MovementController : MonoBehaviour
=======

public class MovementController : MonoBehaviour
{
    public Rigidbody2D rb;

    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private Vector2 velocity = new Vector2();

    // Start is called before the first frame update
    void Start()
>>>>>>> Stashed changes
    {
        public Rigidbody2D rb;

<<<<<<< Updated upstream
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
=======
    // Update is called once per frame
    void Update()
    {
        velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * 2;

        rb.velocity = velocity * speed;
>>>>>>> Stashed changes
    }
}
