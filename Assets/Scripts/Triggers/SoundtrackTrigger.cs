using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackTrigger : MonoBehaviour
{
    public string Soundtrack;
    public bool OneUseOnly;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            if (Soundtrack != "")
            {
                SoundtrackController.Instance.ChangeSoundtrack(Soundtrack);
            }else
            {
                SoundtrackController.Instance.StopSoundtrack();
            }

            if(OneUseOnly) Destroy(gameObject);
        }
    }
}
