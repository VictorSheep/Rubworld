using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passerelle : MonoBehaviour {

	public int _rotationDirection = -1; // -1 for clockwise / 1 for anti-clockwise
	public string _rotationAxis = "x";
	public Color _normalColor = new Color (0.04f, 0.04f, 0.04f);
	public Color _activableColor = new Color (1, 1, 1);

	private string _playerGravity1 = "y";
	private string _playerGravity2 = "z";
	private string _newGravityAxis = "";
	private MeshRenderer _meshRend;

	private RubController RubScript;
	private Rotate90 Rotate90Script;
	bool _isTurning = false;
	bool _canChildPlayer = false;

	void Start()
	{
		Rotate90Script = GetComponent<Rotate90>();
		_meshRend = gameObject.GetComponent<MeshRenderer> ();

		// Set les 2 potentiels axes de gravités du player selon sens de rotation de la passerelle
		if (_rotationAxis == "x") {
			_playerGravity1 = "y";
			_playerGravity2 = "z";
		} else if (_rotationAxis == "y") {
			_playerGravity1 = "x";
			_playerGravity2 = "z";
		} else if (_rotationAxis == "z") {
			_playerGravity1 = "x";
			_playerGravity2 = "y";
		}

		_meshRend.material.color = _normalColor;
	}

	// Update is called once per frame
	void Update () {
		
		if (Input.GetButtonDown("Interact") && (!_isTurning) && _canChildPlayer && !RubStates.instance.GetIsInDialogue())
		{
			_isTurning = true;
			TakePlayer();
			RubScript.SetGravityAxis (_newGravityAxis);
			Rotate90Script.RotateObject(_rotationAxis, _rotationDirection, EndOfRotation);
		}
	}
		
	private void TakePlayer ()
	{
		// The player become child of this passerelle
		GameObject player = GameObject.Find("RubLandmark");
		player.transform.SetParent(transform);
		RubStates.instance.SetIsInPasserelleRotation (true);
	}

	private void DropPlayer ()
	{
		// Removes the child player from the Passerelle
		GameObject player = GameObject.Find("RubLandmark");
		player.transform.parent = null;
		RubStates.instance.SetIsInPasserelleRotation (false);
	}

	private void EndOfRotation ()
	{
		_isTurning = false;
		//RubScript.SetGravityAxis (_newGravityAxis);
		DropPlayer ();
		// reset the Passerelle rotation to 0
		transform.localEulerAngles = new Vector3 (
			0,
			0,
			0
		);
	}
	/*
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			// We get the script to be able to edit, afterwards,
			// the _gravityAxis property of the player
			RubScript = collision.gameObject.GetComponent<RubController>();
		}
	}
*/
	private void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Player")
		{
			// We get the script to be able to edit, afterwards,
			// the _gravityAxis property of the player
			RubScript = collider.GetComponent<RubController>();
			_meshRend.material.color = _activableColor;
		}
	}

	void OnTriggerStay(Collider collider)
	{
		if (collider.tag == "Player") {
			_canChildPlayer = true;
			RubStates.instance.SetIsOnPasserelle (true);

			// Depending on the severity of the player, we set the correct
			// direction of rotation and we plan to change
			// the player's _gravityAxis property after the rotation
			if (RubScript.GetGravityAxis() == _playerGravity1) {
				_rotationDirection = -1;
				_newGravityAxis = _playerGravity2;
			} else if (RubScript.GetGravityAxis() == _playerGravity2) {
				_rotationDirection = 1;
				_newGravityAxis = _playerGravity1;
			}
		}

	}

	private void OnTriggerExit(Collider collider)
	{
		// If the player is no longer in contact with the
		// gateway it is not allowed to pass as a child
		_canChildPlayer = false;
		RubStates.instance.SetIsOnPasserelle (false);
		if (collider.tag == "Player") {
			_meshRend.material.color = _normalColor;
		}
	}
}
