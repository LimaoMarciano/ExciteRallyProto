using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

	public enum playerNum { player1, player2 };
	public playerNum player;

	private string horizontal;
	private string vertical;
	private string accelerator;
	private string brakes;

	private Car car;
	private float accInput = 0;
	private float brakesInput = 0;

	// Use this for initialization
	void Start () {
		car = GetComponent<Car> ();

		string pNumber = "";

		switch (player) {
		case playerNum.player1:
			pNumber = "P1";
			break;
		case playerNum.player2:
			pNumber = "P2";
			break;
		}

		horizontal = pNumber + "Horizontal";
		vertical = pNumber + "Vertical";
		accelerator = pNumber + "Accelerator";
		brakes = pNumber + "Brakes";
	}
	
	// Update is called once per frame
	void Update () {
		accInput = Input.GetAxis (accelerator);
		brakesInput = Input.GetAxis (brakes);

		float vert = -Input.GetAxis (vertical);
		float horz = Input.GetAxis (horizontal);

		car.targetDirection = new Vector3 (vert, 0, horz);

		car.targetAlignment = Input.GetAxis (vertical);

		car.accInput = accInput;
		car.brakeInput = brakesInput;
	}
}
