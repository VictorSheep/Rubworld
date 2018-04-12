using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubController : MonoBehaviour {

	public float _speed = 2;
	public Transform _respawnPoint;
	public Animator rubAnimator;

	float _gravityForce = -0.13F;
	bool _isInTheAir = true;
	bool _canRotate = false;
	bool _canMove = false;

	public string _gravityAxis = "y";
	public float _angleOffset = 0;

	// Getter
	public string GetGravityAxis(){
		return _gravityAxis;
	}

	// Setter
	public void SetGravityAxis(string newGravity){
		if (_gravityAxis == "z" && newGravity == "y") {
			_angleOffset += 90;
			_angleOffset = _angleOffset % 360;
		}
		if (_gravityAxis == "y" && newGravity == "z") {
			_angleOffset -= 90;
			_angleOffset = _angleOffset % 360;
		}
		_gravityAxis = newGravity;

		// Save new respawn position
		//_respawnPoint.transform.position = transform.position;
	}

	/// //////////////////////////////////////////////////////
	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update () {

		if (!RubStates.instance.GetIsMotionless()){
			bool _inPasserelleRotation = RubStates.instance.GetIsInPasserelleRotation ();
			bool _isInDialogue = RubStates.instance.GetIsInDialogue ();

			if (!PauseMenu._isPaused) {
				float _hk = Input.GetAxis("HorizontalKey"); // right(1) left(-1)
				float _vk = Input.GetAxis("VerticalKey"); // up(1) back(-1)

				// if the player is on passerelle that is rotating, he can't rotate
				_canRotate = !_inPasserelleRotation && !_isInDialogue;
				// if the player don't tuch the floor (_isInTheAir), he can't move
				// if the player is on passerelle that is rotating, he can't move
				_canMove = !_isInTheAir && !_inPasserelleRotation && !_isInDialogue;

				if (_hk == 0 && _vk == 0) {
					rubAnimator.SetBool ("isWalking", false);
				}

				if (_canRotate) { RotateRub (_hk, _vk); }
				if (_canMove) { TranslateRub(_hk, _vk); }
			}
			ApplyRubGravity();
		}
	}

	void RotateRub (float _hk, float _vk)
	{
		// check if one key at least is pressed
		if (_hk != 0 || _vk != 0)
		{
			// calculate angle according to the inputs
			float _angle = (_hk * 90 + 90 * Mathf.Abs(_hk)) + (_vk * 90);
			_angle = _angle / (1 + (Mathf.Abs(_hk)* Mathf.Abs(_vk)));
			// one particular case
			if (_hk == 1 && _vk == -1)
			{
				_angle = -135;
			}
			_angle += _angleOffset;

			// apply the angle to player
			transform.localEulerAngles = new Vector3(
				transform.localEulerAngles.x,
				_angle,
				transform.localEulerAngles.z
			);

		}
	}

	void TranslateRub (float _hk, float _vk)
	{
		if (!_isInTheAir)
		{
			if (_hk != 0 || _vk != 0)
			{
				transform.Translate((Vector3.forward * Time.deltaTime * _speed));
				rubAnimator.SetBool ("isWalking", true);
			}
		}
	}

	void ApplyRubGravity ()
	{
		if (_isInTheAir) {
			if (PauseMenu._isPaused) {
				transform.Translate (new Vector3 (0, _gravityForce/10, 0));
			} else {
				transform.Translate (new Vector3 (0, _gravityForce, 0));
			}
			rubAnimator.SetBool ("isFalling", true);
		} else {
			rubAnimator.SetBool ("isFalling", false);
		}
	}

	public void Die() {
		GameObject player = GameObject.Find("RubLandmark");
		player.transform.rotation = new Quaternion (0, 0, 0, 0);
		transform.position = _respawnPoint.transform.position;
		transform.rotation = new Quaternion (0, 180, 0, 0);
		SetGravityAxis ("y");
		_angleOffset = 0;
	}

	void OnTriggerStay(Collider collider)
	{
		// if the player's feet tuch the floor, he is not in the air
		if (collider.tag == "Floor") {
			_isInTheAir = false;
		}
	}
	void OnTriggerExit(Collider collider)
	{
		_isInTheAir = true;
	}
}
