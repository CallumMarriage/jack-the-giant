﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour {
	private float minX, maxX;
	// Use this for initialization
	void Start () {
		setMinAndMaxX ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < minX) {
			Vector3 temp = transform.position;
			temp.x = minX;

			transform.position = temp;
		} else if (transform.position.x > maxX) {
			Vector3 temp = transform.position;
			temp.x = maxX;
			transform.position = temp;
		}
	}

	void setMinAndMaxX() {
		Vector3 bounds = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));
		maxX = bounds.x - 0.3f;
		minX = -bounds.x + 0.3f;
	}

}
