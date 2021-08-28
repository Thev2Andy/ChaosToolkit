using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyResupplyTrigger : MonoBehaviour
{
    public bool OneUseOnly;
    public bool UseWhenFull;
    public bool SilentResupply;

    private void OnTriggerStay2D(Collider2D c)
    {
        HealthSystem hs = c.gameObject.GetComponent<HealthSystem>();
        StickyLauncher sticky = c.gameObject.GetComponent<StickyLauncher>();
        if (c.gameObject.CompareTag("Player") && hs != null)
        {
            if (hs.Dead || sticky.stickyBombs == sticky.MaxStickyBombs && UseWhenFull == false) return;

            sticky.Resupply(SilentResupply);

            if (OneUseOnly) Destroy(gameObject);
        }
    }
}
