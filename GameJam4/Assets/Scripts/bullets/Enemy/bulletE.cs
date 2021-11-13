using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bullets
{
    public class bulletE : MonoBehaviour
    {
        Rigidbody rb;
        public float bulletForce = 20f;
        Transform lookE;
        private void Awake()
        {
            lookE = GameObject.Find("lookatE").GetComponent<Transform>();
            rb = GetComponent<Rigidbody>();
        }
        private void OnEnable()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(lookE.forward * bulletForce, ForceMode.Impulse);
        }
        private void OnTriggerEnter(Collider collision)
        {
            gameObject.SetActive(false);
        }
    }
}
