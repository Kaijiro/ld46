
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{

	[HideInInspector] public float horizontal;		
	[HideInInspector] public bool jumpPressed;
	[HideInInspector] public bool trashPressed;
	public bool inQte = false;
	bool readyToClear;								



	void Update()
	{
		ClearInput();

		if (!inQte)
		{
			ProcessInputs();

			horizontal = Mathf.Clamp(horizontal, -1f, 1f);
		}
	}

	void FixedUpdate()
	{
		readyToClear = true;
	}

	void ClearInput()
	{
		if (!readyToClear)
			return;
		
		horizontal = 0f;
		jumpPressed = false;
		readyToClear = false;
		trashPressed = false;
	}

	void ProcessInputs()
	{
		horizontal		+= Input.GetAxis("Horizontal");
		jumpPressed		= jumpPressed || Input.GetKeyDown("space");
		trashPressed = trashPressed || Input.GetKeyDown("t");
	}

}
