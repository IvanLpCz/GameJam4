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
        private Vector3 mousePos;
        public Transform bulletPosition;
        public float cd = 0.2f;
        private float lastShoot = 1;
        private void Start()
        {

        }
        private void Update()
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newMousePos = new Vector3(mousePos.x, 0f, mousePos.z);

            Vector3 direction = newMousePos - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(direction, Vector3.back);
            transform.rotation = newRotation;
            if (Input.GetKeyDown(KeyCode.Mouse0) && lastShoot > cd)
            {
                StartCoroutine(shoot());
            }
            lastShoot = Time.deltaTime + lastShoot;
        }
        IEnumerator shoot()
        {
            GameObject bullet = pooling.instance.GetPooledObject();

            if (bullet != null)
            {             
                bullet.transform.position = bulletPosition.position;
                bullet.SetActive(true);
                lastShoot = 0f;
            }
            yield return null;
        }

    }
}
