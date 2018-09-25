using UnityEngine;

public class Grass : MonoBehaviour {

	public float DistanceFromRightPlatform = 0.6f;

	public void ConnectRopeEnd(Rigidbody2D rightGRassRB)
	{
		HingeJoint2D hookHingeRB = transform.Find("Hook").gameObject.AddComponent<HingeJoint2D>();
		hookHingeRB.autoConfigureConnectedAnchor = false;
		hookHingeRB.connectedBody = rightGRassRB;
		hookHingeRB.anchor = Vector2.zero;
		hookHingeRB.connectedAnchor = new Vector2(0f, 0.3f);
	}
}
