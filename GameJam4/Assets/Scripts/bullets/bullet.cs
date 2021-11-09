using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bullets
{
    public class bullet : MonoBehaviour
    {
        Rigidbody2D rb;
        public Transform firePoint;
        public GameObject bulletPrefab;

        public float bulletForce = 20f;

        private void FixedUpdate()
        {
            rb = GetComponent<Rigidbody2D>();
            shoot();
        }

        private void shoot()
        {
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            gameObject.SetActive(false);
        }
    }
}
