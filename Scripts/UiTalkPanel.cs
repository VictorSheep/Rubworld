using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTalkPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DialogueManager.instance.GetTalkPanel ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
