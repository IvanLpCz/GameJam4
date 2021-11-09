using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class controller : MonoBehaviour
    {
        private Rigidbody rb;
        public float speed = 1;
        public float dashSpeed = 5;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            float xMov = Input.GetAxisRaw("Horizontal");
            float zMov = Input.GetAxisRaw("Vertical");

            rb.velocity = new Vector3(xMov, rb.velocity.y, zMov) * speed;

            if (Input.GetKey(KeyCode.Space))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    rb.AddForce(Vector3.forward * dashSpeed, ForceMode.Impulse);
                }               
                if (Input.GetKey(KeyCode.S))
                {
                    rb.AddForce(-Vector3.forward * dashSpeed, ForceMode.Impulse);
                }

                if (Input.GetKey(KeyCode.D))
                {
                    rb.AddForce(Vector3.right * dashSpeed, ForceMode.Impulse);
                }

                if (Input.GetKey(KeyCode.A))
                {
                    rb.AddForce(-Vector3.right * dashSpeed, ForceMode.Impulse);
                }
            }
        }

    }
}
