using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pools;
using weapons;

namespace player
{
    public class aimPlayer : MonoBehaviour
    {
        public Camera cam;
        public Transform aimPos;
        public float cd = 0.2f;
        private float lastShoot = 1;
        public bool handgun, shotgun, ar;
        private bool canShotgun, canAr;
        private float fireRate;
        private float ammoAR, ammoShotgun;

        [SerializeField] public weaponScript[] WeaponScript;
        [SerializeField] private LayerMask groundMask;

        private GameObject bulletHandgun, bulletShotGun, bulletAR;

        private void Start()
        {
            cam = Camera.main;
            handgun = true;
        }
        private void Update()
        {
            Aim();
            ammoManagement();
            gunManagement();
            if (Input.GetKeyDown(KeyCode.Mouse0) && lastShoot > fireRate && handgun)
            {
                shootHandGun();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && lastShoot > fireRate && shotgun)
            {
                shootShotGun();
            }
            if (Input.GetKey(KeyCode.Mouse0) && lastShoot > fireRate && ar)
            {
                shootAR();
            }
            lastShoot = Time.deltaTime + lastShoot;
        }

        void ammoManagement()
        {
            if(ammoAR > 0)
            {
                canAr = true;
            }
            else
            {
                canAr = false;
            }
            if(ammoShotgun > 0)
            {
                canShotgun = true;
            }
            else
            {
                canShotgun = false;
            }
        }
        private void gunManagement()
        {
            if (Input.GetKey(KeyCode.Alpha1) && canAr)
            {
                ar = true;
                shotgun = false;
                handgun = false;

                ammoAR = ammoAR - 1;
            }
            if (Input.GetKey(KeyCode.Alpha2) && canShotgun)
            {
                ar = false;
                shotgun = true;
                handgun = false;

                ammoShotgun = ammoShotgun - 1;
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                ar = false;
                shotgun = false;
                handgun = true;
            }
            if (handgun)
            {
                fireRate = WeaponScript[0].fireRate;
                print("pistola activa " + "su cadencia es de " + fireRate);
                bulletHandgun = objectPooling.SharedInstance.GetPooledObject("bala");
            }
            if (shotgun)
            {
                fireRate = WeaponScript[1].fireRate;
                print("escopeta activa " + "su cadencia es de " + fireRate + "su munición restante es de: " + ammoShotgun);
                bulletShotGun = objectPooling.SharedInstance.GetPooledObject("balaEscopeta");
            }
            if (ar)
            {
                fireRate = WeaponScript[2].fireRate;
                print("rifle de asalto activo " + "su cadencia es de " + fireRate + "su munición restante es de: " + ammoAR);
                bulletAR = objectPooling.SharedInstance.GetPooledObject("balaAR");
            }
        }
        void shootAR()
        {
            if(bulletAR != null)
            {
                bulletAR.transform.position = aimPos.position;
                bulletAR.transform.rotation = aimPos.rotation;
                bulletAR.SetActive(true);
                lastShoot = 0f;
            }
        }
        void shootHandGun()
        {
            if (bulletHandgun != null)
            {
                bulletHandgun.transform.position = aimPos.position;
                bulletHandgun.transform.rotation = aimPos.rotation;
                bulletHandgun.SetActive(true);
                lastShoot = 0f;
            }
        }
        void shootShotGun()
        {
            if (bulletShotGun != null)
            {
                bulletShotGun.transform.position = aimPos.position;
                bulletShotGun.transform.rotation = aimPos.rotation;
                bulletShotGun.SetActive(true);
                lastShoot = 0f;
            }
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("ammoAR"))
            {
                ammoAR = ammoAR + 60;
            }
            if (other.gameObject.CompareTag("ammoShotgun"))
            {
                ammoShotgun = ammoShotgun + 12;
            }
        }

    }
}
