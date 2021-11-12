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
        private bool hasShooted;

        private void Awake()
        {
            look = GameObject.Find("lookat").GetComponent<Transform>();
            rb = GetComponent<Rigidbody>();
        }
        private void OnEnable()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(look.forward * bulletForce, ForceMode.Impulse);
        }
        private void OnTriggerEnter(Collider collision)
        {
            gameObject.SetActive(false);
        }
    }
}
