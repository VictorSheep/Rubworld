using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	GameObject TalkPanel;
	public delegate void CallBackFunction();
	private Text nameZone;
	private Text textZone;
	private RawImage pictZone;

	private Queue<Sentence> _currentConversation = new Queue<Sentence>();
	private bool _isInDialogue = false;

	public bool GetIsInDialogue(){
		return _isInDialogue;
	}

	#region Singleton
	public static DialogueManager instance;

	void Awake (){
		DontDestroyOnLoad (gameObject);
		if (instance != null) {
			Debug.LogWarning ("More than one instance of DialogueManager found");
			return;
		}
		instance = this;
	}
	#endregion

	public void Destroy(){
		Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
	}

	public void GetTalkPanel() {
		TalkPanel = GameObject.Find ("TalkPanel");
		TalkPanel.SetActive (false);
		nameZone = TalkPanel.transform.Find("Name").GetComponent<Text> ();
		textZone = TalkPanel.transform.Find("Text").GetComponent<Text> ();
		pictZone = TalkPanel.transform.Find("Image").GetComponent<RawImage> ();
		Debug.Log (TalkPanel);
	}

	public void startConversation(Dialogue conv, CallBackFunction callBack = default(CallBackFunction) ){
		if (!_isInDialogue) {
			// Set the current conversation
			foreach (var item in conv.dialogue) {
				_currentConversation.Enqueue (item);
			}
			// Disp the dialogue text zone
			RubStates.instance.SetIsInDialogue(true);
			TalkPanel.SetActive (true);
		}

		_isInDialogue = true;

		DisplayNextSentence (_currentConversation, callBack);

	}

	public void DisplayNextSentence(Queue<Sentence> conv, CallBackFunction callBack = default(CallBackFunction) ){


		if (_currentConversation.Count <= 0) {
			EndDialogue (callBack);
			return;
		}
		Sentence sentenceToPrint = _currentConversation.Dequeue ();
		if (sentenceToPrint.isGoingToNextScene) {
			GameManager.instance.GoToNextScene ();
		}
		if (sentenceToPrint.isIncrementGlobalGameStep) {
			GameManager.instance.SetGlobalGameStep(GameManager.instance.GetTutorialStep() + 1);
		}
		if (sentenceToPrint.isIncrementTutorialStep) {
			GameManager.instance.SetTutorialStep(GameManager.instance.GetTutorialStep() + 1);
		}
		// Disp the text
		nameZone.text = sentenceToPrint.name;
		textZone.text = sentenceToPrint.text;
		pictZone.texture = sentenceToPrint.picto;


	}

	public void EndDialogue(CallBackFunction callBack = default(CallBackFunction) ){
		TalkPanel.SetActive (false);
		_currentConversation.Clear ();
		_isInDialogue = false;

		if (callBack is CallBackFunction) {
			callBack ();
		}else{
			EmptyFunction();
		}

		RubStates.instance.SetIsInDialogue(false);
	}

	public void EmptyFunction(){
		print ("Empty callBack");
	}

}
