using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BonusTupe
{
    health,
    speedAttack,
    damage
}

public class Bonuses : MonoBehaviour
{
    public static Bonuses instance;

    public static Action playerGetBonus;

    public float healthNumber;
    public float damageNumber;
    public float delayAttackNumber;

    private int randomIndex;

    [SerializeField] private GameObject _bonusModel;
    [SerializeField] private GameObject[] _bonusVariante;

    [SerializeField] private BonusTupe _bonusTupe;



    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        BonusRandomaser();
        BonusInstantiate();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(Tags.player))
        {
            playerGetBonus?.Invoke();
            Destroy(gameObject);
        }
    }


    private void BonusRandomaser()
    {
        var numberOfBonuses = Enum.GetNames(typeof(BonusTupe)).Length;
        randomIndex = UnityEngine.Random.Range(0, numberOfBonuses);

        _bonusModel = _bonusVariante[randomIndex];

        _bonusTupe = (BonusTupe)randomIndex;

        if (randomIndex == (int)BonusTupe.health)
            tag = Tags.healthBonus;

        if (randomIndex == (int)BonusTupe.speedAttack)
            tag = Tags.speedAttackBonus;

        if (randomIndex == (int)BonusTupe.damage)
            tag = Tags.damageBonus;
    }


    private void BonusInstantiate()
    {
        var bonusPlace = new Vector3(transform.position.x, -3, transform.position.z);
        var bonus = Instantiate(_bonusModel, bonusPlace, Quaternion.identity) as GameObject;
        bonus.transform.parent = gameObject.transform;
    }
}

