using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTrigger : MonoBehaviour
{
    public bool OneUseOnly;
    public bool UseWhenFullHP;

    public AudioClip UseSound;

    private void OnTriggerEnter2D(Collider2D c)
    {
        HealthSystem hs = c.gameObject.GetComponent<HealthSystem>();
        if (c.gameObject.CompareTag("Player") && hs != null)
        {
            if (hs.Dead || hs.Damaged == false && UseWhenFullHP == false) return;

            hs.Damaged = false;

            Camera.main.GetComponent<AudioSource>().PlayOneShot(UseSound);

            if (OneUseOnly) Destroy(gameObject);
        }
    }
}
