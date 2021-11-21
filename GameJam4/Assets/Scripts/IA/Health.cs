﻿using System.Collections;
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
        public HealthBar healthBar;
        private float damageTaken, maxHealth;
        public bool isDead = false;
        public bool isPlayer = false;
        public GameObject deathCanvas, hudCanvas;
        private void Start()
        {
            maxHealth = healthPoints;
            Bullet = GameObject.Find("bala").GetComponent<bullet>();
            BulletE = GameObject.Find("balaE").GetComponent<bulletE>();
            healthBar.SetMaxHealth(maxHealth);
        }
        private void Update()
        {
            if (!isPlayer)
            {
                healthBar = null;
            }
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

        }
    }
}
