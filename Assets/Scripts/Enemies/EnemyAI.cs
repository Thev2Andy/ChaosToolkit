using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator EnemyAnimator;
    public Collider2D EnemyCollider;
    public Collider2D BodyCollider;
    public AudioSource SwingSoundSource;
    public AudioClip SwingSound;
    public Transform AttackPoint;
    public float AttackRange;
    public LayerMask AttackMask;

    // Private / Hidden variables.
    [HideInInspector] public bool Damaged;
    private bool attacking;

    public void TakeDamage(MeleeSystem DamageSender)
    {
        EnemyAnimator.SetTrigger("Hurt");
        
        if (Damaged == true)
        {
            EnemyAnimator.SetBool("Dead", true);

            int chance = Random.Range(0, 100);
            if (chance < 30 && DamageSender != null) DamageSender.CanUltimate = true;
            
            BodyCollider.enabled = true;
            gameObject.layer = 10;
            gameObject.name = (gameObject.name + " (Dead)");
            Destroy(EnemyCollider);
            Destroy(gameObject, 300f);
            Destroy(this);
        }

        Damaged = true;
    }

    private void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, AttackMask);
        hits = hits.Distinct().Cast<Collider2D>().ToArray();
        if (hits.Length > 0 && !attacking) StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        attacking = true;
        yield return new WaitForSeconds(0.75f);
        Collider2D[] hits = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, AttackMask);

        hits = hits.Distinct().Cast<Collider2D>().ToArray();

        if (hits.Length > 0)
        {
            EnemyAnimator.SetTrigger("Attack");
            SwingSoundSource.PlayOneShot(SwingSound);
        }

        foreach (Collider2D hostiles in hits)
        {

            if (hostiles.gameObject.GetComponent<HealthSystem>())
            {
                hostiles.gameObject.GetComponent<HealthSystem>().TakeDamage();
            }

            if (hostiles.gameObject.GetComponent<StickyBomb>())
            {
                hostiles.gameObject.GetComponent<StickyBomb>().Explode();
                EnemyAnimator.ResetTrigger("Attack");
            }
        }
        
        attacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if(AttackPoint != null && AttackRange > 0) Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
