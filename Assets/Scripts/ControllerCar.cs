using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ControllerCar : MonoBehaviour {

	public int playerId = 0;

	private Player player;
	private Car car;

	private Vector3 directionInput;
	private float acceleratorInput;
	private float brakesInput;

	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer (playerId);
		car = GetComponent<Car> ();
	}
	
	// Update is called once per frame
	void Update () {
		GetInput ();
		ProcessInput ();
	}

	private void GetInput() {
		directionInput.x = player.GetAxis ("Horizontal");
		directionInput.y = -player.GetAxis ("Vertical");
		acceleratorInput = player.GetAxis ("Accelerator");
		brakesInput = player.GetAxis ("Brakes");
	}

	private void ProcessInput() {
		car.targetDirection = new Vector3 (directionInput.y, 0, directionInput.x);

		car.targetAlignment = directionInput.y;

		car.accInput = acceleratorInput;
		car.brakeInput = brakesInput;
	}
}
