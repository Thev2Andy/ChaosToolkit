using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MeleeSystem : MonoBehaviour
{
    public Animator PlayerAnimator;
    public CharacterController2D Controller;
    public Rigidbody2D PlayerRB;
    public Movement PlayerMovement;
    public HealthSystem HealthSys;
    public Volume UltimateFX;
    public float UltimateChargeTime;
    public float AttackDuration; // Used to prevent movement while attacking.
    public Transform AttackPoint;
    public float AttackRange;
    public float UltimateRangeMultiplier;
    public LayerMask AttackMask;

    // Private / Hidden variables.
    private float attackMovementPreventionTimer;
    [HideInInspector] public bool CanUltimate;
    private float ultiTimer;

    private void Start()
    {
       ultiTimer = UltimateChargeTime * 2f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Controller.m_Grounded)
            {
                Attack(false);
            }else if (CanUltimate && !HealthSys.Damaged)
            {
                if (Input.GetKey(KeyCode.LeftShift)) Attack(true);
            }
        }

        if (!CanUltimate)
        {
            ultiTimer -= Time.deltaTime;
            if (ultiTimer < 0) ultiTimer = 0;
            if (ultiTimer <= 0)
            {
                CanUltimate = true;
                ultiTimer = UltimateChargeTime;
            }
        }

        if (CanUltimate)
        {
            UltimateFX.weight = Mathf.Lerp(UltimateFX.weight, 1, 0.02f);
        }else
        {
            UltimateFX.weight = Mathf.Lerp(UltimateFX.weight, 0, 0.02f);
        }

        attackMovementPreventionTimer -= Time.deltaTime;
        if (attackMovementPreventionTimer < 0) attackMovementPreventionTimer = 0;

        if (attackMovementPreventionTimer > 0)
        {
            PlayerRB.velocity = new Vector2(0, PlayerRB.velocity.y);
            PlayerMovement.enabled = false;
        }else
        {
            PlayerMovement.enabled = true;
        }
    }

    private void Attack(bool ultimate)
    {
        if (!ultimate) attackMovementPreventionTimer = AttackDuration;

        Collider2D[] hits = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange * (ultimate ? UltimateRangeMultiplier : 1), AttackMask);

        if (hits.Length > 0) PlayerAnimator.SetTrigger("Attack");

        foreach (Collider2D enemy in hits)
        {
            if (enemy.gameObject.GetComponent<EnemyAI>())
            {
                if (ultimate)
                {
                    if (enemy.gameObject.transform.position.x > PlayerRB.transform.position.x && !Controller.m_FacingRight)
                    {
                        PlayerRB.transform.localScale = new Vector3(PlayerRB.transform.localScale.x * -1, PlayerRB.transform.localScale.y, PlayerRB.transform.localScale.z);
                        Controller.m_FacingRight = true;
                    }else if (enemy.gameObject.transform.position.x < PlayerRB.transform.position.x && Controller.m_FacingRight)
                    {
                        PlayerRB.transform.localScale = new Vector3(PlayerRB.transform.localScale.x * -1, PlayerRB.transform.localScale.y, PlayerRB.transform.localScale.z);
                        Controller.m_FacingRight = false;
                    }

                    PlayerRB.transform.position = enemy.gameObject.transform.position;
                    enemy.gameObject.GetComponent<EnemyAI>().Damaged = true;
                    CanUltimate = false;
                    enemy.gameObject.GetComponent<EnemyAI>().TakeDamage(this);
                }else
                {
                    enemy.gameObject.GetComponent<EnemyAI>().TakeDamage(this);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(AttackPoint != null && AttackRange > 0) Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
