// This script controls the player's movement and physics within the game

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public bool drawDebugRaycasts = true;	//Should the environment checks be visualized

	[Header("Movement Properties")]
	public float speed = 8f;				//Player speed
	public float coyoteDuration = .05f;		//How long the player can jump after falling
	public float maxFallSpeed = -25f;       //Max speed player can fall


	[Header("Jump Properties")]
	public float jumpForce = 6.3f;          //Initial force of jump
	public float jumpHoldDuration = .1f;    //How long the jump key can be held

	[Header ("Status Flags")]
	public bool isJumping;					//Is player jumping?

	PlayerInput input;						//The current inputs for the player
	BoxCollider2D bodyCollider;				//The collider component
	Rigidbody2D rigidBody;					//The rigidbody component
	
	float jumpTime;							//Variable to hold jump duration
	float coyoteTime;						//Variable to hold coyote duration
	float playerHeight;						//Height of the player

	float originalXScale;					//Original scale on X axis
	int direction = 1;						//Direction player is facing

	Vector2 colliderStandSize;				//Size of the standing collider
	Vector2 colliderStandOffset;			//Offset of the standing collider

	const float smallAmount = .05f;			//A small amount used for hanging position


	void Start ()
	{
		//Get a reference to the required components
		input = GetComponent<PlayerInput>();
		rigidBody = GetComponent<Rigidbody2D>();
		bodyCollider = GetComponent<BoxCollider2D>();

		//Record the original x scale of the player
		originalXScale = transform.localScale.x;

		//Record the player's height from the collider
		playerHeight = bodyCollider.size.y;

		//Record initial collider size and offset
		colliderStandSize = bodyCollider.size;
		colliderStandOffset = bodyCollider.offset;
	}

	void FixedUpdate()
	{

		//Process ground and air movements
		GroundMovement();		
		MidAirMovement();
	}

	void GroundMovement()
	{

		//Calculate the desired velocity based on inputs
		float xVelocity = speed * input.horizontal;

		//If the sign of the velocity and direction don't match, flip the character
		if (xVelocity * direction < 0f)
			FlipCharacterDirection();

		//Apply the desired velocity 
		rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);
	}

	void MidAirMovement()
	{

		//If the jump key is pressed AND the player isn't already jumping AND EITHER
		//the player is on the ground or within the coyote time window...
		if (input.jumpPressed && !isJumping)
		{

			//...The player is no longer on the groud and is jumping...
			isJumping = true;

			//...record the time the player will stop being able to boost their jump...
			jumpTime = Time.time + jumpHoldDuration;

			//...add the jump force to the rigidbody...
			rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

			//...and tell the Audio Manager to play the jump audio
			// AudioManager.PlayJumpAudio();
		}
		//Otherwise, if currently within the jump time window...
		else if (isJumping)
		{
			//...and if jump time is past, set isJumping to false
			if (jumpTime <= Time.time)
				isJumping = false;
		}

		//If player is falling to fast, reduce the Y velocity to the max
		if (rigidBody.velocity.y < maxFallSpeed)
			rigidBody.velocity = new Vector2(rigidBody.velocity.x, maxFallSpeed);
	}

	void FlipCharacterDirection()
	{
		//Turn the character by flipping the direction
		direction *= -1;

		//Record the current scale
		Vector3 scale = transform.localScale;

		//Set the X scale to be the original times the direction
		scale.x = originalXScale * direction;

		//Apply the new scale
		transform.localScale = scale;
	}

}
