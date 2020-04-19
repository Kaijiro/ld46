using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float groundSpeed = 8f;
	public float airSpeed = 3f;
	public float maxFallSpeed = -25f;       
	public float footOffset = .4f;          
	public float groundDistance = .87f;     
	public LayerMask groundLayer;           

	public float jumpForce = 6.3f;          
	public bool isJumping;

	PlayerInput input;
	Inventory inventory;
	BoxCollider2D bodyCollider;				
	Rigidbody2D rigidBody;					

	float originalXScale;					
	int direction = 1;						


	void Start ()
	{
		input = GetComponent<PlayerInput>();
		rigidBody = GetComponent<Rigidbody2D>();
		bodyCollider = GetComponent<BoxCollider2D>();
		originalXScale = transform.localScale.x;
		inventory = GetComponent<Inventory>();
	}

	void FixedUpdate()
	{
		PhysicsCheck();
		GroundMovement();
		MidAirMovement();

		if (input.trashPressed)
		{
			inventory.Empty();
			input.trashPressed = false;
		}
	}

	void PhysicsCheck()
	{
		Vector2 pos = transform.position;
		Vector2 lOffset = new Vector2(-footOffset, 0f);
		Vector2 rOffset = new Vector2(footOffset, 0f);
		RaycastHit2D leftCheck = Physics2D.Raycast(pos + lOffset, Vector2.down, groundDistance, groundLayer);
		RaycastHit2D rightCheck = Physics2D.Raycast(pos + rOffset, Vector2.down, groundDistance, groundLayer);

		/**
		Color color = leftCheck ? Color.red : Color.green;
		Debug.DrawRay(pos + lOffset, Vector2.down * groundDistance, color);
		/**/

		if (leftCheck || rightCheck)
			isJumping = false;
	}

	void GroundMovement()
	{
		float speed = groundSpeed;
		if (isJumping)
			speed = airSpeed;
		
		float xVelocity = speed * input.horizontal;
		if (xVelocity * direction < 0f)
			FlipCharacterDirection();

		rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);
	}

	void MidAirMovement()
	{
		if (input.jumpPressed && !isJumping)
		{
			isJumping = true;
			rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
			// AudioManager.PlayJumpAudio();
		}

		if (rigidBody.velocity.y < maxFallSpeed)
			rigidBody.velocity = new Vector2(rigidBody.velocity.x, maxFallSpeed);
	}

	void FlipCharacterDirection()
	{
		direction *= -1;
		Vector3 scale = transform.localScale;
		scale.x = originalXScale * direction;
		transform.localScale = scale;
	}


}
