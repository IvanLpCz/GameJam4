using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Core;

namespace Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;

        NavMeshAgent navMeshAnget;
        Health health;

        private void Start()
        {
            navMeshAnget = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            navMeshAnget.enabled = !health.IsDead();

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAnget.destination = destination;
            navMeshAnget.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAnget.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAnget.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAnget.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

    }

}