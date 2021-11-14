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
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void OnEnable()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(transform.forward * bulletForce, ForceMode.Impulse);
        }
        private void OnTriggerEnter(Collider collision)
        {
            gameObject.SetActive(false);
        }
    }
}
