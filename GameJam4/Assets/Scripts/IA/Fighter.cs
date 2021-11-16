using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;
using Core;
using Pools;

namespace Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        public Transform aimPos;

        Health target;

        float timeSinceLastAttack = Mathf.Infinity;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
                GetComponent<Animator>().SetBool("inRange", false);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
                GetComponent<Animator>().SetBool("inRange", true);
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }

        }

        private void TriggerAttack()
        {
            GameObject bullet = objectPooling.SharedInstance.GetPooledObject("balaE");

            if (bullet != null)
            {
                bullet.transform.position = aimPos.position;
                bullet.transform.rotation = aimPos.rotation;
                bullet.SetActive(true);
            }
            GetComponent<Animator>().ResetTrigger("StopAtk");
            GetComponent<Animator>().SetTrigger("Attack");
        }
        void Shoot() 
        {
            if (target == null) return;
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
            print("lucha");
        }
        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("StopAtk");
        }
    }
}
