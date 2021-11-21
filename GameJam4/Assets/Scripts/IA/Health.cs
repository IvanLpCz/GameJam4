using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using bullets;

namespace Core
{
    public class Health : MonoBehaviour
    {
       [SerializeField] float healthPoints = 100f;
       [SerializeField] bullet Bullet;
       [SerializeField] bulletE BulletE;
       [SerializeField] bullet AR;
       [SerializeField] bullet shotgun;
       [SerializeField] bullet shotgunR;
       [SerializeField] bullet shotgunL;

        public HealthBar healthBar;
        private float damageTaken, maxHealth;
        public bool isDead = false;
        public bool isPlayer = false;
        public GameObject deathCanvas, hudCanvas;
        public GameObject deathSound;
        private void Start()
        {
            maxHealth = healthPoints;
            Bullet = GameObject.Find("bala").GetComponent<bullet>();
            BulletE = GameObject.Find("balaE").GetComponent<bulletE>();
            AR = GameObject.Find("balaAR").GetComponent<bullet>();
            shotgun = GameObject.Find("balaEscopeta").GetComponent<bullet>();
            shotgunR = GameObject.Find("balaEscopetaR").GetComponent<bullet>();
            shotgunL = GameObject.Find("balaEscopetaL").GetComponent<bullet>();

                healthBar.SetMaxHealth(maxHealth);
        }
        private void Update()
        {
            if (isPlayer)
            {
                healthBar.SetHealth(healthPoints);
            }
        }
        public bool IsDead()
        {
            return isDead;
        }
        public void TakeDamage()
        {
            healthPoints = Mathf.Max(healthPoints - damageTaken, 0);
            if (healthPoints == 0)
            {
                Die();
            }
        }
        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            deathSound.GetComponent<AudioSource>().Play();
            if (isPlayer)
            {
                deathCanvas.SetActive(true);
                hudCanvas.SetActive(false);
                Time.timeScale = 0;
            }
            if (!isPlayer)
            {
                deathCanvas = null;
                hudCanvas = null;
            }
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("bala"))
            {
                damageTaken = Bullet.bulletDamage;
                print("playerdmg" + damageTaken);
                TakeDamage();
            }
            if (collision.gameObject.CompareTag("balaE"))
            {
                damageTaken = BulletE.bulletDamage;
                print("bossdmg" + damageTaken);
                TakeDamage();
            }
            if (collision.gameObject.CompareTag("balaAR"))
            {
                damageTaken = AR.bulletDamage;
                print("bossdmg" + damageTaken);
                TakeDamage();
            }
            if (collision.gameObject.CompareTag("balaEscopeta"))
            {
                damageTaken = shotgun.bulletDamage;
                print("bossdmg" + damageTaken);
                TakeDamage();
            }
            if (collision.gameObject.CompareTag("balaEscopetaR"))
            {
                damageTaken = shotgunR.bulletDamage;
                print("bossdmg" + damageTaken);
                TakeDamage();
            }
            if (collision.gameObject.CompareTag("balaEscopetaL"))
            {
                damageTaken = shotgunL.bulletDamage;
                print("bossdmg" + damageTaken);
                TakeDamage();
            }

        }
    }
}
