using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeInfo : MonoBehaviour{
	PipeCell myCell;
	PipeTypes pipetype;

	public void Placement() {
		myCell = FindCell();
		myCell.SetPipe(pipetype, ReCalculateState(), transform.localEulerAngles.y);

		PipeGrid.instance.CheckGrid();
		//print("Placement: " + transform.name + " >mycell: " + myCell);
	}

	public void Breaking() {
		//myCell = FindCell();
		//print("Breaking: " + transform.name + " >mycell: " + myCell);


		myCell.DisabledPipe();
		PipeGrid.instance.CheckGrid();
		myCell = null;
	}

	public void Rotate() {
		//myCell = FindCell();
		//print("Rotate: " + transform.name + " >mycell: " + myCell);


		myCell.SetAngleY(transform.localEulerAngles.y);
		PipeGrid.instance.CheckGrid();
	}

	PipeCell FindCell() {
		Vector3 myPos = transform.position;
		int floor = PipeLineManager.instance.i_Floor;

		for(int x = 0; x < 4; x++) {
			for(int y = 0; y < 4; y++) {
				Vector3 cellPos = PipeGrid.instance.grid[floor, x, y].worldPoint;
				myPos.y = cellPos.y = 0;

				float dis = Vector3.Distance(myPos, cellPos);
				//print("celPos: " + cellPos + " -myPos: " + myPos + " --dis: " + dis);

				if(dis < 1.5f) {
					return PipeGrid.instance.grid[floor, x, y];
				}
			}
		}

		Debug.Log("Error - couldnt find cell");
		return null;
	}

	PipeForms ReCalculateState() {
		PipeLine.PipeLine_State state = transform.GetComponent<PipeLine>().MyState;
		switch(state) {
			case PipeLine.PipeLine_State.PS_None:
				return PipeForms.Hidden;
			case PipeLine.PipeLine_State.PS_Corner:
				return PipeForms.L_shape;
			case PipeLine.PipeLine_State.PS_I:
				return PipeForms.I_shape;
			case PipeLine.PipeLine_State.PS_T:
				return PipeForms.T_shape;
			case PipeLine.PipeLine_State.PS_X:
				return PipeForms.X_shape;
			default:
				return PipeForms.Hidden;
		}
	}
}
