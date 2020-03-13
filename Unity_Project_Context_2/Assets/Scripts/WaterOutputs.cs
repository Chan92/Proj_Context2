using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOutputs : MonoBehaviour{
	public static WaterOutputs instance;
	public float newFlowDelay = 3f, waterChangeDelay = 7f;
	[SerializeField]
	private GameObject[] waterOutputsGroup;
	private OutputInfo[,] outputsList;

	private void Awake() {
		instance = this;
	}

	private void Start() {
		GetOutputs();
		SetNewWaterFlow(0, WaterTypes.CleanWater);
		StartCoroutine(SetWaterOverTime());
		//StartCoroutine(ChangeWaterOverTime());
	}

	void GetOutputs() {
		outputsList = new OutputInfo[5,3];

		for(int i = 0; i < waterOutputsGroup.Length; i++) {
			int id = 0;
			foreach(Transform outputObj in waterOutputsGroup[i].transform) {
				outputsList[i, id] = outputObj.GetComponent<OutputInfo>();
				id++;
			}
		}
	}

	public void SetNewWaterFlow (int floorId, int outputId, WaterTypes type) {
		floorId = (floorId > 7) ?  7 : Mathf.Abs(floorId);
		outputId = (outputId > 3) ? 3 : Mathf.Abs(outputId);

		outputsList[floorId, outputId].SetWaterType(WaterTypes.CleanWater);
	}

	public void SetNewWaterFlow (int floorId, WaterTypes type) {
		floorId = (floorId > 7) ? 7 : Mathf.Abs(floorId);
		int outputId = Random.Range(0, 3);

		outputsList[floorId, outputId].SetWaterType(type);
	}

	public void SetNewWaterFlow (WaterTypes type) {
		int floorId = Random.Range(0, FloorManager.instance.unlockedFloors);
		int outputId = Random.Range(0, 3);

		outputsList[floorId, outputId].SetWaterType(type);
	}

	IEnumerator SetWaterOverTime() {
		while(true) {
			yield return new WaitForSeconds(newFlowDelay);
			SetNewWaterFlow(WaterTypes.CleanWater);
		}
	}

	IEnumerator ChangeWaterOverTime() {
		while(true) {
			yield return new WaitForSeconds(waterChangeDelay);
			
			//get random water apart of noWater
			int randomWater = Random.Range(1, 4);
			Vector2Int id = GetRandomOutputIdWithWater();

			SetNewWaterFlow(id.x, id.y, (WaterTypes)randomWater);			
		}		
	}

	Vector2Int GetRandomOutputIdWithWater() {
		int floorId = Random.Range(0, FloorManager.instance.unlockedFloors);
		int outputId = Random.Range(0, 3);

		for(int f = 0; f < 7; f++) {
			for(int i = 0; i < 3; i++) {
				if(outputsList[floorId, outputId].watertype == WaterTypes.NoWater) {
					outputId = (outputId + 1) % 3;
					print("new random output: " + outputId);
				} else {
					return new Vector2Int(floorId, outputId);
				}
			}

			floorId = (floorId + 1) % 7;
			print("new random floor: " + floorId);
		}

		return Vector2Int.zero;
	}
}
