using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkableNpc : MonoBehaviour {
	
	public GameObject hudIndication;

	private bool _canInteract = false;
	private bool _isInRange = false;
	private bool _isInDialogue = false;
	private DialoguesList _dialoguesList;

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
		_dialoguesList = GetComponent<DialoguesList>();
	}

	// Update is called once per frame
	void Update () {
		// Set bools from others (some comes from DialogueManager and RubStates)
		_isInDialogue = DialogueManager.instance.GetIsInDialogue();
		_canInteract = _isInRange && !RubStates.instance.GetIsOnPasserelle ();

		if (_canInteract && !_isInDialogue) {
			// make feedbak "[e]" just above the interactable object
			hudIndication.SetActive (true);
		} else {
			// remoove feedbak "[e]"
			hudIndication.SetActive (false);
		}
		if (_canInteract && Input.GetButtonDown("Interact")) {
			// run the action
			hudIndication.SetActive(false);
			_dialoguesList.RunDialogue ();
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
