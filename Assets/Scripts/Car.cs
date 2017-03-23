using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	public Transform centerOfMass;
	public WheelCollider FRWheel;
	public WheelCollider FLWheel;
	public WheelCollider RRWheel;
	public WheelCollider RLWheel;

	private Rigidbody rBody;
	public float accInput;
	public float brakeInput;
	public float engineTorque = 500.0f;
	public float brakeTorque = 200.0f;
	public float maxSteerAngle = 20.0f;

	public float targetAlignment = 0;

	private bool isReversing = false;

	void Start () {
		rBody = GetComponent<Rigidbody> ();

		FRWheel.motorTorque = 0.01f;
		FLWheel.motorTorque = 0.01f;
		RRWheel.motorTorque = 0.01f;
		RLWheel.motorTorque = 0.01f;

		rBody.centerOfMass = centerOfMass.localPosition;
	}

	void FixedUpdate () {
		FRWheel.motorTorque = accInput * engineTorque;
		FLWheel.motorTorque = accInput * engineTorque;

		if (rBody.velocity.magnitude < 2 && brakeInput > 0) {
			isReversing = true;
		}

		if (accInput != 0 || brakeInput == 0) {
			isReversing = false;
		}

		if (isReversing) {
			FRWheel.motorTorque = -brakeInput * engineTorque;
			FLWheel.motorTorque = -brakeInput * engineTorque;
		} else {
			FRWheel.brakeTorque = brakeInput * brakeTorque;
			FLWheel.brakeTorque = brakeInput * brakeTorque;
			RRWheel.brakeTorque = brakeInput * brakeTorque;
			RLWheel.brakeTorque = brakeInput * brakeTorque;
		}

		Vector3 target = new Vector3 (-targetAlignment * 0.5f, 0, 1);
		float roadAlignment = 1 - Vector3.Dot (transform.forward, target.normalized);

		float angleDiff = Vector3.Angle (transform.forward, target);
		float angleSign = Vector3.Cross (transform.forward, target).y;

		if (angleSign >= 0) {
			angleSign = 1;
		} else {
			angleSign = -1;
		}

		float steerAngle = Mathf.Clamp (angleDiff * angleSign, -maxSteerAngle, maxSteerAngle);

		if (isReversing) {
			FRWheel.steerAngle = -steerAngle;
			FLWheel.steerAngle = -steerAngle;
		} else {
			FRWheel.steerAngle = steerAngle;
			FLWheel.steerAngle = steerAngle;
		}

		Debug.DrawLine (transform.position + transform.forward * 2, transform.position + (transform.forward * 2) + target, Color.red);
//		Debug.Log (angleDiff * angleSign);

//		Debug.Log (rBody.velocity.magnitude);
//		Debug.Log (accInput);

	}
}
