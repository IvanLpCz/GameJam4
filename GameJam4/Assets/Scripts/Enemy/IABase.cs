using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABase : MonoBehaviour
{
    [SerializeField] float chaseDistance = 10f;
    private GameObject player;
    //private Health health;
    //private Movement movement;
    private NavMeshAgent navMeshAgent;
    private Vector3 guardingLocation;


    private void Start()
    {

        //health = GetComponent<Health>();
        //movement = GetComponent<Movement>();

        guardingLocation = transform.position;
    }
    private void FixedUpdate()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        //if (health.IsDead()) return;

        if (InRange())//&& combat.CanAttakck(player)
        {
            //combat.Attack(player);
            Debug.Log("Debe de Perseguir al jugador");
        }
        else
        {
            PatrolBehaviour();
        }
    }

    private void PatrolBehaviour()
    {
        Vector3 nextPosition = guardingLocation;

        StartMoveAction(nextPosition);
    }

    private bool InRange()
    {
        float DistanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return DistanceToPlayer < chaseDistance;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
    public void StartMoveAction(Vector3 destination)
    {
        //GetComponent<Scheduler>().StartAction(this);
        MoveTo(destination);
    }

    public void MoveTo(Vector3 destination)
    {
        navMeshAgent.destination = destination;
        navMeshAgent.isStopped = false;
    }
}
