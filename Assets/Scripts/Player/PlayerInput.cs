
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{

	[HideInInspector] public float horizontal;		
	[HideInInspector] public bool jumpPressed;		
	bool readyToClear;								



	void Update()
	{
		ClearInput();

		//If the Game Manager says the game is over, exit
		/*
		if (GameManager.IsGameOver())
			return;
		*/

		
		ProcessInputs();
				
		horizontal = Mathf.Clamp(horizontal, -1f, 1f);
	}

	void FixedUpdate()
	{
		readyToClear = true;
	}

	void ClearInput()
	{
		if (!readyToClear)
			return;
		
		horizontal		= 0f;
		jumpPressed		= false;
		readyToClear	= false;
	}

	void ProcessInputs()
	{
		horizontal		+= Input.GetAxis("Horizontal");
		jumpPressed		= jumpPressed || Input.GetKeyDown("space");
	}

}
