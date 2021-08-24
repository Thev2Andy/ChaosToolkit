using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HealthSystem : MonoBehaviour
{
    public Animator PlayerAnimator;
    public Volume HurtFX;
    public GameObject DeathUI;
    public Collider2D[] PlayerColliders;
    public Collider2D BodyCollider;

    // Private / Hidden variables.
    [HideInInspector] public bool Damaged;
    [HideInInspector] public bool Dead;

    public void TakeDamage()
    {
        if (Dead) return;

        if (GetComponent<Rigidbody2D>()) GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        PlayerAnimator.SetTrigger("Hurt");
        
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
            if (GetComponent<Movement>()) Destroy(GetComponent<Movement>());
            if (GetComponent<CharacterController2D>()) Destroy(GetComponent<CharacterController2D>());
            if (GetComponent<MeleeSystem>()) Destroy(GetComponent<MeleeSystem>());

            DeathUI.SetActive(true);
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

        if (Damaged)
        {
            HurtFX.weight = Mathf.Lerp(HurtFX.weight, 1, 0.05f);
        }else
        {
            HurtFX.weight = Mathf.Lerp(HurtFX.weight, 0, 0.05f);
        }
    }
}
