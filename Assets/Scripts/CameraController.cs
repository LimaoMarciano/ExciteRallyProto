using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;
	public float offset = 0;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (target != null) {

			transform.position = new Vector3 (transform.position.x, transform.position.y, target.position.z + offset);

		}
	}
}
