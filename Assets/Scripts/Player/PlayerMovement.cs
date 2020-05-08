using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float groundSpeed = 8f;
	public float airSpeed = 3f;
	public float maxFallSpeed = -25f;
	public float fallingGravity = 4f;
	public float normalGravity = 1f;
	public float hipsOffset = .4f;
	public float footOffset = .4f;
	public float kneeOffset = .4f;
	public float chestOffset = .4f;
	public float groundDistance = .87f;
	public float wallDistance = 1f;
	public float ceilingDistance = 2f;
	public LayerMask groundLayer;
	public LayerMask wallLayer;

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

	private bool isFalling = false;

	private float maxVelocityUp = 12f;
	private float maxVelocityDown = -30f;

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
		Vector2 lOffset = new Vector2(-hipsOffset, footOffset);
		Vector2 rOffset = new Vector2(hipsOffset, footOffset);
		Vector2 vOffset = new Vector2(0f, kneeOffset);
		RaycastHit2D leftCheck = Physics2D.Raycast(pos + lOffset, Vector2.down, groundDistance, groundLayer);
		RaycastHit2D rightCheck = Physics2D.Raycast(pos + rOffset, Vector2.down, groundDistance, groundLayer);
		RaycastHit2D lWallCheck = Physics2D.Raycast(pos + vOffset, Vector2.left, wallDistance, wallLayer);
		RaycastHit2D rWallCheck = Physics2D.Raycast(pos + vOffset, Vector2.right, wallDistance, wallLayer);
		RaycastHit2D urWallCheck = Physics2D.Raycast(pos, Vector2.up, ceilingDistance, wallLayer);

		/**
		Color color = leftCheck ? Color.red : Color.green;
		Debug.DrawRay(pos + lOffset, Vector2.down * groundDistance, color);
		color = rightCheck ? Color.red : Color.green;
		Debug.DrawRay(pos + rOffset, Vector2.down * groundDistance, color);
		Debug.DrawRay(pos + vOffset, Vector2.left * wallDistance, color);
		Debug.DrawRay(pos + vOffset, Vector2.right * wallDistance, color);
		Debug.DrawRay(pos, Vector2.up * ceilingDistance, Color.magenta);
		/**/

		if (leftCheck || rightCheck)
		{

			if (rigidBody.velocity.y <= 0)
			{
				isJumping = false;
			}			
			isFalling = false;
			bodyCollider.enabled = true;
			spriteRenderer.sprite = Iddle;
			rigidBody.gravityScale = normalGravity;
			
		}
		
		/*if (leftCheck && rightCheck)
		{
			isJumping = false;
		}*/

		if ( lWallCheck || rWallCheck || urWallCheck )
		{
			bodyCollider.enabled = true;
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
		Vector2 vOffset = new Vector2(0f, chestOffset);
		Vector2 pos = transform.position;
		RaycastHit2D lWallCheck = Physics2D.Raycast(pos + vOffset, Vector2.left, wallDistance, groundLayer);
		RaycastHit2D rWallCheck = Physics2D.Raycast(pos + vOffset, Vector2.right, wallDistance, groundLayer);

		/**
		Color color = lWallCheck ? Color.green : Color.magenta;
		Debug.DrawRay(pos + vOffset, Vector2.left * wallDistance, color);
		color = rWallCheck ? Color.green : Color.magenta;
		Debug.DrawRay(pos + vOffset, Vector2.right * wallDistance, color);
		/**/

		if (input.jumpPressed && !isJumping)
		{
			isJumping = true;
			rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
			jumpHeight = this.transform.position.y;			

			if (!lWallCheck && !rWallCheck)
			{
				bodyCollider.enabled = false;
			}		

		}

		if (isJumping)
		{
			// check max height is reached
			if ((this.transform.position.y - jumpHeight) >= 2f)
			{
				rigidBody.gravityScale = fallingGravity;
				isFalling = true;
			}

		}

		if (rigidBody.velocity.y < -0.5f)
		{
			rigidBody.gravityScale = fallingGravity;
		}

		if (rigidBody.velocity.y < -9f)
		{
			isFalling = true;
		}

		if (rigidBody.velocity.y > maxVelocityUp)
		{
			rigidBody.velocity = new Vector2(rigidBody.velocity.x, maxVelocityUp);
		}

		if (rigidBody.velocity.y < maxVelocityDown)
		{
			rigidBody.velocity = new Vector2(rigidBody.velocity.x, maxVelocityDown);
			bodyCollider.enabled = true;
		}

		if (isFalling)
		{
			jumpHeight = 0f;
			spriteRenderer.sprite = Falling;
		}


		if (rigidBody.velocity.y >= 0f)
		{
			spriteRenderer.sprite = Iddle;
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
