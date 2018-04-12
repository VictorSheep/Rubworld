using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	void Start()
	{
		// init the game (for demo)
		if (GameManager.instance is GameManager) {
			GameManager.instance.Destroy ();
		}
		if (DialogueManager.instance is DialogueManager) {
			DialogueManager.instance.Destroy ();
		}
	}

	public void PlayGame()
	{
		SceneManager.LoadScene ("lvl_tuto");
	}

	public void QuitGame()
	{
		Debug.Log ("Quit");
		Application.Quit ();
	}

}
