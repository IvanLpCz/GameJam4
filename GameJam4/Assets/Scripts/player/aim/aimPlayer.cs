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
        public Transform bulletPosition;
        public float cd = 0.2f;
        private float lastShoot = 1;


        [SerializeField] private LayerMask groundMask;

        private void Start()
        {
            cam = Camera.main;
        }
        private void Update()
        {
            Aim();

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

        private void Aim()
        {

            var (success, position) = GetMousePosition();
            if (success)
            {
                Vector3 direction = position - transform.position;
                direction.y = 0;                
                transform.forward = direction;
            }
        }

        private (bool success, Vector3 position) GetMousePosition()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
            {
                
                return (success: true, position: hitInfo.point);
            }
            else
            {
                
                return (success: false, position: Vector3.zero);
            }
        }

    }
}
