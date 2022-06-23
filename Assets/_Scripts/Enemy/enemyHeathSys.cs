using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class enemyHeathSys : MonoBehaviour
{
    public static Action enemyGetHit;

    [SerializeField] private Image _healthLine;
    [SerializeField] private GameObject _textInfo;
    [SerializeField] private GameObject _impact;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _bonus;

    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float hp;
    [SerializeField] private float maxHp;

    [SerializeField] private int chanceNumber;
    [SerializeField] private int lengthСhanceToBonuse;

    [SerializeField] private int timeToDestroy;
    [SerializeField] private float hightImpactPosition;

    [SerializeField] NavMeshAgent _navMeshAgent;
    [SerializeField] enemyAnimationsController enemyAnimations;
    [SerializeField] enemyMoveSys _enemyMoveSys;
    [SerializeField] enemyDamageSys _enemyDamageSys;
    [SerializeField] enemyTargetSys _enemyTargetSys;

    


    private void Awake()
    {
        hp = maxHp;
    }


    private void Start()
    {
        chanceNumber = UnityEngine.Random.Range(0, lengthСhanceToBonuse);
    }


    private void FixedUpdate()
    {
        if (_healthLine != null)
        {
            _healthLine.fillAmount = hp / maxHp;
            _healthLine.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        _textInfo.transform.rotation = Quaternion.Euler(0, 0, 0);


        Die();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.bullet))
        {
            enemyGetHit?.Invoke();
            var hpBefore = hp;
            var impactPosition = new Vector3(transform.position.x, transform.position.y + hightImpactPosition, transform.position.z);
            Instantiate(_impact, impactPosition, Quaternion.identity);
            hp -= collision.gameObject.GetComponent<Bullet>().damage;

            StartCoroutine(TextDamageInfo(hp - hpBefore));
        }

        
    }

    private void Die()
    {
        if (hp < 1)
        {
            //enemyDie = true;

            if (_explosion != null)
                _explosion.SetActive(true);

            if (_enemyTargetSys != null)
                _enemyTargetSys.enabled = false;

            if (enemyAnimations != null)
            enemyAnimations.EnemyDieAnimation();

            if (_enemyMoveSys != null)
            _enemyMoveSys.enabled = false;

            if (_enemyDamageSys != null)
            {
                _enemyDamageSys.StopAllCoroutines();
                _enemyDamageSys.enabled = false;
            }
            
            if (_navMeshAgent != null)
            _navMeshAgent.enabled = false;

            if (_rigidbody != null)
            {
                _rigidbody.isKinematic = true;
            }

            if (_boxCollider != null)
                _boxCollider.enabled = false;

            gameObject.tag = "Untagged";

            Destroy(gameObject, timeToDestroy);
        }
    }


    IEnumerator TextDamageInfo(float damage)
    {
        yield return new WaitForSeconds(0);
        _textInfo.SetActive(true);
        _textInfo.GetComponent<Text>().text = $"{damage}";

        yield return new WaitForSeconds(0.5f);
        _textInfo.SetActive(false);

        StopAllCoroutines();

    }


    private void OnDestroy()
    {
        if (chanceNumber == 0 && GetComponentInParent<Transform>() != null)
        {
            var item = Instantiate(_bonus, transform.position, Quaternion.identity) as GameObject;
            item.transform.parent = GetComponentInParent<Transform>().parent.transform;
        }
    }
}
