using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterConnectCheck : MonoBehaviour{
	public int layerAmount = 5;
	public int outputAmount = 3;
	public GameObject[] tileList;

	[HideInInspector]
	public bool[,] connectedWater;	
	public GameObject[,] outputObjects;

	private void Awake() {
		connectedWater = new bool[layerAmount, outputAmount];
		outputObjects = new GameObject[layerAmount, outputAmount];
		FindOutputs();
	}

	public void FindOutputs() {
		for(int i = 0; i < tileList.Length; i++) {
			for(int j = 0; j < 3; j++) {
				outputObjects[i, j] = tileList[i].transform.GetChild(j).gameObject;
			}
		}
	}

	public int GetTotalOutputs() {
		return (layerAmount * outputAmount);
	}

	public int ConnectedOnLayer(int layerId) {
		int counter = 0;
		for(int i = 0; i < outputAmount; i++) {
			if(connectedWater[layerId, i] == true) {
				counter++;
			}
		}

		return counter;
	}

	public int ConnectedTotal() {
		int counter = 0;

		for(int layerId = 0; layerId < layerAmount; layerId++) {
			for(int i = 0; i < outputAmount; i++) {
				if(connectedWater[layerId, i] == true) {
					counter++;
				}
			}
		}

		return counter;
	}

	public void CheckConnected() {
		connectedWater[0,0] = outputObjects[0, 0].GetComponent<ConnectBool>().waterConnected;
	}
}
