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
    public Transform GroundCheck;
    public float GroundedRadius;
    public LayerMask GroundLayers;
    public GameObject[] DeathDrops;

    [Space]
    public bool DisableDrops;
    public GameObject GuaranteedDrop;
    public bool GuaranteedUltimate;
    [Space]

    public Transform DropPoint;
    public AudioClip SwingSound;
    public Transform AttackPoint;
    public float AttackRange;
    public bool Damaged;
    public LayerMask AttackMask;

    // Private / Hidden variables.
    private bool attacking;
    [HideInInspector] public bool Grounded;

    public void TakeDamage(MeleeSystem DamageSender)
    {
        EnemyAnimator.ResetTrigger("Attack");
        EnemyAnimator.SetTrigger("Hurt");
        
        if (Damaged == true)
        {
            EnemyAnimator.SetBool("Dead", true);

            int chance = Random.Range(0, 100);
            if (chance <= 30 && DamageSender != null || GuaranteedUltimate && DamageSender != null ) DamageSender.CanUltimate = true;

            if(chance > 30 && chance <= 45 || GuaranteedDrop != null)
            {
                Rigidbody2D drop = null;

                if(!DisableDrops)
                {
                    if (GuaranteedDrop != null) drop = Instantiate(GuaranteedDrop, DropPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                    if (GuaranteedDrop == null) drop = Instantiate(DeathDrops[Random.Range(0, DeathDrops.Length)], DropPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                }

                if (drop != null)
                {
                    drop.AddForce(new Vector2(0, 165));
                    drop.AddTorque(Random.Range(-27.5f, 27.5f));
                }
            }
            
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
        if (hits.Length > 0 && !attacking && Grounded) StartCoroutine(Attack());

        EnemyAnimator.SetBool("Grounded", Grounded);
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

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, GroundLayers);
        for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				Grounded = true;
			}
		}
    }

    private void OnDrawGizmosSelected()
    {
        if(AttackPoint != null && AttackRange > 0) Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);

        if(GroundCheck != null && GroundedRadius > 0) Gizmos.DrawWireSphere(GroundCheck.position, GroundedRadius);
    }
}
