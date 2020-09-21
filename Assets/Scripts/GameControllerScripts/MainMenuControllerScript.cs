using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuControllerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void StartGame () {
		Application.LoadLevel ("Gameplay");

	}

	public void HighscoreMenu (){
		Application.LoadLevel ("HighscoreScene");
	}

	public void OptionsMenu (){
		Application.LoadLevel ("OptionsMenu");
	}

	public void QuitGame (){
		Application.Quit ();
	}

	public void MusicButton (){
	}
		
}
