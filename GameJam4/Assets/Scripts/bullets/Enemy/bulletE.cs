using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Scripteable_Bullet;

namespace bullets
{
    public class bulletE : MonoBehaviour
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
            gameObject.SetActive(false);
        }
    }
}
