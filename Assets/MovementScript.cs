using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

	public float movingSpeed = 1f;

	Transform EggTF;
	public float DistantToOverlap = 2f;

	private float ScreenHeight;
	private float ScreenWidth;
	private float MostLeftX;
	private float MostRightX;

	private Vector3 Size;

	void Start()
	{
		Camera camera = GameObject.Find("MainCamera").GetComponent<Camera>();
		ScreenHeight = 2f * camera.orthographicSize;
		ScreenWidth = ScreenHeight * camera.aspect;
		MostLeftX = -ScreenWidth / 2;
		MostRightX = -MostLeftX;

		Size = this.GetComponent<SpriteRenderer>().bounds.size;

		if(transform.position.x > 0)
		{
			movingSpeed = -movingSpeed;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if(CheckIfHitTheEdge())
		{
			movingSpeed = -movingSpeed;
		}

		transform.position = new Vector3(transform.position.x + (movingSpeed * Time.deltaTime), transform.position.y, transform.position.z);

		Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), new Vector2(2,DistantToOverlap), 0);
		for (int i = 0; i < colliders.Length; i++)
		{
			if(colliders[i].gameObject.tag == "Egg")
			{
				colliders[i].transform.position = new Vector3(colliders[i].transform.position.x + (movingSpeed * Time.deltaTime), colliders[i].transform.position.y, colliders[i].transform.position.z);
			}
		}
	}

	private bool CheckIfHitTheEdge()
	{
		float platformRightEdgeXPosition = transform.position.x + Size.x / 2;
		float platformLeftEdgeXPosition = transform.position.x - Size.x / 2;

		if (platformLeftEdgeXPosition < MostLeftX || platformRightEdgeXPosition > MostRightX) return true;
		else return false;
	}
}
