using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pools;

namespace player
{
    public class aimPlayer : MonoBehaviour
    {
        public Camera cam;
        private Rigidbody rb;
        pool Pool;
        private Vector3 mousePos;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void Update()
        {
           mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetKey("Fire1"))
            {
                shoot();
            }
        }
        private void FixedUpdate()
        {

        }
        private void shoot()
        {
            
        }

    }
}
