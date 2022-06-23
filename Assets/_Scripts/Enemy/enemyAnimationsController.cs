using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAnimationsController : MonoBehaviour
{
    private const string IdleTriggerName = "Idle";
    private const string RunTriggerName = "Run";
    private const string AttackTriggerName = "Attack";
    private const string ShootTriggerName = "Shoot";
    private const string GetHitTriggerName = "GetHit";
    private const string DieBoolName = "Die";

    [SerializeField] private Animator _animator;


    private void OnEnable()
    {
        enemyMoveSys.enemyMove += EnemyMoveAnimation;
        enemyDamageSys.enemyDamage += EnemyAttackAnimation;
        enemyMoveSys.enemyIdle += EnemyIdleAnimation;
        enemyDamageSys.enemyShoot += EnemyShootAnimation;

        //PlayerController.playerDie += PlayerDieAnimation;
    }


    private void OnDisable()
    {
        enemyMoveSys.enemyMove -= EnemyMoveAnimation;
        enemyDamageSys.enemyDamage -= EnemyAttackAnimation;
        enemyMoveSys.enemyIdle -= EnemyIdleAnimation;
        enemyDamageSys.enemyShoot -= EnemyShootAnimation;

        //PlayerController.playerDie -= PlayerDieAnimation;
    }


    public void EnemyIdleAnimation()
    {
        _animator.SetTrigger(IdleTriggerName);
    }


    public void EnemyMoveAnimation()
    {
        _animator.SetTrigger(RunTriggerName);
    }


    public void EnemyAttackAnimation()
    {
        _animator.SetTrigger(AttackTriggerName);
    }

    public void EnemyShootAnimation()
    {
        _animator.SetTrigger(ShootTriggerName);
    }


    public void EnemyGetHitAnimation()
    {
        //_animator.SetTrigger(GetHitTriggerName);
    }


    public void EnemyDieAnimation()
    {
        _animator.SetBool(DieBoolName, true);
    }
}
