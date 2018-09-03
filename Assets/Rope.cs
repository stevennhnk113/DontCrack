using UnityEngine;

public class Rope : MonoBehaviour {

	public Rigidbody2D _Hook;
	public GameObject _LinkPrefab;

	public int _NumberOfLinks = 7;

	// Use this for initialization
	void Start () {
		GenerateRope();	
	}

	void GenerateRope()
	{
		Rigidbody2D previousRB = _Hook;

		for(int i = 0; i < _NumberOfLinks; i++)
		{
			GameObject link = Instantiate(_LinkPrefab, transform);
			HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
			joint.connectedBody = previousRB;

			previousRB = link.GetComponent<Rigidbody2D>();
		}
	}
	
}
