using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelVisual : MonoBehaviour {

	private WheelCollider col;
	private Transform visualWheel;

	// Use this for initialization
	void Start () {
		col = GetComponent<WheelCollider> ();
		visualWheel = col.transform.GetChild (0);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position;
		Quaternion rotation;
		col.GetWorldPose(out position, out rotation);

		visualWheel.transform.position = position;
		visualWheel.transform.rotation = rotation;
	}
}
