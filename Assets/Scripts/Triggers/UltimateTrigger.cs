using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateTrigger : MonoBehaviour
{
    public bool OneUseOnly;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player") && c.gameObject.GetComponent<MeleeSystem>())
        {
            c.gameObject.GetComponent<MeleeSystem>().CanUltimate = true;
            if(OneUseOnly) Destroy(gameObject);
        }
    }
}
