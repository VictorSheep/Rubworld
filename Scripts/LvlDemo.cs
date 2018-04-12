using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlDemo : MonoBehaviour {

	const int _stepTarget = 2;
	bool _lvlFinished = false;
	public Animator canvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		_lvlFinished = GameManager.instance.GetGlobalGameStep () == _stepTarget;
		if (_lvlFinished) {
			canvas.SetBool ("lvlFinished", true);
		}
	}
}
