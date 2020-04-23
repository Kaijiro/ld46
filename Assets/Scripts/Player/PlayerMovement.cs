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
	private bool isJumping;

	public Sprite Iddle;
	public Sprite Falling;

	private float jumpHeight = 0f;

	PlayerInput input;
	Inventory inventory;
	BoxCollider2D bodyCollider;
	Rigidbody2D rigidBody;
	SpriteRenderer spriteRenderer;

	float originalXScale;
	int direction = 1;

	private float initialFrameCount = 0;
	private bool isFalling = false;

	void Start()
	{
		input = GetComponent<PlayerInput>();
		rigidBody = GetComponent<Rigidbody2D>();
		bodyCollider = GetComponent<BoxCollider2D>();
		originalXScale = transform.localScale.x;
		inventory = GetComponent<Inventory>();
		spriteRenderer = GetComponent<SpriteRenderer>();
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
		{
			if (isJumping)
			{
				initialFrameCount += Time.deltaTime;
				Debug.Log("Ground reached in : " + (initialFrameCount));
			}
			isJumping = false;
			isFalling = false;
			spriteRenderer.sprite = Iddle;
			rigidBody.gravityScale = 1f;
		}
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
			initialFrameCount = Time.deltaTime;
			rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
			jumpHeight = this.transform.position.y;
		}

		if (isJumping)
		{
			// check max height is reached
			if ((this.transform.position.y - jumpHeight) >= 2f)
			{
				rigidBody.gravityScale = 4f;
				isFalling = true;
			}

		}

		if (rigidBody.velocity.y < -0.5f)
		{
			rigidBody.gravityScale = 4f;
		}

		if (rigidBody.velocity.y < -9f)
		{
			isFalling = true;
		}

		if (isFalling)
		{
			jumpHeight = 0f;
			spriteRenderer.sprite = Falling;
		}
	}

	void FlipCharacterDirection()
	{
		direction *= -1;
		Vector3 scale = transform.localScale;
		scale.x = originalXScale * direction;
		transform.localScale = scale;
	}


}
