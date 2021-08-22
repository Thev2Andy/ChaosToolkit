using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController2D Controller;

    public Animator PlayerAnimator;

	public float RunSpeed = 40f;

	[HideInInspector] public float horizontalMove = 0f;
	bool jump = false;
	
	// Update is called once per frame
	private void Update ()
    {

		horizontalMove = Input.GetAxisRaw("Horizontal") * RunSpeed;

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

        PlayerAnimator.SetBool("Grounded", Controller.m_Grounded);
        PlayerAnimator.SetBool("Walking", ((horizontalMove != 0) ? true : false));

	}

	void FixedUpdate ()
	{
		// Move our character
		Controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
		jump = false;
	}
}
