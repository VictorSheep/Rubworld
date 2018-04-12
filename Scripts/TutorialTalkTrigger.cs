using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTalkTrigger : MonoBehaviour {

	private bool _canInteract = false;
	private bool _isInRange = false;
	private DialoguesList _dialoguesList;

	public bool GetIsInRange(){
		return _isInRange;
	}

	public void SetIsInRange(bool val){
		_isInRange = val;
	}

	// Use this for initialization
	void Start () {
		_dialoguesList = GetComponent<DialoguesList>();
	}

	// Update is called once per frame
	void Update () {
		// Set bools from others (some comes from DialogueManager and RubStates)
		_canInteract = _isInRange;

		if (_canInteract && Input.GetButtonDown("Interact")) {
			// run the action
			_dialoguesList.RunDialogue (EndOfThisTalkTrigger);
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.tag == "Player") {
			_isInRange = true;
			_dialoguesList.RunDialogue ();
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.tag == "Player") {
			_isInRange = false;
		}
	}

	void EndOfThisTalkTrigger() {
		GameManager.instance.SetTutorialStep(GameManager.instance.GetTutorialStep() + 1);
		Object.Destroy (gameObject);
	}
}
