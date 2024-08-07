using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
	public float horizontal;
	public float vertical;

	private bool jump;

	private bool run;

	private void Update()
	{
		horizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
		vertical = Input.GetAxis("Vertical") * Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			jump = true;
		}

		if (Input.GetKey(KeyCode.LeftShift))
		{
			run = true;
		}
		else if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			run = false;
		}
	}

	public bool GetJump()
	{
		return jump;
	}

	public void SetJump(bool jump)
	{
		this.jump = jump;
	}

	public bool GetRun()
	{
		return run;
	}

	public void SetRun(bool run)
	{
		this.run = run;
	}
}
