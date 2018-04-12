using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMessage : MonoBehaviour {

	public int _step = -1;
	public string _gravityAxis = "y";
	public GameObject _messagePanel;
	public Sentence sentenceToPrint;
	public bool quit = false;

	private Text nameZone;
	private Text textZone;
	private RawImage pictZone;
	private bool _isInTutorialMessage = false;
	private RubController _rubScript;

	// Use this for initialization
	void Start () {
		_messagePanel.SetActive (false);
		nameZone = _messagePanel.transform.Find("Name").GetComponent<Text> ();
		textZone = _messagePanel.transform.Find("Text").GetComponent<Text> ();
		pictZone = _messagePanel.transform.Find("Image").GetComponent<RawImage> ();
		GameObject player = GameObject.Find("Rub");
		_rubScript = player.GetComponent<RubController> ();
	}

	void OnTriggerEnter(Collider collider) {
		
		bool canDoInteraction_ = (_step == GameManager.instance.GetTutorialStep () || _step == -1) && _rubScript.GetGravityAxis () == _gravityAxis;

		if (canDoInteraction_) {
			if (quit) {
				_messagePanel.SetActive (false);
			} else {
				if (!_isInTutorialMessage) {

					if (collider.tag == "Player") {
						nameZone.text = sentenceToPrint.name;
						textZone.text = sentenceToPrint.text;
						pictZone.texture = sentenceToPrint.picto;
						RubStates.instance.SetIsInDialogue (false);
						_messagePanel.SetActive (true);
						GameManager.instance.SetTutorialStep (GameManager.instance.GetTutorialStep () + 1);
					}

				}
			}
		}
	}
}
