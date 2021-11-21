using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripteable_Bullet;
using player;

namespace bullets
{
    public class bullet : MonoBehaviour
    {
        Rigidbody rb;
        [SerializeField] basicBullet BasicBullet;
        public float bulletDamage;
        public GameObject particle;
        private float bonusSpeed;

        private perksOnPlayer perkOn;

        public GameObject activeBonus;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            bulletDamage = BasicBullet.bulletDamage;
            bonusSpeed = 0f;
            perkOn = GameObject.Find("Player").GetComponent<perksOnPlayer>();
        }
        private void FixedUpdate()
        {
            if (perkOn.perkActivated)
            {
                bonusSpeed = 10f;
            }
        }
        private void OnEnable()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(transform.forward * (BasicBullet.bulletForce + bonusSpeed), ForceMode.Impulse);
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("getHit"))
            {
                print("hula, te he dado");
            }
            Instantiate(particle, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
