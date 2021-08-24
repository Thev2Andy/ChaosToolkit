﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBomb : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    public float ExplosionRange;
    public float ExplosionForce;

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (GetComponent<Rigidbody2D>())
        {
            GetComponent<Rigidbody2D>().Sleep();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) Explode();
    }

    public void Explode()
    {
        Destroy(Instantiate(ExplosionPrefab, transform.position, Quaternion.identity), 1.25f);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, ExplosionRange);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.GetComponent<Rigidbody2D>())
            {
                Rigidbody2DExtension.AddExplosionForce(hit.gameObject.GetComponent<Rigidbody2D>(), ExplosionForce, transform.position, ExplosionRange);
            }

            if (hit.gameObject.GetComponent<EnemyAI>())
            {
                hit.gameObject.GetComponent<EnemyAI>().Damaged = true;
                hit.gameObject.GetComponent<EnemyAI>().TakeDamage(null);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if(ExplosionRange > 0) Gizmos.DrawWireSphere(transform.position, ExplosionRange);
    }
}
