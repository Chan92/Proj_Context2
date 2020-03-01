using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectBool : MonoBehaviour{
	public bool waterConnected = false;
	public bool cleanWater = false;

	private GameObject connectedPipe;

	private void LateUpdate() {
		if(connectedPipe != null) {
			waterConnected = connectedPipe.GetComponent<PipeLine>().b_IsWater;
			//cleanWater = connectedPipe.GetComponent<PipeLine>().b_IsWater;
		}
	}

	private void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.GetComponent<PipeLine>()) {
			connectedPipe = collision.gameObject;
		}
	}

	private void OnCollisionExit(Collision collision) {
		if(collision.gameObject.GetComponent<PipeLine>()) {
			connectedPipe = null;
			waterConnected = false;
			cleanWater = false;
		}
	}
}