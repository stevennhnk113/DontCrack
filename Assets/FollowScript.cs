using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FollowScript : MonoBehaviour {

	public Transform EggTransform;
	public float OffSet = 10f;

	public UnityEvent OnReachingBottom;

	public GameManager GameManager;

	private float ScreenHeight;
	private float ScreenWidth;

	private Camera Camera;

	void Start()
	{
		Camera = GameObject.Find("MainCamera").GetComponent<Camera>();
		ScreenHeight = 2f * Camera.orthographicSize;
		ScreenWidth = ScreenHeight * Camera.aspect;

		if (OnReachingBottom == null)
		{
			OnReachingBottom = new UnityEvent();
		}
	}

	void Update()
	{
		if(EggTransform.position.y < transform.position.y)
		{
			transform.position = new Vector3(transform.position.x, EggTransform.position.y, transform.position.z);
		}
	}

	void FixedUpdate()
	{
		float cameraBottomYPosition = Camera.transform.position.y - ScreenHeight / 2;
		if(cameraBottomYPosition - 10 < GameManager.CurrentLowestPosition.y)
		{
			GameManager.AddScene();
		}
	}
}
