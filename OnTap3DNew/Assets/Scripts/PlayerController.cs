using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float originalSpeed;
	[SerializeField] private float rotateSpeed;
	[SerializeField] private float jumpForce;

	private CharacterController characterController;
	private InputController inputController;
	private Animator animator;

	private float speed;
	private Vector3 movement;
	private float currentGravity;
	private float gravity = -0.981f;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		inputController = GetComponent<InputController>();
		animator = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		CharacterMove();
	}

	private void CharacterMove()
	{
		speed = originalSpeed;

		movement = new Vector3(inputController.horizontal, 0, inputController.vertical);
		movement.Normalize();

		if (movement.magnitude > 0)
		{
			Vector3 newRotateDirection = Quaternion.AngleAxis(Camera.main.transform.rotation.eulerAngles.y, Vector3.up) * movement;
			Quaternion toRotation = Quaternion.LookRotation(newRotateDirection);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed);

			if (inputController.GetRun())
			{
				speed = 8f;
			}

			Vector3 moveDirection = newRotateDirection * speed * Time.deltaTime;
			characterController.Move(new Vector3(moveDirection.x, currentGravity, moveDirection.z));

			animator.SetBool("Run", inputController.GetRun());
		}

		if (characterController.isGrounded)
		{
			currentGravity = 0;
			if (inputController.GetJump())
			{
				currentGravity = jumpForce * Time.deltaTime;

				characterController.Move(new Vector3(0, transform.position.y + 3f, 0));
				inputController.SetJump(false);
				animator.SetTrigger("Jump");
			}
		}
		else
		{
			currentGravity += gravity * Time.deltaTime;
		}

		characterController.Move(new Vector3(0, currentGravity, 0));

		animator.SetFloat("Speed", movement.magnitude);
	}
}
