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
        private Vector3 forward, right;
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
            move();
        }

        private void move()
        {           
            Vector3 rightMovement = right * speed * Time.deltaTime * Input.GetAxis("Horizontal");
            Vector3 upMovement = forward * speed * Time.deltaTime * Input.GetAxis("Vertical");

            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

            //rb.velocity = new Vector3(upMovement.x, rb.velocity.y, rightMovement.z); 

            transform.forward = heading;
            transform.position += rightMovement;
            transform.position += upMovement;
        }
    }
}
