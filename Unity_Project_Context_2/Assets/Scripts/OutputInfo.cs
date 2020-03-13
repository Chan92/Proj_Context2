using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputInfo : MonoBehaviour{
	[SerializeField]
	private Vector3Int gridChildId;
	PipeCell childCell;

	public WaterTypes watertype {
		get;
		private set;
	}

	private void Start() {
		watertype = WaterTypes.NoWater;

		StartCoroutine(SlowStart());
	}

	public void SetWaterType(WaterTypes newType) {
		watertype = newType;

		if(childCell != null && childCell.pipeForm != PipeForms.Hidden){
			//if(OpenOnTop()) {
				childCell.watertype = newType;
				PipeGrid.instance.CheckGrid();
			//}
		}
	}

	IEnumerator SlowStart() {
		yield return null;
		childCell = PipeGrid.instance.grid[gridChildId.x, gridChildId.y, gridChildId.z];
	}

	private bool OpenOnTop() {
		switch(childCell.pipeForm) {
			case PipeForms.I_shape:
				if(Mathf.Abs(childCell.angle) == 90) {
					return true;
				} else {
					return false;
				}
			case PipeForms.L_shape:
				if(childCell.angle == 0 || childCell.angle == 90) {
					return true;
				} else {
					return false;
				}
			case PipeForms.T_shape:
				if(childCell.angle != -90) {
					return true;
				} else {
					return false;
				}
			case PipeForms.X_shape:
				return true;
			default:
				return false;
		}
	}
}
