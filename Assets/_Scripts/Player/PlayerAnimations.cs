using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private const string IdleTriggerName = "Idle";
    private const string RunTriggerName = "Run";
    private const string AttackTriggerName = "Attack";
    private const string DieTriggerName = "Die";

    [SerializeField] private Animator _animator;



    private void OnEnable()
    {
        PlayerController.playerMove += PlayerMoveAnimation;
        PlayerController.playerAttack += PlayerAttackAnimation;
        PlayerController.playerIdle += PlayerIdleAnimation;
        PlayerController.playerDie += PlayerDieAnimation;
    }


    private void OnDisable()
    {
        PlayerController.playerMove -= PlayerMoveAnimation;
        PlayerController.playerAttack -= PlayerAttackAnimation;
        PlayerController.playerIdle -= PlayerIdleAnimation;
        PlayerController.playerDie -= PlayerDieAnimation;
    }


    public void PlayerIdleAnimation()
    {
        _animator.SetTrigger(IdleTriggerName);
        _animator.speed = 1;
    }


    public void PlayerMoveAnimation()
    {
       _animator.SetTrigger(RunTriggerName);
        _animator.speed = 1;
    }


    public void PlayerAttackAnimation()
    {
        _animator.SetTrigger(AttackTriggerName);
        _animator.speed = 1 / PlayerController.instance.delayAttack;
    }


    public void PlayerDieAnimation()
    {
        _animator.SetBool(DieTriggerName, true);
        _animator.speed = 1;
    }
}
