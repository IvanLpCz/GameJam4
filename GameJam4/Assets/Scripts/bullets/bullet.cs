using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bullets
{
    public class bullet : MonoBehaviour
    {
        Rigidbody rb;
        public float bulletForce = 20f;
        public Transform look;

        private void Start()
        {
            look = GameObject.Find("lookat").GetComponent<Transform>();

        }
        private void FixedUpdate()
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = look.forward * bulletForce;
            
        }

        private void OnTriggerEnter(Collider collision)
        {
            gameObject.SetActive(false);
        }
    }
}
