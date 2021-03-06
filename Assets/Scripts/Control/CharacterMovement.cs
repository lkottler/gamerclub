﻿using Photon.Pun;
using System.Collections.Specialized;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	private PhotonView PV;
	private CharacterController myCC;
	private Animator animator;

	[SerializeField]
	GameObject cam;

	[SerializeField]
	bool isDemo = false;

	public Vector3 moveDirection = Vector3.zero;
	public readonly float gravity = 50.0F;

	//[Header("Settings")]
	private float speed = 0f;
	private float sprintMultiplier = 1.6f;
	private float rotateSpeed = 240f;
	private float jumpSpeed = 26.0F;

	public int jumpsRemaining = 1;
	private bool moving = false, falling = false;
	private Vector3 scalingRate = new Vector3(0.005f, 0.005f, 0.005f);

	private void Start()
	{
		Physics.IgnoreCollision(GetComponent<Collider>(), GetComponent<CharacterController>());
		this.animator = this.GetComponent<Animator>();
		PV = GetComponent<PhotonView>();
		if (PV.IsMine || isDemo)
		{
			cam.SetActive(true);
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			//GetComponentInChildren<Camera>().enabled = true;
			//GetComponentInChildren<AudioListener>().enabled = true;
		}
		myCC = GetComponent<CharacterController>();
	}

    void BasicMovement()
	{
		moving = false;
		if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
		{
			//Debug.Log(jumpsRemaining);
			jumpsRemaining--;
			animator.SetTrigger("Jump_t");
			moveDirection.y = jumpSpeed * LocalStats.scale;
		}
		else if (myCC.isGrounded)
		{
			moveDirection.y = (moveDirection.y < 0) ? 0 : moveDirection.y;
			jumpsRemaining = LocalStats.jumps;
		}
		else
		{
			moveDirection.y -= (moveDirection.y > -50f) ? gravity * Time.deltaTime : 0;
			falling = (moveDirection.y < -20.0f);
		}

		// diagonal fix
		if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
			&& (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)))
			speed /= Mathf.Sqrt(2);

		myCC.Move(moveDirection * Time.deltaTime);
		if (Input.GetKey(KeyCode.W))
		{
			myCC.Move(transform.forward * Time.deltaTime * speed);
			moving = true;
		}
		if (Input.GetKey(KeyCode.A))
		{
			myCC.Move(-transform.right * Time.deltaTime * speed);
			moving = true;
		}
		if (Input.GetKey(KeyCode.S))
		{
			myCC.Move(-transform.forward * Time.deltaTime * speed);
			moving = true;
		}
		if (Input.GetKey(KeyCode.D))
		{
			myCC.Move(transform.right * Time.deltaTime * speed);
			moving = true;
		}
	}

	void BasicRotation()
	{
		if (Input.GetKey(KeyCode.Q))
		{
			transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.E))
		{
			transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
		}

		if (Input.GetMouseButton(1))
        {
			Debug.Log("moving");
			transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0);
        }
	}

	void Update()
	{
		if (PV.IsMine || isDemo)
		{
			if (Input.GetKey(KeyCode.LeftShift))
			{
				speed = LocalStats.runSpeed * sprintMultiplier;
			}
			else
			{
				speed = LocalStats.runSpeed;
			}

			speed *= LocalStats.scale;

			BasicMovement();
			BasicRotation();

			Vector3 properScale = new Vector3(LocalStats.scale, LocalStats.scale, LocalStats.scale);

			if (transform.localScale != properScale)
            {
				if (transform.localScale.magnitude < properScale.magnitude)
                {
					transform.localScale += scalingRate;
                }
                else
                {
					transform.localScale -= scalingRate;
                }
            }

			animator.SetBool("Falling_b", falling);

			if (moving)
			{
				animator.SetFloat("Speed_f", speed/2);
			}
			else
			{
				animator.SetFloat("Speed_f", 0);
			}
		}



	}
}
