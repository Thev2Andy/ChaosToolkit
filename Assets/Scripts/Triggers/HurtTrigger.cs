using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtTrigger : MonoBehaviour
{
    public bool AllowKill;
    public bool OneUseOnly;
    public bool DestroyOnEnemyUse;

    private void OnTriggerEnter2D(Collider2D c)
    {
        HealthSystem hs = c.gameObject.GetComponent<HealthSystem>();
        if (c.gameObject.CompareTag("Player") && hs != null)
        {
            if(!AllowKill) hs.Damaged = false;
            hs.TakeDamage();

            if(OneUseOnly) Destroy(gameObject);
        }

        EnemyAI enemy = c.gameObject.GetComponent<EnemyAI>();
        if (c.gameObject.CompareTag("Enemy") && enemy != null)
        {
            if(!AllowKill) enemy.Damaged = false;
            enemy.TakeDamage(null);

            if(OneUseOnly && DestroyOnEnemyUse) Destroy(gameObject);
        }
    }
}
