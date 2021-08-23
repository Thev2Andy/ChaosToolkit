using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator EnemyAnimator;
    public Collider2D EnemyCollider;
    public Collider2D BodyCollider;

    // Private / Hidden variables.
    [HideInInspector] public bool Damaged;

    public void TakeDamage(MeleeSystem DamageSender)
    {
        EnemyAnimator.SetTrigger("Hurt");
        
        if (Damaged == true)
        {
            EnemyAnimator.SetBool("Dead", true);

            int chance = Random.Range(0, 100);
            if (chance < 30 && DamageSender != null) DamageSender.CanUltimate = true;
            
            BodyCollider.enabled = true;
            gameObject.layer = 0;
            gameObject.name = (gameObject.name + " (Dead)");
            Destroy(EnemyCollider);
            Destroy(gameObject, 300f);
            Destroy(this);
        }

        Damaged = true;
    }
}
