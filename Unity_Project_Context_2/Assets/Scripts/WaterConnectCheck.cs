using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterConnectCheck : MonoBehaviour{
	public int layerAmount = 5;
	public int outputAmount = 3;
	public GameObject[] tileList;
	public float pointGainDelay = 2f;
	public int pointGainAmount = 100;

	[Header("overflow")]
	public float waterOverflowDelay = 3f;
	public float waterOverflowRaise = 0.5f;
	public Transform waterRaise;

	[HideInInspector]
	public bool[,] connectedWater;	
	public GameObject[,] outputObjects;

	private void Awake() {
		connectedWater = new bool[layerAmount, outputAmount];
		outputObjects = new GameObject[layerAmount, outputAmount];
		FindOutputs();
	}

	private void Start() {
		StartCoroutine(CheckPointGain());
		StartCoroutine(CheckWaterOverflow());
	}

	private void LateUpdate() {
		CheckConnected();
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
		for(int i = 0; i < layerAmount; i++) {
			for(int j = 0; j < outputAmount; j++) {
				connectedWater[i, j] = outputObjects[i, j].GetComponent<ConnectBool>().waterConnected;
			}
		}
	}

	IEnumerator CheckPointGain() {
		while (true) {
			yield return new WaitForSeconds(pointGainDelay);
			Currency.Instance.AddToCurrency(ConnectedTotal() * pointGainAmount);
		}
	}

	IEnumerator CheckWaterOverflow() {
		while(waterRaise.localPosition.y < 0) {
			yield return new WaitForSeconds(waterOverflowDelay);
			//float punishmentLevel = (GetTotalOutputs() / layerAmount) - ConnectedTotal();
			float punishmentLevel = 3 - ConnectedTotal();
			if(punishmentLevel < 0) {
				punishmentLevel = 0;
			}

			float newPosY = waterRaise.localPosition.y + (waterOverflowRaise * punishmentLevel);

			while (waterRaise.localPosition.y < newPosY) {
				waterRaise.localPosition += new Vector3(0, Time.deltaTime, 0);
				yield return null;
			}
		}
	}
}
