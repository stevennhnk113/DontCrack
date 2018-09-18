using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FollowScript : MonoBehaviour {

	public Transform EggTransform;
	public float OffSet = 10f;

	public UnityEvent OnReachingBottom;

	void Update()
	{
		if(EggTransform.position.y < transform.position.y)
		{
			transform.position = new Vector3(transform.position.x, EggTransform.position.y, transform.position.z);
		}
	}
}
