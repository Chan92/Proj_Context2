using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour{
	public float scrollSpeed = 14;
	private Transform cam;

	void Start(){
		cam = Camera.main.transform;
    }

    void Update(){
		Scrolling();
	}

	void Scrolling() {
		float dir = Input.GetAxis("Mouse ScrollWheel");

		if (dir != 0) {
			//cam.localPosition += new Vector3(0, dir * scrollSpeed * Time.deltaTime, 0);
			cam.localPosition += new Vector3(0, Mathf.Sign(dir) * scrollSpeed, 0);
		}
	}
}
