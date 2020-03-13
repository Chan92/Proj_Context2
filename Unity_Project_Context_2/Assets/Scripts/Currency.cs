using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour{
	public Text pointText;
	public int startCurrency = 500;

	private int hiddenPoints;

	public int myCurrency {
		get;
		private set;
	}

	public static Currency Instance 
	{
		get;
		private set;
	}

	private void Awake() 
	{
		if(Instance == null) 
			Instance = this;
		else 
			Destroy(gameObject);
	}

	private void Start() {
		AddToCurrency(startCurrency);
	}

	//connect complete
	public void AddToCurrency(int amount) {
		myCurrency += amount;
		hiddenPoints += amount;
		pointText.text = "" + myCurrency;
	}

	//shop
	public void RemoveFromCurrency(int amount) {
		myCurrency -= amount;
		pointText.text = "" + myCurrency;
	}
}
