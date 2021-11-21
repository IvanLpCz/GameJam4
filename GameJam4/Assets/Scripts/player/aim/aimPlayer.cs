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

        private float firerateUpgrade;
        private float firerateUpgradeShotGun;

        private void Start()
        {
            cam = Camera.main;
            handgun = true;
            GetComponent<Animator>().SetBool("pistola", true);
            firerateUpgrade = 0;
            firerateUpgradeShotGun = 0;
        }
        private void Update()
        {
            Aim();
            ammoManagement();
            gunManagement();
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)) {
                if (Input.GetKeyDown(KeyCode.Mouse0) && lastShoot > fireRate && handgun)
                {
                    shootHandGun();
                    playerPistola.GetComponent<AudioSource>().Play();
                }
                if (Input.GetKeyDown(KeyCode.Mouse0) && lastShoot > fireRate && shotgun)
                {
                    shootShotGun();
                    playerEscopeta.GetComponent<AudioSource>().Play();
                    if (ammoShotgun <= 0)
                    {
                        shotgun = false;
                        handgun = true;
                    }
                }
                if (Input.GetKey(KeyCode.Mouse0) && lastShoot > fireRate && ar)
                {
                    shootAR();
                    playerAR.GetComponent<AudioSource>().Play();
                    if (ammoAR <= 0)
                    {
                        ar = false;
                        handgun = true;
                    }
                }
            }              
            lastShoot = Time.deltaTime + lastShoot;
            if(ammoAR <= 0)
            {
                playerAR.SetActive(false);
                playerPistola.SetActive(true);
            }
            if(ammoShotgun <= 0)
            {
                playerEscopeta.SetActive(false);
                playerPistola.SetActive(true);
            }

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
            if (Input.GetKey(KeyCode.Alpha2) && canAr)
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
            if (Input.GetKey(KeyCode.Alpha3) && canShotgun)
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
            if (Input.GetKey(KeyCode.Alpha1))
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
                fireRate = WeaponScript[0].fireRate - firerateUpgrade;
                print("pistola activa " + "su cadencia es de " + fireRate);
                bulletHandgun = objectPooling.SharedInstance.GetPooledObject("bala");
            }
            if (shotgun)
            {
                fireRate = WeaponScript[1].fireRate - firerateUpgradeShotGun;
                print("escopeta activa " + "su cadencia es de " + fireRate + "su munición restante es de: " + ammoShotgun);
                bulletShotGun0 = objectPooling.SharedInstance.GetPooledObject("balaEscopeta");
                bulletShotGun1 = objectPooling.SharedInstance.GetPooledObject("balaEscopetaR");
                bulletShotGun2 = objectPooling.SharedInstance.GetPooledObject("balaEscopetaL");
            }
            if (ar)
            {
                fireRate = WeaponScript[2].fireRate - firerateUpgrade;
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
                ammoAR = ammoAR + 30;
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("ammoShotgun"))
            {
                ammoShotgun = ammoShotgun + 12;
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("PerkC"))
            {
                firerateUpgrade = 0.1f;
                firerateUpgradeShotGun = 0.4f;
            }
        }

    }
}
