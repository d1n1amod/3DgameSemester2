using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float UpdateSpeed = 0.1f; // how frequently to recalculate path based on Target transform's position

    private NavMeshAgent Agent;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(FollowedTarget());
    }

    private IEnumerator FollowedTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(UpdateSpeed);

        while (enabled)
        {
            Agent.SetDestination(target.transform.position);

            yield return wait;
        }
    }
}
