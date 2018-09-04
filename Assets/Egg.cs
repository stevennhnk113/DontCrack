using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {

	public EggController EggController; 
	
	public float RollingSpeed = 40f;

	private Rigidbody2D _MyRB;

	float movement = 0f;

	// Use this for initialization
	void Start () {
		_MyRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		movement = Input.GetAxisRaw("Horizontal") * RollingSpeed;
	}

	void FixedUpdate()
	{
		EggController.Move(movement * Time.fixedDeltaTime, false, false);

		//if (_MyRB.velocity.x > 0.1f || _MyRB.velocity.x < -0.1f)
		//{
		//	Debug.Log(_MyRB.velocity.x);
		//	_MyRB.AddTorque(-_MyRB.velocity.x);
		//}
	}

	public void OnLand()
	{
		Debug.Log("Landed");
		//_MyRB.AddTorque(20);
	}
}
