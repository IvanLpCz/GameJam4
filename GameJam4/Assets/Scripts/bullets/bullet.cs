using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripteable_Bullet;

namespace bullets
{
    public class bullet : MonoBehaviour
    {
        Rigidbody rb;
        [SerializeField] basicBullet BasicBullet;
        public float bulletDamage;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            bulletDamage = BasicBullet.bulletDamage;
        }
        private void OnEnable()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(transform.forward * BasicBullet.bulletForce, ForceMode.Impulse);
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("getHit"))
            {
                print("hula, te he dado");
            }
            gameObject.SetActive(false);
        }
    }
}
