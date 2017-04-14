using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	public bool isAdvancedControl = false;

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

	public Vector3 targetDirection;

	private bool isReversing = false;

	void Start () {
		rBody = GetComponent<Rigidbody> ();

//		FRWheel.motorTorque = 0.01f;
//		FLWheel.motorTorque = 0.01f;
//		RRWheel.motorTorque = 0.01f;
//		RLWheel.motorTorque = 0.01f;

		rBody.centerOfMass = centerOfMass.localPosition;
	}

	void FixedUpdate () {
//		RRWheel.motorTorque = accInput * engineTorque;
//		RLWheel.motorTorque = accInput * engineTorque;
		FRWheel.motorTorque = accInput * engineTorque;
		FLWheel.motorTorque = accInput * engineTorque;

		if (rBody.velocity.magnitude < 2 && brakeInput > 0) {
			isReversing = true;
		}

		if (accInput != 0 || brakeInput == 0) {
			isReversing = false;
		}

		if (isReversing) {
//			RRWheel.motorTorque = -brakeInput * engineTorque;
//			RLWheel.motorTorque = -brakeInput * engineTorque;
			FRWheel.motorTorque = -brakeInput * engineTorque;
			FLWheel.motorTorque = -brakeInput * engineTorque;
		} else {
			FRWheel.brakeTorque = brakeInput * brakeTorque;
			FLWheel.brakeTorque = brakeInput * brakeTorque;
			RRWheel.brakeTorque = brakeInput * brakeTorque;
			RLWheel.brakeTorque = brakeInput * brakeTorque;
		}

		Vector3 target;

		if (isAdvancedControl) {
			FRWheel.steerAngle = targetDirection.z * maxSteerAngle;
			FLWheel.steerAngle = targetDirection.z * maxSteerAngle;
		} else {
			if (targetDirection.x == 0 && targetDirection.z == 0) {
				target = transform.forward;
			} else {
				target = targetDirection.normalized;
			}

			float angleDiff = Vector3.Angle (transform.forward, target);
			float angleSign = Vector3.Cross (transform.forward, target).y;

			if (angleSign >= 0) {
				angleSign = 1;
			} else {
				angleSign = -1;
			}

			float steerAngle = Mathf.Clamp (angleDiff * angleSign, -maxSteerAngle, maxSteerAngle);

			Vector3 velocity = rBody.velocity;
			Vector3 localVelocity = transform.InverseTransformDirection (velocity);

			if (localVelocity.z > 0) {
				FRWheel.steerAngle = steerAngle;
				FLWheel.steerAngle = steerAngle;
			} else {
				FRWheel.steerAngle = -steerAngle;
				FLWheel.steerAngle = -steerAngle;
			}
		}
	
//		Debug.Log (FRWheel.motorTorque);

//		Debug.DrawLine (transform.position + transform.forward * 2, transform.position + (transform.forward * 2) + target, Color.red);
//		Debug.Log (angleDiff * angleSign);

//		Debug.Log (rBody.velocity.magnitude);
//		Debug.Log (accInput);

	}
}
