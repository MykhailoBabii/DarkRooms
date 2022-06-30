using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum EnemyType
{
    Rapist,
    Shooter,
    Turret
}


public class EnemyDamageSys : MonoBehaviour
{
    public int damage;

    [SerializeField] private float delayTimeAttack;

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _shootPlace;
    [SerializeField] private GameObject parentPlace;

    [SerializeField] private EnemyType _enemyType;

    public static Action enemyDamage;
    public static Action enemyShoot;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.gameObject;

            if(_enemyType != EnemyType.Rapist)
                StartCoroutine(Shoot());
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject != null)
            enemyDamage?.Invoke();
            _player = collision.gameObject;
    }


    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(delayTimeAttack);
        enemyShoot?.Invoke();
        var bullet = Instantiate(_bullet, _shootPlace.transform.position, _shootPlace.transform.rotation) as GameObject;
        bullet.transform.parent = GetComponentInParent<Transform>().parent.transform;
        StopAllCoroutines();
    }
}



