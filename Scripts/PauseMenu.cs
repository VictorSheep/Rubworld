using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public static bool _isPaused = false;

	public GameObject _pauseMenuUi;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (_isPaused) {
				Resume ();
			} else {
				Pause ();
			}
		}
	}

	public void Resume(){
		_pauseMenuUi.SetActive (false);
		Time.timeScale = 1f;
		_isPaused = false;
	}

	void Pause(){
		_pauseMenuUi.SetActive (true);
		Time.timeScale = 0.1f;
		_isPaused = true;
	}

	public void LoadMenu(){
		Time.timeScale = 1f;
		_isPaused = false;
		SceneManager.LoadScene (0);
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
