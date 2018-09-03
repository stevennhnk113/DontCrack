using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {

	private Rigidbody2D _MyRB;

	float movement = 0f;

	// Use this for initialization
	void Start () {
		_MyRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		_MyRB.AddTorque(-_MyRB.velocity.x);
	}
}
