using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private MeshRenderer _meshRenderer;

    [SerializeField] private int speedForce;
    [SerializeField] private float lifeTime = 5;


    void Start()
    {
        Destroy(gameObject, lifeTime);
        _rigidbody.velocity = transform.forward * speedForce;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.wall) || collision.gameObject.CompareTag(Tags.enemy))
        {
            Destroy(gameObject);
        }
    }
}
