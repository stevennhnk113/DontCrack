using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {

	public Transform EggTransform;
	public float OffSet = 10f;

	void Update()
	{

		if(EggTransform.position.y < transform.position.y)
		{
			transform.position = new Vector3(transform.position.x, EggTransform.position.y, transform.position.z);
		}
	}
}
