using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTargetSys : MonoBehaviour
{
    private GameObject _player;

    [SerializeField] private SphereCollider _sphereCollider;


    private void FixedUpdate()
    {
        LookAtTargetToAttack();
    }


    private void LookAtTargetToAttack()
    {
        if (_player != null)
        {
            Vector3 dir = _player.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            _player = other.gameObject;
    }
}
