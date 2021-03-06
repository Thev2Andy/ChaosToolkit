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
    public HealthSystem HS;
    public AudioSource SwingSoundSource;
    public AudioClip SwingSound;
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
    private bool lastUltiValue;

    private void Start()
    {
       ultiTimer = UltimateChargeTime * 2f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !PauseMenu.Instance.Paused)
        {
            if (Controller.m_Grounded)
            {
                Attack(false);
            }else if (CanUltimate && !HealthSys.Damaged && Input.GetKey(KeyCode.LeftShift))
            {
                Attack(true);
            }else if(!CanUltimate && Input.GetKey(KeyCode.LeftShift))
            {
                GameUIController.Instance.ShowMessage("Your ulti isn't ready.", 2.75f);
            }
        }

        if(!lastUltiValue && CanUltimate && HS.Damaged == false)
        {
            GameUIController.Instance.ShowMessage("Your ulti is ready.", 2.75f);
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
            PlayerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            PlayerMovement.enabled = false;
        }else
        {
            PlayerMovement.enabled = true;
            PlayerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        lastUltiValue = CanUltimate;
    }

    private void Attack(bool ultimate)
    {
        if (!ultimate) attackMovementPreventionTimer = AttackDuration;

        Collider2D[] hits = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange * (ultimate ? UltimateRangeMultiplier : 1), AttackMask);

        if (ultimate && hits.Length > 0)
        {
            PlayerAnimator.SetTrigger("Attack");
            SwingSoundSource.PlayOneShot(SwingSound);
        }else if (!ultimate)
        {
            PlayerAnimator.SetTrigger("Attack");
            SwingSoundSource.PlayOneShot(SwingSound);
        }

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

                    SubtitleController.Instance.Show("no one walks away", 1.75f);

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
