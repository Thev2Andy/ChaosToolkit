using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    public bool OneUseOnly;
    public Transform TeleportTarget;
    public bool DestroyTeleportTarget;

    private void OnTriggerEnter2D(Collider2D c)
    {
        c.gameObject.transform.position = TeleportTarget.position;

        if(DestroyTeleportTarget) Destroy(TeleportTarget);
        if(OneUseOnly) Destroy(gameObject);
    }
}
