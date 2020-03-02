using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectBool : MonoBehaviour{
	[HideInInspector]
	public bool waterConnected = false, cleanWater = false;

	private GameObject connectedPipe;

	private void LateUpdate() {
		if(connectedPipe != null) {
			waterConnected = connectedPipe.GetComponent<PipeLine>().b_IsWater;
			//cleanWater = connectedPipe.GetComponent<PipeLine>().b_IsWater;
		}
	}

	private void OnTriggerEnter(Collider other) {
		if(other.transform.parent.GetComponent<PipeLine>()) {	
			connectedPipe = other.transform.parent.gameObject;
		}
	}

	private void OnTriggerExit(Collider other) {
		if(other.transform.parent.GetComponent<PipeLine>()) {			
			connectedPipe = null;
			waterConnected = false;
			cleanWater = false;
		}
	}
}