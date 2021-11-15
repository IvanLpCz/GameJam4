using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Core
{
    public class Health : MonoBehaviour
    {
       [SerializeField] float healthPoints = 100f;
       [SerializeField] float damageGiven;

        bool isDead = false;
        public HealthBar healthBar;

        public bool IsDead()
        {
            return isDead;
        }
        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
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
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("getHit"))
            {
                print("me han dado");
                //health.TakeDamage(bulletDamage);
            }

        }
    }
}
