﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MeleeSystem : MonoBehaviour
{
    public Animator PlayerAnimator;
    public CharacterController2D Controller;
    public Rigidbody2D PlayerRB;
    public Volume UltimateFX;
    public float AttackDuration; // Used to prevent movement while attacking.
    public Transform AttackPoint;
    public float AttackRange;
    public float UltimateRangeMultiplier;
    public LayerMask AttackMask;

    // Private / Hidden variables.
    private float attackMovementPreventionTimer;
    [HideInInspector] public bool CanUltimate;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Controller.m_Grounded)
            {
                Attack(false);
            }else if (CanUltimate)
            {
                Attack(true);
            }
        }

        if (CanUltimate)
        {
            UltimateFX.weight = Mathf.Lerp(UltimateFX.weight, 1, 0.01f);
        }else
        {
            UltimateFX.weight = Mathf.Lerp(UltimateFX.weight, 0, 0.01f);
        }

        attackMovementPreventionTimer -= Time.deltaTime;
        if (attackMovementPreventionTimer < 0) attackMovementPreventionTimer = 0;

        PlayerRB.simulated = ((attackMovementPreventionTimer <= 0) ? true : false);
    }

    private void Attack(bool ultimate)
    {
        PlayerAnimator.SetTrigger("Attack");

        if (!ultimate) attackMovementPreventionTimer = AttackDuration;

        Collider2D[] hits = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange * (ultimate ? UltimateRangeMultiplier : 1), AttackMask);

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