using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTrigger : MonoBehaviour
{
    public bool OneUseOnly;
    public bool UseWhenFullHP;

    private void OnTriggerEnter2D(Collider2D c)
    {
        HealthSystem hs = c.gameObject.GetComponent<HealthSystem>();
        if (c.gameObject.CompareTag("Player") && hs != null)
        {
            if (hs.Dead || hs.Damaged == false && UseWhenFullHP == false) return;

            hs.Damaged = false;

            if (OneUseOnly) Destroy(gameObject);
        }
    }
}
