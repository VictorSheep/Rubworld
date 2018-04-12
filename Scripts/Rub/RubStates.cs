using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubStates : MonoBehaviour {

	#region Singleton
	public static RubStates instance;

	void Awake (){
		if (instance != null) {
			Debug.LogWarning ("More than one instance of RubStates found");
			return;
		}
		instance = this;
	}
	#endregion

	bool _isInPasserelleRotation = false;
	bool _isInDialogue = false;
	bool _isOnPasserelle = false;
	bool _isMotionless = false;

	public GameObject audioGetReward;
	AudioSource asGetReward;

	// Getter
	public bool GetIsInPasserelleRotation(){
		return _isInPasserelleRotation;
	}
	public bool GetIsInDialogue(){
		return _isInDialogue;
	}
	public bool GetIsOnPasserelle() {
		return _isOnPasserelle;
	}
	public bool GetIsMotionless() {
		return _isMotionless;
	}

	// Setter
	public void SetIsInPasserelleRotation(bool val){
		_isInPasserelleRotation = val;
	}
	public void SetIsInDialogue(bool val){
		_isInDialogue = val;
	}
	public void SetIsOnPasserelle(bool val) {
		_isOnPasserelle = val;
	}
	public void SetIsMotionless(bool val) {
		_isMotionless = val;
	}

	// Use this for initialization
	void Start () {
		asGetReward = audioGetReward.GetComponent<AudioSource> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetReward() {
		asGetReward.Play ();
	}

}
