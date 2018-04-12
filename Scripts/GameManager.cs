using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public int _globalGameStep = 0;
	public int _tutorialStep = 0;
	public Animator feufolletAnimator;
	public Animator playerVisualEffectAnimator;
	public Animator tutoFruitEffectAnimator;

	public void SetTutorialStep(int newTutorialStep) {
		_tutorialStep = newTutorialStep;
		feufolletAnimator.SetInteger ("tutoStep", newTutorialStep);
		if (tutoFruitEffectAnimator != null) {
			tutoFruitEffectAnimator.SetInteger ("tutoStep", newTutorialStep);
		}
		playerVisualEffectAnimator.SetTrigger ("haveUp");
	}
	public void SetGlobalGameStep(int newGlobalGameState) {
		_globalGameStep = newGlobalGameState;
	}

	public int GetGlobalGameStep() {
		return _globalGameStep;
	}
	public int GetTutorialStep() {
		return _tutorialStep;
	}

	public void Destroy(){
		Destroy (gameObject);
	}

	#region Singleton
	public static GameManager instance;

	void Awake (){
		DontDestroyOnLoad (gameObject);
		if (instance != null) {
			Debug.LogWarning ("More than one instance of GameManager found");
			return;
		}
		instance = this;
	}
	#endregion

	// Use this for initialization
	void Start ()
	{
		_globalGameStep = 0;
		_tutorialStep = 0;
	}

	public void PlayerGetItem(FountainFoodItem item) {
		if (_globalGameStep < item._value) {
			_globalGameStep = item._value;
		}
	}

	public void GoToNextScene() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}

}
