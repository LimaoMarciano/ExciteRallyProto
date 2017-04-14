using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof (OrbitCamera))]
public class CameraController : MonoBehaviour {

	public int playerId = 0;
	public Transform target;
	public float horizontalSpeed = 80.0f;
	public float verticalSpeed = 50.0f;

	private Player player;
	private OrbitCamera orbitCamera;

	private float horizontalInput = 0.0f;
	private float verticalInput = 0.0f;

	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer (playerId);
		orbitCamera = GetComponent<OrbitCamera> ();
		orbitCamera.target = target;
	}

	// Update is called once per frame
	void Update () {
		GetInput ();
		ProcessInput ();
	}

	private void GetInput () {
		horizontalInput = player.GetAxis ("CameraHorizontal");
		verticalInput = player.GetAxis ("CameraVertical");
	}

	private void ProcessInput () {
		orbitCamera.ChangeHorizontalAngle (horizontalInput * horizontalSpeed * Time.deltaTime);
		orbitCamera.ChangeVerticalAngle (verticalInput * verticalSpeed * Time.deltaTime);
	}
}
