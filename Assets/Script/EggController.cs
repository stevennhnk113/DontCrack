using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EggController : MonoBehaviour
{
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.

	public float k_GroundedRadius = 1f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private Vector3 m_Velocity = Vector3.zero;
	float movement = 0f;
	public float RollingSpeed = 40f;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public float MaxVelocity = -15f;
	private float PreviousVelocity = 0;

	public GameManager GameManager;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	void Update()
	{
		movement = Input.GetAxisRaw("Horizontal") * RollingSpeed;
		PreviousVelocity = Mathf.Min(PreviousVelocity, m_Rigidbody2D.velocity.y);
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if(movement != 0f)
				{
					Move(movement * Time.fixedDeltaTime, false, false);
				}
			}
		}
	}

	public void Move(float move, bool crouch, bool jump)
	{
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
		}
	}

	public void OnLand()
	{
		Debug.Log("Landed");
		Debug.Log(m_Rigidbody2D.velocity);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		
		if (PreviousVelocity < MaxVelocity)
		{
			GameOver();
			Debug.Log("Landed");
			//GameObject.Find("GameManager").gameObject;
		}
	}

	private void GameOver()
	{
		Time.timeScale = 0;
		GameManager.GameOver();
	}
}
