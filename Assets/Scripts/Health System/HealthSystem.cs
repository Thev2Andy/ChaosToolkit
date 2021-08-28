using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HealthSystem : MonoBehaviour
{
    public Animator PlayerAnimator;
    public float InvulnerabilityPeriod;
    public float DamageDuration;
    public Rigidbody2D PlayerRB;
    public Movement PlayerMovement;
    public Volume HurtFX;
    public GameObject DeathUI;
    public Collider2D[] PlayerColliders;
    public bool Damaged;
    public Collider2D BodyCollider;

    // Private / Hidden variables.
    [HideInInspector] public bool Dead;
    private float damageMovementPreventionTimer;
    private float invulnerableTimer;

    public void TakeDamage()
    {
        if (Dead || invulnerableTimer > 0 || PauseMenu.Instance.Paused) return;

        invulnerableTimer = InvulnerabilityPeriod;

        damageMovementPreventionTimer = DamageDuration;

        if (GetComponent<Rigidbody2D>()) GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        PlayerAnimator.SetTrigger("Hurt");

        MeleeSystem melee = GetComponent<MeleeSystem>();
        if (melee != null && melee.CanUltimate)
        {
            GameUIController.Instance.ShowMessage("You lost your ulti!", 2.75f);
            melee.CanUltimate = false;
        }
        
        if (Damaged == true)
        {
            PlayerAnimator.SetBool("Dead", true);
            Dead = true;
            
            BodyCollider.enabled = true;
            gameObject.layer = 10;
            gameObject.name = (gameObject.name + " (Dead)");
            
            for (int i = 0; i < PlayerColliders.Length; i++)
            {
                Destroy(PlayerColliders[i]);
            }

            // Add here any other components that need to be destroyed.
            if (PlayerMovement != null) Destroy(PlayerMovement);
            if (GetComponent<CharacterController2D>()) Destroy(GetComponent<CharacterController2D>());
            if (melee != null) Destroy(melee);

            StickyLauncher sticky = GetComponent<StickyLauncher>();
            if (sticky != null)
            {
                if(sticky.StickyInstance != null && sticky.StickyInstance.GetComponent<StickyBomb>()) sticky.StickyInstance.GetComponent<StickyBomb>().Explode();
                Destroy(sticky);
            }

            DeathUI.SetActive(true);
        }else
        {
            SubtitleController.Instance.Show("ouch", 2.75f);
        }

        Damaged = true;
    }

    private void Update()
    {
        if (Dead)
        {
            PlayerAnimator.SetBool("Grounded", true);
            PlayerAnimator.SetBool("Dead", true);
        }

        invulnerableTimer -= Time.deltaTime;
        if (invulnerableTimer < 0) invulnerableTimer = 0;

        damageMovementPreventionTimer -= Time.deltaTime;
        if (damageMovementPreventionTimer < 0) damageMovementPreventionTimer = 0;

        if (damageMovementPreventionTimer > 0)
        {
            PlayerRB.velocity = new Vector2(0, PlayerRB.velocity.y);
            if(PlayerMovement != null) PlayerMovement.enabled = false;
        }else
        {
            if(PlayerMovement != null) PlayerMovement.enabled = true;
        }

        if (Damaged)
        {
            HurtFX.weight = Mathf.Lerp(HurtFX.weight, 1, 0.05f);
        }else
        {
            HurtFX.weight = Mathf.Lerp(HurtFX.weight, 0, 0.05f);
        }
    }
}
