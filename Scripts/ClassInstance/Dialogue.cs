using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

	public List<Sentence> dialogue;

	public Dialogue(List<Sentence> _dialogue){
		dialogue = _dialogue;
	}

}
