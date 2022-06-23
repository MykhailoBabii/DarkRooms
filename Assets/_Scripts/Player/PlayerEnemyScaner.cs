using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyScaner : MonoBehaviour
{
    public static PlayerEnemyScaner instance;
    public static Action enemyDetection;

    public bool enemyDetected;

    [SerializeField] private GameObject[] enemyTargets;
    [SerializeField] private GameObject nearestEnemy;


    private void Awake()
    {
        instance = this;
    }


    private void OnEnable()
    {
        PlayerController.playerDie += Die;
        PlayerController.playerAttack += LookAtTargetToAttack;
    }


    private void OnDisable()
    {
        PlayerController.playerDie -= Die;
        PlayerController.playerAttack -= LookAtTargetToAttack;

    }


    private void Start()
    {
        instance = this;
        StartCoroutine(TargetEnemyUpdate());
    }


    private void EnemyTargetScaner()
    {
        enemyDetection?.Invoke();

        enemyTargets = GameObject.FindGameObjectsWithTag(Tags.enemy);

        nearestEnemy = null;

        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemyTargets)
        {
            if (enemy == null) return;
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (enemyTargets.Length == 0) enemyDetected = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.enemy) && enemyTargets.Length > 0)
            enemyDetected = true;
    }


    private void LookAtTargetToAttack()
    {
        if (nearestEnemy != null)
        {
            Vector3 dir = nearestEnemy.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, rotation.y, 0);
        }
    }

    private void Die()
    {
        StopAllCoroutines();
    }


    IEnumerator TargetEnemyUpdate()
    {
        while (true)
        {
            EnemyTargetScaner();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
