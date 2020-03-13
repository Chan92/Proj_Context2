using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour{
	public int itemFee;
	public bool pipe = true;
	public PipeForms form;
	public PipeTypes type;

	private Text feeText;

    void Start() {
		feeText = transform.Find("FeeText").GetComponent<Text>();
		feeText.text = itemFee + "$";
	}

	public void Buy() {
		if(Currency.Instance.myCurrency >= itemFee) {
			Currency.Instance.RemoveFromCurrency(itemFee);

			if(pipe) {
				GetPipe();
			} else {
				PipeBreaker.instance.AddBreaker();
			}
		}
	}

	void GetPipe() {
		PipeLineManager.instance.BuyPipeLine();
	}
}