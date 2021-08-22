using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController2D controller;

    public Animator PlayerAnimator;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	
	// Update is called once per frame
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

        PlayerAnimator.SetBool("Grounded", controller.m_Grounded);
        PlayerAnimator.SetBool("Walking", ((horizontalMove != 0) ? true : false));

	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
		jump = false;
	}
}
