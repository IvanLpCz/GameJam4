using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class playerMovement : MonoBehaviour
    {
        private Rigidbody rb;
        public float speed = 5f;
        public float dashSpeed = 5f;
        public float dashDuration = 0.25f;
        private Vector3 forward, right;
        public bool canDash = true;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            forward = Camera.main.transform.forward;
            forward.y = 0;
            forward = Vector3.Normalize(forward);
            right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && canDash)
            {

                StartCoroutine(Dash());
            }
        }
        private void FixedUpdate()
        {
            move();
        }
        private void move()
        {           
            Vector3 rightMovement = right * speed * Input.GetAxis("Horizontal");
            Vector3 upMovement = forward * speed * Input.GetAxis("Vertical");
            
            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

            rb.velocity = new Vector3(heading.x, rb.velocity.y, heading.z); 

            transform.forward = heading;
            transform.position += rightMovement;
            transform.position += upMovement;
        }
        IEnumerator Dash()
        {
            rb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
            canDash = false;

            yield return new WaitForSeconds(dashDuration);

            rb.velocity = Vector3.zero;
            canDash = true;
        }

    }
}
