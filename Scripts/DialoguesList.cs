using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguesList : MonoBehaviour {

	public List<Dialogue> dialogues;

	private int dialogueIndex = 0;

	public void RunDialogue(DialogueManager.CallBackFunction callBack = default(DialogueManager.CallBackFunction)) {
		int globalGameStep = GameManager.instance.GetGlobalGameStep ();
		// If the current global game step is higher than the last index of dialogues, we will show the last dialogue
		if (globalGameStep >= dialogues.Count) {
			dialogueIndex = dialogues.Count - 1;
		} else {
			dialogueIndex = globalGameStep;
		}

		if (callBack is DialogueManager.CallBackFunction) {
			DialogueManager.instance.startConversation (dialogues[dialogueIndex], callBack);
		}else{
			DialogueManager.instance.startConversation (dialogues[dialogueIndex], EmptyFunction);
		}

	}

	public void EmptyFunction(){
		//print ("Empty callBack");
	}
}
