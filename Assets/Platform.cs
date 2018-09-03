using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	private Rigidbody2D _LeftGrassRB;
	private Rigidbody2D _RightGrassRB;

	private Grass _RightGrass;
	private Grass _LeftGrass;

	private Rigidbody2D _LeftGrassHookRB;
	private Rigidbody2D _RightGrassHookRB;

	public GameObject LinkPrefab;

	public int NumberOfLinks = 15;

	// Use this for initialization
	void Start () {
		_LeftGrassRB = transform.Find("LeftGrass").gameObject.GetComponent<Rigidbody2D>();
		_RightGrassRB = transform.Find("RightGrass").gameObject.GetComponent<Rigidbody2D>();

		_RightGrass = transform.Find("RightGrass").gameObject.GetComponent<Grass>();
		_LeftGrass = transform.Find("LeftGrass").gameObject.GetComponent<Grass>();

		_RightGrassHookRB = transform.Find("RightGrass").Find("Hook").gameObject.GetComponent<Rigidbody2D>();
		_LeftGrassHookRB = transform.Find("LeftGrass").Find("Hook").gameObject.GetComponent<Rigidbody2D>();

		MakeRope();
	}

	void MakeRope()
	{
		Rigidbody2D previousHookRB = _LeftGrassHookRB;

		for (int i = 0; i < NumberOfLinks; i++)
		{
			GameObject link = Instantiate(LinkPrefab, transform);
			HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
			joint.connectedBody = previousHookRB;

			// Last link
			if(i == NumberOfLinks - 1)
			{
				//HingeJoint2D rightGrassJoint = _RightGrassRB.GetComponent<HingeJoint2D>();

				//_RightGrass.ConnectRopeEnd(previousHookRB);
			}
			else
			{
				previousHookRB = link.GetComponent<Rigidbody2D>();
			}
		}


	}
}
