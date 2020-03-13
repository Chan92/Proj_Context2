using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeBreaker : MonoBehaviour{
	public static PipeBreaker instance;
	public Text breakerAmountText;

	public int breakerAmount {
		get;
		private set;
	}

	private void Awake() {
		instance = this;
	}

	private void Start() {
		AddBreaker();
	}

	public void AddBreaker() {
		breakerAmount += 1;
		breakerAmountText.text = "" + breakerAmount;
	}

	public bool UseBreaker() {
		if(breakerAmount > 0) {
			//print("breaking");
			breakerAmount--;
			breakerAmountText.text = ""+ breakerAmount;
			return true;
		} else {
			//print("no breaker");
			return false;
		}
	}
}
