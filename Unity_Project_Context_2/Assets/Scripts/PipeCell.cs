using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCell{
	public WaterTypes watertype {
		get;
		set;
	}

	public PipeTypes pipeType {
		get;
		private set;
	}

	public PipeForms pipeForm {
		get;
		private set;
	}

	public float angle;
	public Vector3Int gridPosition;
	public Vector3 worldPoint;

	public PipeCell(Vector3 worldPos, Vector3Int gridPos) {
		worldPoint = worldPos;
		gridPosition = gridPos;

		watertype = WaterTypes.NoWater;
		pipeType = PipeTypes.Empty;
		pipeForm = PipeForms.Hidden;
	}

	public void SetPipe(PipeTypes type, PipeForms form, float angleY) {
		pipeType = type;
		pipeForm = form;
		angle = angleY;
	}

	public void SetAngleY(float angleY) {
		angle = angleY;
	}

	public void DisabledPipe() {
		pipeForm = PipeForms.Hidden;
		pipeType = PipeTypes.Empty;
		watertype = WaterTypes.NoWater;
	}

	//string x = "329847";
	//x[0].CompareTo(x[1]);

	///*
	///
	/// if type = I 
	///		case: r = 0		> 0110
	/// 	case: r = 90	> 1001
	/// 	case: r = 180	> 0110
	/// 	case: r = -90	> 1001
	/// if type = L 
	///		case: r = 0		> 1100
	/// 	case: r = 90	> 1010
	/// 	case: r = 180	> 0011
	/// 	case: r = -90	> 0101
	/// if type = T 
	///		case: r = 0		> 1101
	/// 	case: r = 90	> 1110
	/// 	case: r = 180	> 1011
	/// 	case: r = -90	> 0111
	/// if type = X 
	///		case: r = 0		> 1111
	/// 	case: r = 90	> 1111
	/// 	case: r = 180	> 1111
	/// 	case: r = -90	> 1111



}
