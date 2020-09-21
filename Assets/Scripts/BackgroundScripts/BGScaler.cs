using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();
		Vector3 tempScale = transform.localScale;

		float width = spriteRenderer.sprite.bounds.size.x;

		float worldHeight = Camera.main.orthographicSize * 2;
		float worldWidth = worldHeight / Screen.height * Screen.height;

		tempScale.x = worldWidth / width;

		transform.localScale = tempScale;
	}
}
