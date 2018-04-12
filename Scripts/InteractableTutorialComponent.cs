using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTutorialComponent : MonoBehaviour {

	public int _tutorialStep;
	public GameObject hudIndication;

	private bool _canInteract = false;
	private bool _isInRange = false;
	private bool _isInDialogue = false;

	public bool GetIsInDialogue(){
		return _isInDialogue;
	}
	public bool GetIsInRange(){
		return _isInRange;
	}

	public void SetIsInDialogue(bool val){
		_isInDialogue = val;
	}
	public void SetIsInRange(bool val){
		_isInRange = val;
	}

	// Use this for initialization
	void Start () {
		hudIndication.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		// Set bools from others (some comes from DialogueManager and RubStates)
		_canInteract = _isInRange && (_tutorialStep == GameManager.instance.GetTutorialStep());

		if (_canInteract) {
			// make feedbak "[e]" just above the interactable object
			hudIndication.SetActive (true);
		} else {
			// remoove feedbak "[e]"
			hudIndication.SetActive (false);
		}
		if (_canInteract && Input.GetButtonDown("Interact")) {
			// run the action
			hudIndication.SetActive(false);
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.tag == "Player") {
			_isInRange = true;
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.tag == "Player") {
			_isInRange = false;
		}
	}
}
