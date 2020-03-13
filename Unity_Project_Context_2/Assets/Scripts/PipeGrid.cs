using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaterTypes {
	NoWater,
	CleanWater,
	AcidWater,
	TrashWater
}

public enum PipeTypes {
	Empty,
	Normal,
	Filter
}

public enum PipeForms {
	Hidden,
	I_shape,
	L_shape,
	T_shape,
	X_shape
}

public class PipeGrid : MonoBehaviour{
	public WaterTypes watertype;
	public PipeCell[,,] grid;
	private int gridFloors = 5;
	private Vector2Int gridSize = new Vector2Int(4, 4);
	public Transform[] snapPoints;
	public static PipeGrid instance;

	private void Awake() {
		instance = this;
	}

	void Start(){
		int floorId = PipeLineManager.instance.i_Floor;
		grid = new PipeCell[gridFloors, gridSize.x, gridSize.y];
		for(int i = 0; i < gridFloors; i++) {
			CreateGrit(floorId + i);
		}
	}

 	private void CreateGrit(int floorId) {		
		int pointId = 0;

		for(int x = 0; x < gridSize.x; x++) {
			for(int y = 0; y < gridSize.y; y++) {
				Vector3 pos = snapPoints[pointId].position;
				pos.y = pos.y - (floorId * PipeLineManager.instance.f_floorDistance);
				grid[floorId, x, y] = new PipeCell(pos, new Vector3Int(floorId,x,y));
				pointId++;
			}
		}
	}

	private string GetOpeningsId(PipeForms form, float rot) {
		switch(form) {
			case PipeForms.Hidden:
				return "0000";
			case PipeForms.I_shape:
				switch(rot) {
					case 0f:
						return "0110";
					case 90f:
						return "1001";
					case 180f:
						return "0110";
					case -90f:
						return "1001";
					default:
						return "Error";
				}
			case PipeForms.L_shape:
				switch(rot) {
					case 0f:
						return "1100";
					case 90f:
						return "1010";
					case 180f:
						return "0011";
					case -90f:
						return "0101";
					default:
						return "Error";
				}
			case PipeForms.T_shape:
				switch(rot) {
					case 0f:
						return "1101";
					case 90f:
						return "1110";
					case 180f:
						return "1011";
					case -90f:
						return "0111";
					default:
						return "Error";
				}
			case PipeForms.X_shape:
				return "1111";
			default:
				return "Error";
		}
	}

	//called on every change
	//breaking pipe, added pipe, rotated pipe
	public void CheckGrid() {
		for(int floor = 0; floor < gridFloors; floor++) {
			for(int x = 0; x < gridSize.x; x++) {
				for(int y = 0; y < gridSize.y; y++) {
					CheckNeighbours(grid[floor, x, y]);
				}
			}
		}
	}

	private void CheckNeighbours(PipeCell cell) {
		List<PipeCell> n = GetNeighbours(cell);
		int[] neighbourType = new int[4];

		for(int i = 0; i < n.Count; i++) {
			switch(n[i].watertype) {
				case WaterTypes.NoWater:
					neighbourType[0]++;
					break;
				case WaterTypes.CleanWater:
					neighbourType[1]++;
					break;
				case WaterTypes.AcidWater:
					neighbourType[2]++;
					break;
				case WaterTypes.TrashWater:
					neighbourType[3]++;
					break;
				default:
					break;
			}
		}

		CheckType(cell, neighbourType);
	}

	private List<PipeCell> GetNeighbours(PipeCell cell) {
		List<PipeCell> neigbours = new List<PipeCell>();
		string cellOpeningId = GetOpeningsId(cell.pipeForm, cell.angle);
		int cellId = 0;

		for(int x = -1; x <= 1; x++) {
			for(int y = -1; y <= 1; y++) {
				if(Mathf.Abs(x) == Mathf.Abs(y)) {
					continue;
				}

				int checkPosX = cell.gridPosition.y + x;
				int checkPosY = cell.gridPosition.z + y;

				if(checkPosX >= 0 && checkPosX < 4 &&
					checkPosY >= 0 && checkPosY < 4) {
					PipeCell n = grid[PipeLineManager.instance.i_Floor, checkPosX, checkPosY];
					string nOpeningId = GetOpeningsId(n.pipeForm, n.angle);

					if(ConnectedPipe(cellOpeningId, nOpeningId, cellId)) {
						neigbours.Add(n);
					}
				}

				cellId++;
			}
		}

		return neigbours;
	}

	private bool ConnectedPipe(string idA, string idB, int id) {
		return idA[id].Equals(idB[id]);
	}
	
	private void CheckType(PipeCell cell, int[] neighbourType) {
		for(int j = neighbourType.Length -1; j >= 0; j--) {
			if(neighbourType[j] > 0) {
				if(cell.watertype != (WaterTypes) j) {
					cell.watertype = (WaterTypes) j;
					CheckNeighbours(cell);
					break;
				} else {
					break;
				}
			}
		}
	}

	private void OnDrawGizmos() {
		if(grid != null) {
			foreach(PipeCell c in grid) {
				switch(c.watertype) {
					case WaterTypes.NoWater:
						Gizmos.color = Color.white;
						break;
					case WaterTypes.CleanWater:
						Gizmos.color = Color.blue;
						break;
					case WaterTypes.AcidWater:
						Gizmos.color = Color.yellow;
						break;
					case WaterTypes.TrashWater:
						Gizmos.color = Color.red;
						break;
				}

				Gizmos.DrawWireSphere(c.worldPoint, 0.15f);
			}
		}
	}
}
