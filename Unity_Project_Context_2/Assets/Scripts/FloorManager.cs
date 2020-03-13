using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorManager : MonoBehaviour{
	public static FloorManager instance;
	public Text populationText;
	public Text unlockedFloorsText;

	public GameObject[] layerButtons;
	private Color startCol;

	[SerializeField]
	private int floorRequirement = 10;
	private int maxFloors = 5;

	[SerializeField]
	private float popIncreaseDelay = 5f;

	public int population {
		get;
		private set;
	} 
		
	public int unlockedFloors {
		get;
		private set;
	}

	private int increasedPop;

	private void Awake() {
		instance = this;
	}

	void Start(){
		population = floorRequirement;
		populationText.text = "" + population;
		unlockedFloors = 1;
		for(int i = 1; i < layerButtons.Length -1; i++) {
			layerButtons[i].SetActive(false);
		}
		startCol = layerButtons[0].GetComponent<Image>().color;
		GoToFloor(0);
		StartCoroutine(IncreasePopulationOverTime());
	}

	public void AddPopulation(int amount) {	
		population += amount;
		populationText.text = ""+ population;
		increasedPop += amount;

		if(unlockedFloors < maxFloors) {
			FloorRequirementCheck();
		}
	}

	void FloorRequirementCheck() {
		if(increasedPop >= floorRequirement) {
			increasedPop = 0;
			unlockedFloors++;
			unlockedFloorsText.text = "" + unlockedFloors;
			layerButtons[unlockedFloors - 1].SetActive(true);
		}
	}

	IEnumerator IncreasePopulationOverTime() {
		while(true) {
			yield return new WaitForSeconds(popIncreaseDelay);
			AddPopulation(1);
		}
	}

	public void GoToFloor(int floorId) {
		for(int i = 0; i < layerButtons.Length; i++) {
			layerButtons[i].GetComponent<Image>().color = startCol;
		}

		layerButtons[floorId].GetComponent<Image>().color = Color.cyan;
		PipeLineManager.instance.i_Floor = floorId;
		Vector3 camPos = Camera.main.transform.position;
		camPos.y = 18 - (floorId * PipeLineManager.instance.f_floorDistance);
		Camera.main.transform.position = camPos;
	}

	public void GoToCity() {
		for(int i = 0; i < layerButtons.Length -1; i++) {
			layerButtons[i].GetComponent<Image>().color = startCol;
		}

		layerButtons[layerButtons.Length -1].GetComponent<Image>().color = Color.cyan;
		//Vector3 camPos = Camera.main.transform.position;
		//camPos.y = 0;
		//Camera.main.transform.position = camPos;
	}
}
