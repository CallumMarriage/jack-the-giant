using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

	[SerializeField]
	private GameObject[] clouds;

	[SerializeField]
	private GameObject[] collectables;

	private float distanceBetweenClouds = 3f;
	private float minX, maxX;
	private float lastCloudPositionY;
	private float controlX;

	private GameObject player;

	// Use this for initialization
	void Awake () {
		controlX = 0f;
		setMinAndMaxX ();
		CreateClouds ();
		player = GameObject.Find ("Player");
	}

	void Start () {
		PositionThePlayer ();
	}
	void setMinAndMaxX() {
		Vector3 bounds = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));

		maxX = bounds.x - 0.7f;
		minX = -bounds.x + 0.7f;
	}

	void Shuffle(GameObject[] clouds) {
		for (int i = 0; i < clouds.Length; i++) {
			GameObject temp = clouds [i];
			int random = Random.Range (i, clouds.Length);
			clouds [i] = clouds [random];

			if (clouds [i].tag == "Deadly" && i > 1) {
				if (clouds [i - 1].tag == "Deadly") {
					clouds [i] = clouds [Random.Range (i, clouds.Length)];
				}
			}
			clouds [random] = temp;

		}
	}
	void CreateClouds() {
		Shuffle (clouds);

		float positionY = 0f;

		for (int i = 0; i < clouds.Length; i++) {
			Vector3 temp = clouds[i].transform.position;
			temp.y = positionY;
			temp.x = Random.Range (minX, maxX);

			temp = RandomizeClouds (temp);

			lastCloudPositionY = positionY;

			clouds [i].transform.position = temp;
			positionY -= distanceBetweenClouds;
		}
	}

	void PositionThePlayer() {
		GameObject[] darkClouds = GameObject.FindGameObjectsWithTag ("Deadly");
		GameObject[] cloudsInGame = GameObject.FindGameObjectsWithTag ("Cloud");

		for (int i = 0; i < darkClouds.Length; i++) {
			if (darkClouds[i].transform.position.y == 0f) {

				Vector3 t = darkClouds[i].transform.position;

				darkClouds[i].transform.position = new Vector3 (cloudsInGame[0].transform.position.x,
																cloudsInGame[0].transform.position.y,
																cloudsInGame[0].transform.position.z);
																					
				cloudsInGame [0].transform.position = t;

			}
				
		}
	
		Vector3 temp = cloudsInGame [0].transform.position;

		for(int i = 1; i < cloudsInGame.Length; i++){
			if (temp.y < cloudsInGame [i].transform.position.y) {
				temp = cloudsInGame [i].transform.position;
			}
		}

		temp.y += 0.8f;

		player.transform.position = temp;
	}

	Vector3 RandomizeClouds(Vector3 temp){

		if (controlX == 0) {
			temp.x = Random.Range (0.0f, maxX);
			controlX = 1;
		} else if (controlX == 1) {
			temp.x = Random.Range (0.0f, minX);
			controlX = 2;
		} else if (controlX == 2) {
			temp.x = Random.Range (1.0f, maxX);
			controlX = 3;
		} else if (controlX == 3) {
			temp.x = Random.Range (-1.0f, minX);
			controlX = 0;
		}
		return temp;
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.tag == "Cloud" || target.tag == "Deadly") {

			if (target.transform.position.y == lastCloudPositionY) {
				Shuffle (clouds);
				Shuffle (collectables);

				Vector3 temp = target.transform.position;


				for(int i = 0; i < clouds.Length; i++){
					if (!clouds [i].activeInHierarchy) {

						temp = RandomizeClouds (temp);

						temp.y -= distanceBetweenClouds;

						lastCloudPositionY = temp.y;

						clouds [i].transform.position = temp;

						clouds [i].SetActive (true);
							
					}
				}
			}
		}
	}
}
