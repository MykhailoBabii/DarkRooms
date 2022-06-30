using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveSys : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private GameObject _player;

    public static Action enemyMove;
    public static Action enemyIdle;

    
    private void FixedUpdate()
    {
        EnemyMovement();
    }


    private void EnemyMovement()
    {
        if (_player != null)
        {
            enemyMove?.Invoke();
            _navMeshAgent.SetDestination(_player.transform.position);
        }
 
        else
            enemyIdle?.Invoke();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Tags.player))
            _player = other.gameObject;
    }
}
