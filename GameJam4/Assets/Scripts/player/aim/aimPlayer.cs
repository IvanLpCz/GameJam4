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
        public Transform[] aimPos;
        public float cd = 0.2f;
        private float lastShoot = 1;
        public bool handgun, shotgun, ar;
        private bool canShotgun, canAr;
        private float fireRate;
        public float ammoAR, ammoShotgun;
        public GameObject playerPistola, playerEscopeta, playerAR;
        public int gunActive;

        [SerializeField] public weaponScript[] WeaponScript;
        [SerializeField] private LayerMask groundMask;

        private GameObject bulletHandgun, bulletAR;
        private GameObject bulletShotGun0, bulletShotGun1, bulletShotGun2;
        private GameObject[] bulletShotGun;

        private void Start()
        {
            cam = Camera.main;
            handgun = true;
            GetComponent<Animator>().SetBool("pistola", true);
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
                if(ammoShotgun <= 0)
                {
                    shotgun = false;
                    handgun = true;
                }
            }
            if (Input.GetKey(KeyCode.Mouse0) && lastShoot > fireRate && ar)
            {
                shootAR();
                if(ammoAR <= 0)
                {
                    ar = false;
                    handgun = true;
                }
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
                playerAR.SetActive(true);
                playerPistola.SetActive(false);
                playerEscopeta.SetActive(false);

                GetComponent<Animator>().SetBool("ar", true);
                GetComponent<Animator>().SetBool("pistola", false);
                GetComponent<Animator>().SetBool("2pistola", false);
                GetComponent<Animator>().SetBool("escopeta", false);

            }
            if (Input.GetKey(KeyCode.Alpha2) && canShotgun)
            {
                ar = false;
                shotgun = true;
                handgun = false;
                playerAR.SetActive(false);
                playerPistola.SetActive(false);
                playerEscopeta.SetActive(true);

                GetComponent<Animator>().SetBool("ar", false);
                GetComponent<Animator>().SetBool("pistola", false);
                GetComponent<Animator>().SetBool("2pistola", false);
                GetComponent<Animator>().SetBool("escopeta", true);

            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                ar = false;
                shotgun = false;
                handgun = true;
                playerAR.SetActive(false);
                playerPistola.SetActive(true);
                playerEscopeta.SetActive(false);

                GetComponent<Animator>().SetBool("ar", false);
                GetComponent<Animator>().SetBool("pistola", true);
                GetComponent<Animator>().SetBool("2pistola", false);
                GetComponent<Animator>().SetBool("escopeta", false);
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
                bulletShotGun0 = objectPooling.SharedInstance.GetPooledObject("balaEscopeta");
                bulletShotGun1 = objectPooling.SharedInstance.GetPooledObject("balaEscopetaR");
                bulletShotGun2 = objectPooling.SharedInstance.GetPooledObject("balaEscopetaL");
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
                bulletAR.transform.position = aimPos[0].position;
                bulletAR.transform.rotation = aimPos[0].rotation;
                bulletAR.SetActive(true);
                lastShoot = 0f;
                ammoAR = ammoAR - 1;
            }
        }
        void shootHandGun()
        {
            if (bulletHandgun != null)
            {
                bulletHandgun.transform.position = aimPos[0].position;
                bulletHandgun.transform.rotation = aimPos[0].rotation;
                bulletHandgun.SetActive(true);
                lastShoot = 0f;
            }
        }
        void shootShotGun()
        {
            if (bulletShotGun0 != null)
            {
                bulletShotGun0.transform.position = aimPos[0].position;
                bulletShotGun0.transform.rotation = aimPos[0].rotation;

                bulletShotGun0.SetActive(true);
                lastShoot = 0f;
                ammoShotgun = ammoShotgun - 1;
            }
            if (bulletShotGun1 != null)
            {
                bulletShotGun1.transform.position = aimPos[1].position;
                bulletShotGun1.transform.rotation = aimPos[1].rotation;
                print("eoo");
                bulletShotGun1.SetActive(true);
                ammoShotgun = ammoShotgun - 1;
            }
            if (bulletShotGun2 != null)
            {
                bulletShotGun2.transform.position = aimPos[2].position;
                bulletShotGun2.transform.rotation = aimPos[2].rotation;
                print("eoo22");
                bulletShotGun2.SetActive(true);
                ammoShotgun = ammoShotgun - 1;
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
