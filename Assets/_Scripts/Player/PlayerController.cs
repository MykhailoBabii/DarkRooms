using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public static Action playerIdle;
    public static Action playerMove;
    public static Action playerAttack;
    public static Action playerShoot;
    public static Action playerGetHit;
    public static Action playerDie;

    public static Action getShootBonus;

    public bool isWalk;
    public bool isDie;

    public float delayAttack;

    [SerializeField] private float healthPoints;
    [SerializeField] private float maxHealthPoints;
    [SerializeField] private float maxDelayAttack;
    [SerializeField] private float maxDamage;
    [SerializeField] private float _playerMoveSpeed;

    [SerializeField] private GameObject _enemyAttacker;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Image _healthLine;
    [SerializeField] private Text _infoText;
    [SerializeField] private Transform shootPlace;
    [SerializeField] private DynamicJoystick _joystick;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private Rigidbody _rb;

    private PlayerEnemyScaner _scaner;

    IEnumerator dieCoroutine;
    //IEnumerator bonusInfo;


    


    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        healthPoints = maxHealthPoints;
        delayAttack = maxDelayAttack;
        _bullet.GetComponent<Bullet>().damage = maxDamage;

        _scaner = PlayerEnemyScaner.instance;
        _joystick = DynamicJoystick.instance;

        dieCoroutine = PlayerDie();
    }

    private void FixedUpdate()
    {
        Move();
        Attack();
    }


    private void Attack()
    {
        if (_joystick != null && (_joystick.Horizontal == 0 || _joystick.Vertical == 0) && isWalk == false)

            if (_scaner.enemyDetected == false)
            playerIdle.Invoke();

            else if (_scaner.enemyDetected == true)
            {
                playerAttack?.Invoke();
                StartCoroutine(ShootAction());
            }

            else
                StopAllCoroutines();
    }

    
    private void Move()
    {
        _rb.velocity = new Vector3(_joystick.Horizontal * _playerMoveSpeed, _rb.velocity.y, _joystick.Vertical * _playerMoveSpeed);
        
        if (_joystick.Vertical != 0 || _joystick.Horizontal != 0)
        {
            playerMove.Invoke();
            _rb.rotation = Quaternion.LookRotation(_rb.velocity);
            isWalk = true;
        }

        else isWalk = false;
    }
    

    private void Die()
    {
        _healthLine.fillAmount = healthPoints / maxHealthPoints;

        if (healthPoints < 1)
        {
            isDie = true;
            StartCoroutine(dieCoroutine);
            StopAllCoroutines();
            Destroy(gameObject, 1);
        }
    }


    private void OnCollisionEnter(Collision collision) //попадання пулі
    {
        if (collision.gameObject.CompareTag(Tags.enemyBullet))
        {
            playerGetHit?.Invoke();
            healthPoints -= collision.gameObject.GetComponent<EnemyBullet>().damage;
        }
        
        else if (collision.gameObject.CompareTag(Tags.healthBonus))
        {
            healthPoints += Bonuses.instance.healthNumber;

            if (healthPoints > maxHealthPoints)
                healthPoints = maxHealthPoints;

            StartCoroutine(DownPanelInfo(Info.UpHealth));
        }

        else if (collision.gameObject.CompareTag(Tags.damageBonus))
        {
            _bullet.GetComponent<Bullet>().damage += Bonuses.instance.damageNumber;
            StartCoroutine(DownPanelInfo(Info.UpDamage));
            Destroy(collision.gameObject);
        }
        
        else if (collision.gameObject.CompareTag(Tags.speedAttackBonus))
        {
            getShootBonus?.Invoke();
            delayAttack -= Bonuses.instance.delayAttackNumber;
            StartCoroutine(DownPanelInfo(Info.UpSpeedAttack));
        }
            
        else return;

        Die();
    }


    private void OnCollisionStay(Collision collision) //нападч
    {
        if (collision.gameObject.CompareTag(Tags.enemy))
        {
            
            PlayerPrefs.SetFloat(Names.playerHp, healthPoints);
            _enemyAttacker = collision.gameObject;
            StartCoroutine(DamageAction());
        }
        else return;
    }


    IEnumerator DamageAction() //напад
    {
        yield return new WaitForSeconds(0.1f);

        if(_enemyAttacker.GetComponent<EnemyDamageSys>() != null)
        healthPoints -= _enemyAttacker.GetComponent<EnemyDamageSys>().damage;
        playerGetHit?.Invoke();
        StopAllCoroutines();
    }


    IEnumerator ShootAction() //постріл
    {
        yield return new WaitForSeconds(delayAttack);
        playerShoot?.Invoke();
        Instantiate(_bullet, shootPlace.position, shootPlace.rotation);
        StopAllCoroutines();
    }


    IEnumerator PlayerDie()
    {
        yield return new WaitForSeconds(0);
        
        playerDie?.Invoke();
        gameObject.tag = "Untagged";
        _rb.isKinematic = true;
        _capsuleCollider.isTrigger = true;
        StopCoroutine(dieCoroutine);
    }


    IEnumerator DownPanelInfo(string textInfo)
    {
        yield return new WaitForSeconds(0);
        _infoText.text = textInfo;

        yield return new WaitForSeconds(1);
        _infoText.text = "";
    }
}
