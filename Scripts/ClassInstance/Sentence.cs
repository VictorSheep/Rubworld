using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sentence {

	public bool isGoingToNextScene = false;
	public bool isIncrementGlobalGameStep = false;
	public bool isIncrementTutorialStep = false;
	public Texture picto;
	public string name;
	[TextArea(3, 110)]
	public string text;

	public Sentence(string name_, string text_, bool isIncrementGlobalGameStep_, bool isIncrementTutorialStep_){
		name = name_;
		text = text_;
		isIncrementGlobalGameStep = isIncrementGlobalGameStep_;
		isIncrementTutorialStep = isIncrementTutorialStep_;
	}

}
