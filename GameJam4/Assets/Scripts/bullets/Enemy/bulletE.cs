using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace bullets
{
    public class bulletE : MonoBehaviour
    {
        Rigidbody rb;
        public float bulletForce = 20f;
        public float bulletDamage = 10f;
        Health health;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            health = GetComponent<Health>();
        }
        private void OnEnable()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(transform.forward * bulletForce, ForceMode.Impulse);
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("getHit"))
            {
                print("hula, te he dado"); 
                health.TakeDamage(bulletDamage);
            }
            gameObject.SetActive(false);
        }
    }
}
