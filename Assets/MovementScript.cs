using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

	public float movingSpeed = 1f;

	Transform EggTF;
	public float Radius = 5f;

	void Start()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		transform.position = new Vector3(transform.position.x + (movingSpeed * Time.deltaTime), transform.position.y, transform.position.z);

		Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), new Vector2(2,0.2f), 0);
		for (int i = 0; i < colliders.Length; i++)
		{
			if(colliders[i].gameObject.tag == "Egg")
			{
				Debug.Log("Egg");
				colliders[i].transform.position = new Vector3(colliders[i].transform.position.x + (movingSpeed * Time.deltaTime), colliders[i].transform.position.y, colliders[i].transform.position.z);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D cold)
	{
		movingSpeed = -movingSpeed;
	}
}
