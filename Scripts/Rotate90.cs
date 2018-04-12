using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate90 : MonoBehaviour {

	private int _rotationStep = 5; // should be less than 90 (Use this for initialization)
	private bool _isCompleteRotation;
	private Vector3 _currentRotation, _targetRotation;
	public delegate void CallBackFunction();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RotateObject (string _rotationAxis, float _rotationDirection, CallBackFunction callBack)
	{
		_currentRotation = gameObject.transform.eulerAngles;
		if (_rotationAxis == "x") {
			_targetRotation.x = (_currentRotation.x + (90 * _rotationDirection));
		}
		if (_rotationAxis == "y") {
			_targetRotation.y = (_currentRotation.y + (90 * _rotationDirection));
		}
		if (_rotationAxis == "z") {
			_targetRotation.z = (_currentRotation.z + (90 * _rotationDirection));
		}
		StartCoroutine(ObjectRotationAnimation(_rotationAxis, _rotationDirection, callBack));
	}

	// Turning object of _rotationStep degrees while his rotation is not equal to _targetRotation
	// _rotationAxis ( "x", "y", "z" )
	// _rotationDirection ( -1 or 1 )
	// callBack ( void function )
	IEnumerator ObjectRotationAnimation(string _rotationAxis, float _rotationDirection, CallBackFunction callBack)
	{
		// add rotation step to current rotation.
		if (_rotationAxis == "x") {
			_currentRotation.x += (_rotationStep * _rotationDirection);
			_isCompleteRotation =
				((int)_currentRotation.x > (int)_targetRotation.x && _rotationDirection < 0) // for clockwise
				||((int)_currentRotation.x < (int)_targetRotation.x && _rotationDirection > 0); // for anti-clockwise
		}
		if (_rotationAxis == "y") {
			_currentRotation.y += (_rotationStep * _rotationDirection);
			_isCompleteRotation =
				((int)_currentRotation.y > (int)_targetRotation.y && _rotationDirection < 0) // for clockwise
				||((int)_currentRotation.y < (int)_targetRotation.y && _rotationDirection > 0); // for anti-clockwise
		}
		if (_rotationAxis == "z") {
			_currentRotation.z += (_rotationStep * _rotationDirection);
			_isCompleteRotation =
				((int)_currentRotation.z > (int)_targetRotation.z && _rotationDirection < 0) // for clockwise
				||((int)_currentRotation.z < (int)_targetRotation.z && _rotationDirection > 0); // for anti-clockwise
		}

		gameObject.transform.eulerAngles = _currentRotation;
		yield return new WaitForSeconds(0);

		if (_isCompleteRotation) // If animation is not finished
		{
			StartCoroutine(ObjectRotationAnimation(_rotationAxis, _rotationDirection, callBack));
		}
		else // when animation is finished
		{
			gameObject.transform.eulerAngles = _targetRotation;
			callBack ();
		}
	}
}
