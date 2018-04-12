using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	//AudioSource s;
	AudioLowPassFilter LPFilter;

	// Use this for initialization
	void Start () {
		//s = gameObject.GetComponent<AudioSource> ();
		LPFilter = gameObject.GetComponent<AudioLowPassFilter> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PauseMenu._isPaused) {
			LPFilter.cutoffFrequency = 680;
		} else {
			LPFilter.cutoffFrequency = 19000;
		}
	}
}
