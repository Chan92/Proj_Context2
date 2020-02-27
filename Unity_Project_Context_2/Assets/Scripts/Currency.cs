using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
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

	public void AddToCurrency(int amount) {
		myCurrency += amount;
	}

	public void RemoveFromcurrency(int amount) {
		myCurrency -= amount;
	}

}
