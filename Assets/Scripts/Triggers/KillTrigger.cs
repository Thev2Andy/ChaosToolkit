using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D c)
    {
        HealthSystem hs = c.gameObject.GetComponent<HealthSystem>();
        if (c.gameObject.CompareTag("Player") && hs != null)
        {
            hs.Damaged = true;
            hs.TakeDamage();
        }

        EnemyAI enemy = c.gameObject.GetComponent<EnemyAI>();
        if (c.gameObject.CompareTag("Enemy") && enemy != null)
        {
            enemy.Damaged = true;
            enemy.TakeDamage(null);
        }
    }
}
