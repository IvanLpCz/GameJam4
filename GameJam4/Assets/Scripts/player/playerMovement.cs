using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripteable_Player;

namespace player
{
    public class playerMovement : MonoBehaviour
    {
        private Rigidbody rb;
        [SerializeField] DashScipteable dashScripteable;
        [SerializeField] movespeedScripteable MovespeedScripteable;
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
            Vector3 rightMovement = right * MovespeedScripteable.speed * Input.GetAxis("Horizontal");
            Vector3 upMovement = forward * MovespeedScripteable.speed * Input.GetAxis("Vertical");
            
            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

            rb.velocity = new Vector3(heading.x, rb.velocity.y, heading.z); 

            transform.forward = heading;
            transform.position += rightMovement;
            transform.position += upMovement;
        }
        IEnumerator Dash()
        {
            rb.AddForce(transform.forward * dashScripteable.dashSpeed, ForceMode.Impulse);
            canDash = false;

            yield return new WaitForSeconds(dashScripteable.dashDuration);

            rb.velocity = Vector3.zero;
            canDash = true;
        }

    }
}
