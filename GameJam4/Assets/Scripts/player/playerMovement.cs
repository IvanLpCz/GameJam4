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
        public bool canDash = false;
        private bool isWall;
        public float bounce = 2f;
        public GameObject guns;
        public float bonusMovement;

        public GameObject dashParticles;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            forward = Camera.main.transform.forward;
            forward.y = 0;
            forward = Vector3.Normalize(forward);
            right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

            bonusMovement = 0;
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
            if (!isWall)
            {
                move();
            }      
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
            {
                //GetComponent<Animator>().SetBool("moving", true);
                guns.SetActive(true);
                GetComponent<Animator>().SetBool("moving", true);
            }
            else
            {
                //GetComponent<Animator>().SetBool("moving", false);
                StartCoroutine(showGuns());
                GetComponent<Animator>().SetBool("moving", false);
                
            }
        }
        IEnumerator showGuns()
        {
            yield return new WaitForSeconds(0.2f);
            guns.SetActive(false);
        }
        private void move()
        {           
            Vector3 rightMovement = right * (MovespeedScripteable.speed + bonusMovement) * Input.GetAxis("Horizontal");
            Vector3 upMovement = forward * (MovespeedScripteable.speed + bonusMovement) * Input.GetAxis("Vertical");


            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

            rb.velocity = new Vector3(heading.x, rb.velocity.y, heading.z); 

            transform.forward = heading;
            transform.position += rightMovement;
            transform.position += upMovement;
        }
        IEnumerator Dash()
        {
            dashParticles.SetActive(true);
            rb.AddForce(transform.forward * dashScripteable.dashSpeed, ForceMode.Impulse);
            
            canDash = false;

            yield return new WaitForSeconds(dashScripteable.dashDuration);

            rb.velocity = Vector3.zero;
            dashParticles.SetActive(false);
            canDash = true;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("wall"))
            {
                isWall = true;
                rb.AddForce(-transform.forward * bounce, ForceMode.VelocityChange);
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            isWall = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PerkD"))
            {
                canDash = true;
            }
            if (other.gameObject.CompareTag("PerkS"))
            {
                bonusMovement = 0.2f;
            }
        }
    }
}
