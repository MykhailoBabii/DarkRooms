using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float damage;

    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private int speedForce;


    void Start()
    {
        Destroy(gameObject, 10);
        _rigidbody.velocity = transform.forward * speedForce;
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
    
}
